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
                default:
                    OnUnknowFunctionReceived(e);
                    break;
            }
        }

        public event EventHandler<EventArgs> OnMessageProcessorCreatedEvent;
        public event EventHandler<Protocol.IRMessageArgs> OnIRMessageReceivedEvent;
        public event EventHandler<Protocol.StateMessageArgs> OnStateMessageReceivedEvent;
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

        public virtual void OnUnknowFunctionReceived(Protocol.MessageByteArgs e)
        {
            OnUnknowFunctionReceivedEvent?.Invoke(this, e);
        }
    }
}
