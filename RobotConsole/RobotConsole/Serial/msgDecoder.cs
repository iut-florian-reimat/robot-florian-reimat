using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventArgsLibrary;



namespace RobotConsole
{

    class MsgDecoder
    {
        public MsgDecoder()
        {
            OnMessageDecoderCreated();
        }

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
        
        static State actualState = State.Waiting;
        
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
                    if (b == Protocol.SOF)
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

        public event EventHandler<EventArgs> OnMessageDecoderCreatedEvent;
        public event EventHandler<DecodeByteArgs> OnSOFByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnUnknowByteEvent;
        public event EventHandler<DecodeByteArgs> OnFunctionMSBByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnFunctionLSBByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnPayloadLenghtMSBByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnPayloadLenghtLSBByteReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnPayloadByteReceivedEvent;
        public event EventHandler<DecodePayloadArgs> OnPayloadReceivedEvent;
        public event EventHandler<DecodeByteArgs> OnChecksumByteReceivedEvent;
        public event EventHandler<MessageByteArgs> OnCorrectChecksumEvent;
        public event EventHandler<MessageByteArgs> OnWrongChecksumEvent;
        public event EventHandler<EventArgs> OnOverLenghtMessageEvent;
        public event EventHandler<EventArgs> OnUnknowFunctionEvent;
        public event EventHandler<EventArgs> OnWrongLenghtFunctionEvent;

        public virtual void OnMessageDecoderCreated()
        {
            OnMessageDecoderCreatedEvent?.Invoke(this, new EventArgs());
        }
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
            OnFunctionLSBByteReceivedEvent?.Invoke(this, new DecodeByteArgs(e));
            if (Protocol.CheckFunctionLenght(msgFunction) != -2)
            {
                actualState = State.PayloadLengthMSB;
            } else
            {
                actualState = State.Waiting;
                OnUnknowFunction();
            }
            
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
            actualState = State.Waiting;
            OnPayloadLenghtLSBByteReceivedEvent?.Invoke(this, new DecodeByteArgs(e));
            if (msgPayloadLenght <= Protocol.MAX_MSG_LENGHT)
            {
                short allowedLenght = Protocol.CheckFunctionLenght(msgFunction);
                if (allowedLenght != -2)
                {
                    if (allowedLenght == -1 || allowedLenght == msgPayloadLenght)
                    {
                        actualState = State.Payload;
                        msgPayloadIndex = 0;
                        msgPayload = new byte[msgPayloadLenght];
                    } else
                    {
                        OnWrongLenghtFunction();
                    }
                } else
                {
                    OnUnknowFunction();
                }
                
            } else
            {
                OnOverLenghtMessage();
            }
            
        }

       
        
        public virtual void OnOverLenghtMessage()
        {
            OnOverLenghtMessageEvent?.Invoke(this,new EventArgs());
        }
        public virtual void OnUnknowFunction()
        {
            OnUnknowFunctionEvent?.Invoke(this, new EventArgs());
        }
        public virtual void OnWrongLenghtFunction()
        {
            OnWrongLenghtFunctionEvent?.Invoke(this, new EventArgs());
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
            OnCorrectChecksumEvent?.Invoke(this, new MessageByteArgs(msgFunction, msgPayloadLenght, msgPayload, msgChecksum));
        }
        public virtual void OnWrongChecksumReceived() 
        {
            OnWrongChecksumEvent?.Invoke(this, new MessageByteArgs(msgFunction, msgPayloadLenght, msgPayload, msgChecksum));
        }
        private static byte CalculateChecksum()
        {
            byte checksum = Protocol.SOF;
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
    }
}
