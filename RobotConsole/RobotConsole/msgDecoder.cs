using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RobotConsole
{

    class MsgDecoder 
    {
        
        private enum State
        {
            Waiting,
            FunctionMSB,
            FunctionLSB,
            PayloadLengthMSB,
            PayloadLengthLSB,
            Payload,
            CheckSum
        }
        private enum FunctionName
        {
            Set_LED     = 0x0020,
            Get_IR      = 0x0030,
            Set_Motor   = 0x0040,
            Get_Text    = 0x0080
            // Add all protocol
        }

        static State actualState = State.Waiting;
        const byte SOF = 0xFE;
        const ushort MAX_MSG_LENGHT = 255;

        private static byte functionMSB;
        private static byte functionLSB;
        private static byte payloadLenghtMSB;
        private static byte payloadLenghtLSB;

        private static ushort msgFunction;
        private static ushort msgPayloadLenght;
        private static byte[] msgPayload;
        private static byte msgChecksum;

        private static int msgPayloadIndex = 0; // Maybe edit type
        public void ByteReceived(byte b)
        {
            switch (actualState)
            {
                case State.Waiting:
                    if (b == SOF)
                    {
                        OnSOFReceived(b);
                    }
                    else
                    {
                        OnUnknowReceived(b);
                    }
                    break;

                case State.FunctionMSB:
                    OnFunctionMSBReceived(b);
                    break;

                case State.FunctionLSB:
                    OnFunctionLSBReceived(b);
                    break;

                case State.PayloadLengthMSB:
                    OnPayloadLenghtMSBReceided(b);
                    break;

                case State.PayloadLengthLSB:
                    OnPayloadLenghtLSBReceided(b);
                    break;

                case State.Payload:
                    OnPayloadByteReceived(b);
                    break;

                case State.CheckSum:
                    OnCheckSumReceived(b);
                    break;
            }
            
        }

        public event EventHandler<DecodeByteArgs> OnSOFByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnUnknowByteEvent;
        public event EventHandler<DecodeByteArgs> OnFunctionMSBByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnFunctionLSBByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnPayloadLenghtMSBByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnPayloadLenghtLSBByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnPayloadByteReceivedEvent;
        public event EventHandler<DecodePayloadArgs> OnPayloadReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnChecksumByteReceivedEvent;
        public event EventHandler<DecodeMsgArgs> OnCorrectChecksumEvent;
        public event EventHandler<DecodeMsgArgs> OnWrongChecksumEvent;

        public virtual void OnSOFReceived(byte e)
        {
            actualState = State.FunctionMSB;
            OnSOFByteReceivedEvent?.Invoke(this, new DecodeByteArgs(e));

        }
        public virtual void OnUnknowReceived(byte e)
        {
            OnUnknowByteEvent?.Invoke(this, new DecodeByteArgs(e));

        }
        public virtual void OnFunctionMSBReceived(byte e)
        {
            functionMSB = e;
            msgFunction = (ushort)(e << 8);
            actualState = State.FunctionLSB;
            OnFunctionMSBByteReceivedEvent?.Invoke(this, new DecodeByteArgs(e));
        }
        public virtual void OnFunctionLSBReceived(byte e)
        {
            functionLSB = e;
            msgFunction += (ushort)(e << 0);
            actualState = State.PayloadLengthMSB;
            OnFunctionLSBByteReceivedEvent?.Invoke(this, new DecodeByteArgs(e));

        }
        public virtual void OnPayloadLenghtMSBReceided(byte e)
        {
            payloadLenghtMSB = e;
            msgPayloadLenght = (ushort)(e << 8);
            actualState = State.PayloadLengthLSB;
            OnPayloadLenghtMSBByteReceivedEvent?.Invoke(this, new DecodeByteArgs(e));
        }
        public virtual void OnPayloadLenghtLSBReceided(byte e)
        {
            payloadLenghtLSB = e;
            msgPayloadLenght += (ushort)(e << 0);
            actualState = State.Payload;
            
            if (msgPayloadLenght <= MAX_MSG_LENGHT)
            {
                msgPayloadIndex = 0;
                msgPayload = new byte[msgPayloadLenght];   
            } else
            {
                actualState = State.Waiting;
            }
            OnPayloadLenghtLSBByteReceivedEvent?.Invoke(this, new DecodeByteArgs(e));
        }
        public virtual void OnPayloadByteReceived(byte e)
        {
            msgPayload[msgPayloadIndex] = e;
            msgPayloadIndex++;
            if (msgPayloadIndex == msgPayloadLenght)
            {
                OnPayloadReceived(msgPayload);
            }
            OnPayloadByteReceivedEvent?.Invoke(this, new DecodeByteArgs(e));
        }
        public virtual void OnPayloadReceived(byte[] e)
        {
            actualState = State.CheckSum;
            OnPayloadReceivedEvent?.Invoke(this, new DecodePayloadArgs(e));
        }
        public virtual void OnCheckSumReceived(byte e)
        {
            msgChecksum = e;
            if (msgChecksum == CalculateChecksum())
            {
                OnCorrectChecksumReceived(); 
            } else
            {
                OnWrongChecksumReceived(); 
            }
            actualState = State.Waiting;
            OnChecksumByteReceivedEvent?.Invoke(this, new DecodeByteArgs(e));
        }
        public virtual void OnCorrectChecksumReceived()
        {
            OnCorrectChecksumEvent?.Invoke(this, new DecodeMsgArgs(msgFunction, msgPayloadLenght, msgPayload, msgChecksum));
        }
        public virtual void OnWrongChecksumReceived() 
        {
            OnWrongChecksumEvent?.Invoke(this, new DecodeMsgArgs(msgFunction, msgPayloadLenght, msgPayload, msgChecksum));
        }
        private static byte CalculateChecksum()
        {
            byte checksum = SOF;
            checksum ^= functionMSB;
            checksum ^= functionLSB;
            checksum ^= payloadLenghtMSB;
            checksum ^= payloadLenghtLSB;
            foreach (byte x in msgPayload)
            {
                checksum ^= x;
            }
            return checksum;
        }

        public class DecodeByteArgs : EventArgs
        {
            public byte b { get; set; }
            
            public DecodeByteArgs(byte b_a)
            {
                b = b_a;
            }
        }
        public class DecodePayloadArgs : EventArgs
        {
            public byte[] payload { get; set; }

            public DecodePayloadArgs(byte[] payload_a)
            {
                payload = payload_a;
            }
        }
        public class DecodeMsgArgs : EventArgs
        {
            public ushort msgFunction { get; set; }
            public ushort msgPayloadLenght { get; set; }
            public byte[] msgPayload { get; set; }
            public byte msgChecksum { get; set; }

            public DecodeMsgArgs(ushort msgFunction_a, ushort msgPayloadLenght_a,  byte[] msgPayload_a, byte msgChecksum_a)
            {
                msgFunction = msgFunction_a;
                msgPayloadLenght = msgPayloadLenght_a;
                msgPayload = msgPayload_a;
                msgChecksum = msgChecksum_a;
            }
        }
    }
}
