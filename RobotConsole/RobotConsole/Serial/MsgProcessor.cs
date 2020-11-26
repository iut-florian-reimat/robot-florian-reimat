using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotConsole
{
    class MsgProcessor
    {
        public MsgProcessor()
        {
            OnMessageProcessorCreated();
        }

        public void MessageProcessor(object sender, Protocol.MessageByteArgs e)
        {
            switch (e.msgFunction)
            {
                case (ushort)Protocol.FunctionName.GET_IR:
                    OnIRMessageReceived(e);
                    break;

                case (ushort)Protocol.FunctionName.GET_STATE:
                    OnStateMessageReceived(e);
                    break;
                case (ushort)Protocol.FunctionName.GET_POSITION:
                    OnPositionMessageReceived(e);
                    break;
                case (ushort)Protocol.FunctionName.GET_TEXT:
                    OnTextMessageReceived(e);
                    break;
                default:
                    OnUnknowFunctionReceived(e);
                    break;
            }
        }

        #region Event
        public event EventHandler<EventArgs> OnMessageProcessorCreatedEvent;
        public event EventHandler<Protocol.IRMessageArgs> OnIRMessageReceivedEvent;
        public event EventHandler<Protocol.StateMessageArgs> OnStateMessageReceivedEvent;
        public event EventHandler<Protocol.PositionMessageArgs> OnPositionMessageReceivedEvent;
        public event EventHandler<Protocol.TextMessageArgs> OnTextMessageReceivedEvent;
        public event EventHandler<Protocol.MessageByteArgs> OnUnknowFunctionReceivedEvent;

        public virtual void OnMessageProcessorCreated()
        {
            OnMessageProcessorCreatedEvent?.Invoke(this, new EventArgs());
        }

        public virtual void OnIRMessageReceived(Protocol.MessageByteArgs e)
        {
            
            OnIRMessageReceivedEvent?.Invoke(this, new Protocol.IRMessageArgs(e.msgPayload[0], e.msgPayload[1], e.msgPayload[2])); 
        }

        public virtual void OnStateMessageReceived(Protocol.MessageByteArgs e)
        {
            uint time = (((uint) e.msgPayload[1]) << 24) + (((uint)e.msgPayload[2]) << 16) + (((uint)e.msgPayload[3]) << 8) + ((uint)e.msgPayload[4] << 0);
            OnStateMessageReceivedEvent?.Invoke(this, new Protocol.StateMessageArgs(e.msgPayload[0], time));
        }

        public virtual void OnPositionMessageReceived(Protocol.MessageByteArgs e)
        {
            uint time = (uint) BitConverter.ToInt32(e.msgPayload, 0);
            float x = BitConverter.ToSingle(e.msgPayload, 4);
            float y = BitConverter.ToSingle(e.msgPayload, 8);
            float theta = BitConverter.ToSingle(e.msgPayload, 12);
            float linearSpeed = BitConverter.ToSingle(e.msgPayload, 16);
            float angularSpeed = BitConverter.ToSingle(e.msgPayload, 20);


            theta = (float) (theta * 180 / Math.PI);
            OnPositionMessageReceivedEvent?.Invoke(this, new Protocol.PositionMessageArgs(time, x, y, theta, linearSpeed, angularSpeed));
        }

        public virtual void OnTextMessageReceived(Protocol.MessageByteArgs e)
        {
            string text = Encoding.UTF8.GetString(e.msgPayload, 0, e.msgPayload.Length);
            OnTextMessageReceivedEvent?.Invoke(this, new Protocol.TextMessageArgs(text));
        }

        public virtual void OnUnknowFunctionReceived(Protocol.MessageByteArgs e)
        {
            OnUnknowFunctionReceivedEvent?.Invoke(this, e);
        }
        #endregion
    }
}
