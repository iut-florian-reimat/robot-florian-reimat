using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventArgsLibrary;

namespace RobotConsole
{
    class MsgProcessor
    {
        public MsgProcessor()
        {
            OnMessageProcessorCreated();
        }

        public void MessageProcessor(object sender, MessageByteArgs e)
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
                case (ushort)Protocol.FunctionName.GET_ASSERV_POLAR_PARAM:
                    OnPolarAsservMessageReceived(e);
                    break;
                default:
                    OnUnknowFunctionReceived(e);
                    break;
            }
        }

        #region Event
        public event EventHandler<EventArgs> OnMessageProcessorCreatedEvent;
        public event EventHandler<IRMessageArgs> OnIRMessageReceivedEvent;
        public event EventHandler<StateMessageArgs> OnStateMessageReceivedEvent;
        public event EventHandler<PositionMessageArgs> OnPositionMessageReceivedEvent;
        public event EventHandler<TextMessageArgs> OnTextMessageReceivedEvent;
        public event EventHandler<PolarAsservMessageArgs> OnPolarAsservMessageReceivedEvent;
        public event EventHandler<MessageByteArgs> OnUnknowFunctionReceivedEvent;
        

        public virtual void OnMessageProcessorCreated()
        {
            OnMessageProcessorCreatedEvent?.Invoke(this, new EventArgs());
        }

        public virtual void OnIRMessageReceived(MessageByteArgs e)
        {
            
            OnIRMessageReceivedEvent?.Invoke(this, new IRMessageArgs(e.msgPayload[0], e.msgPayload[1], e.msgPayload[2])); 
        }

        public virtual void OnStateMessageReceived(MessageByteArgs e)
        {
            uint time = (((uint) e.msgPayload[1]) << 24) + (((uint)e.msgPayload[2]) << 16) + (((uint)e.msgPayload[3]) << 8) + ((uint)e.msgPayload[4] << 0);
            OnStateMessageReceivedEvent?.Invoke(this, new StateMessageArgs(e.msgPayload[0], time));
        }

        public virtual void OnPositionMessageReceived(MessageByteArgs e)
        {
            uint time = (uint) BitConverter.ToInt32(e.msgPayload, 0);
            float x = BitConverter.ToSingle(e.msgPayload, 4);
            float y = BitConverter.ToSingle(e.msgPayload, 8);
            float theta = BitConverter.ToSingle(e.msgPayload, 12);
            float linearSpeed = BitConverter.ToSingle(e.msgPayload, 16);
            float angularSpeed = BitConverter.ToSingle(e.msgPayload, 20);


            // theta = (float) (theta * 180 / Math.PI);
            OnPositionMessageReceivedEvent?.Invoke(this, new PositionMessageArgs(time, x, y, theta, linearSpeed, angularSpeed));
        }

        public virtual void OnTextMessageReceived(MessageByteArgs e)
        {
            string text = Encoding.UTF8.GetString(e.msgPayload, 0, e.msgPayload.Length);
            OnTextMessageReceivedEvent?.Invoke(this, new TextMessageArgs(text));
        }

        public virtual void OnPolarAsservMessageReceived(MessageByteArgs e)
        {
            // Data
            float Measure_LinearSpeed   = BitConverter.ToSingle(e.msgPayload, 4 * 00);
            float Measure_AngularSpeed  = BitConverter.ToSingle(e.msgPayload, 4 * 01);
            float Command_LinearSpeed   = BitConverter.ToSingle(e.msgPayload, 4 * 02);
            float Command_AngularSpeed  = BitConverter.ToSingle(e.msgPayload, 4 * 03);
            float Error_LinearSpeed     = BitConverter.ToSingle(e.msgPayload, 4 * 04);
            float Error_AngularSpeed    = BitConverter.ToSingle(e.msgPayload, 4 * 05);
            float Order_LinearSpeed     = BitConverter.ToSingle(e.msgPayload, 4 * 06);
            float Order_AngularSpeed    = BitConverter.ToSingle(e.msgPayload, 4 * 07);

            // Kp
            float Kp_Linear             = BitConverter.ToSingle(e.msgPayload, 4 * 08);
            float Kp_Angular            = BitConverter.ToSingle(e.msgPayload, 4 * 09);
            float Correction_P_Linear   = BitConverter.ToSingle(e.msgPayload, 4 * 10);
            float Correction_P_Angular  = BitConverter.ToSingle(e.msgPayload, 4 * 11);
            float Kp_Max_Linear         = BitConverter.ToSingle(e.msgPayload, 4 * 12);
            float Kp_Max_Angular        = BitConverter.ToSingle(e.msgPayload, 4 * 13);

            // Ki
            float Ki_Linear             = BitConverter.ToSingle(e.msgPayload, 4 * 14);
            float Ki_Angular            = BitConverter.ToSingle(e.msgPayload, 4 * 15);
            float Correction_I_Linear   = BitConverter.ToSingle(e.msgPayload, 4 * 16);
            float Correction_I_Angular  = BitConverter.ToSingle(e.msgPayload, 4 * 17);
            float Ki_Max_Linear         = BitConverter.ToSingle(e.msgPayload, 4 * 18);
            float Ki_Max_Angular        = BitConverter.ToSingle(e.msgPayload, 4 * 19);

            // Kd
            float Kd_Linear             = BitConverter.ToSingle(e.msgPayload, 4 * 20);
            float Kd_Angular            = BitConverter.ToSingle(e.msgPayload, 4 * 21);
            float Correction_D_Linear   = BitConverter.ToSingle(e.msgPayload, 4 * 22);
            float Correction_D_Angular  = BitConverter.ToSingle(e.msgPayload, 4 * 23);
            float Kd_Max_Linear         = BitConverter.ToSingle(e.msgPayload, 4 * 24);
            float Kd_Max_Angular        = BitConverter.ToSingle(e.msgPayload, 4 * 25);

            PolarAsservMessageArgs Polar =
            new PolarAsservMessageArgs( Measure_LinearSpeed, Measure_AngularSpeed, 
            Command_LinearSpeed, Command_AngularSpeed, Error_LinearSpeed, Error_AngularSpeed,
            Order_LinearSpeed, Order_AngularSpeed, Kp_Linear, Kp_Angular, Correction_P_Linear,
            Correction_P_Angular, Kp_Max_Linear, Kp_Max_Angular, Ki_Linear, Ki_Angular,
            Correction_I_Linear, Correction_I_Angular, Ki_Max_Linear, Ki_Max_Angular, Kd_Linear,
            Kd_Angular, Correction_D_Linear, Correction_D_Angular, Kd_Max_Linear, Kd_Max_Angular);
            OnPolarAsservMessageReceivedEvent?.Invoke(this, Polar);
        }

        public virtual void OnUnknowFunctionReceived(MessageByteArgs e)
        {
            OnUnknowFunctionReceivedEvent?.Invoke(this, e);
        }
        #endregion
    }
}
