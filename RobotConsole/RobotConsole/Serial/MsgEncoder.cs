using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotConsole
{
    class MsgEncoder
    {

        public bool UartEncodeAndSendMessage(ushort msgFunction, byte[] msgPayload)
        {
            short PayloadLenghtTest = Protocol.CheckFunctionLenght(msgFunction);
            ushort msgPayloadLenght = (ushort)msgPayload.Length;
            if (PayloadLenghtTest != -2)
            {
                if (PayloadLenghtTest != -1)
                {
                    msgPayloadLenght = (ushort)PayloadLenghtTest;
                }
            }
            
            if (msgPayloadLenght == msgPayload.Length)
            {
                byte[] msg = EncodeWithoutChecksum(msgFunction, msgPayloadLenght, msgPayload);
                byte checksum = CalculateChecksum(msgFunction, msgPayloadLenght, msgPayload);

                msg[msg.Length - 1] = checksum;
                if (Program.serialPort != null)
                {
                    Program.serialPort.Write(msg, 0, msg.Length);
                    OnSendMessage(msgFunction, msgPayloadLenght, msgPayload, checksum);
                    return true;
                }
                else
                {
                    OnSerialDisconnected();
                }
            }
            else
            {
                OnWrongPayloadSent();
            }
            if (PayloadLenghtTest == -2)
            {
                OnUnknownFunctionSent();
            }
            return false;
        }

        private static byte[] EncodeWithoutChecksum(ushort msgFunction, ushort msgPayloadLength, byte[] msgPayload)
        {
            // Convert Function to byte
            byte LbyteFunction = (byte)(msgFunction >> 0);
            byte HbyteFunction = (byte)(msgFunction >> 8);

            byte LbytePayloadsLength = (byte)(msgPayloadLength >> 0);
            byte HbytePayloadsLength = (byte)(msgPayloadLength >> 8);

            // Append all bytes
            byte[] msg = new byte[6 + msgPayload.Length];
            ushort i;
            msg[0] = Protocol.SOF;
            msg[1] = HbyteFunction;
            msg[2] = LbyteFunction;
            msg[3] = HbytePayloadsLength;
            msg[4] = LbytePayloadsLength;
            for (i = 0; i < msgPayload.Length; i++)
            {
                msg[5 + i] = msgPayload[i];
            }
            return msg;
        }

        private static byte CalculateChecksum(ushort msgFunction, ushort msgPayloadLength, byte[] msgPayload)
        {
            byte[] msg = EncodeWithoutChecksum(msgFunction, msgPayloadLength, msgPayload);

            byte checksum = msg[0];
            for (int i = 1; i < msg.Length; i++)
            {
                checksum ^= msg[i];
            }
            return checksum;
        }

        public event EventHandler<Protocol.MessageByteArgs> OnSendMessageEvent;
        public event EventHandler<Protocol.LedMessageArgs> OnSetLedEvent;
        public event EventHandler<Protocol.MotorMessageArgs> OnSetMotorSpeedEvent;
        public event EventHandler<Protocol.StateMessageArgs> OnSetStateEvent;
        public event EventHandler<EventArgs> OnSerialDisconnectedEvent;
        public event EventHandler<EventArgs> OnWrongPayloadSentEvent;
        public event EventHandler<EventArgs> OnUnknownFunctionSentEvent;
        public virtual void OnSendMessage(ushort msgFunction, ushort msgPayloadLenght, byte[] msgPayload, byte checksum)
        {
            OnSendMessageEvent?.Invoke(this, new Protocol.MessageByteArgs(msgFunction, msgPayloadLenght, msgPayload, checksum));
            switch (msgFunction)
            {
                case (ushort)Protocol.FunctionName.SET_LED:
                    OnSetLed(msgPayload[0], (msgPayload[1] == 0x00) ? false : true);
                    break;
                case (ushort)Protocol.FunctionName.SET_MOTOR:
                    OnSetMotorSpeed((sbyte)msgPayload[0], (sbyte)msgPayload[1]);
                    break;
                case (ushort)Protocol.FunctionName.SET_STATE:
                    OnSetState(msgPayload[0]);
                    break;
            }
        }
        public virtual void OnSetLed(ushort led_number, bool state)
        {
            OnSetLedEvent?.Invoke(this, new Protocol.LedMessageArgs(led_number, state));
        }
        public virtual void OnSetMotorSpeed(sbyte left_motor_speed, sbyte right_motor_speed)
        {
            OnSetMotorSpeedEvent?.Invoke(this, new Protocol.MotorMessageArgs(left_motor_speed, right_motor_speed));
        }
        public virtual void OnSetState(ushort state)
        {
            OnSetStateEvent?.Invoke(this, new Protocol.StateMessageArgs(state));
        }

        public virtual void OnSerialDisconnected()
        {
            OnSerialDisconnectedEvent?.Invoke(this, new EventArgs());
        }
        public virtual void OnWrongPayloadSent()
        {
            OnWrongPayloadSentEvent?.Invoke(this, new EventArgs());
        }
        public virtual void OnUnknownFunctionSent()
        {
            OnUnknownFunctionSentEvent?.Invoke(this, new EventArgs());
        }
    }
}
