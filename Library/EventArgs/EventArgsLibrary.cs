using System;

namespace EventArgsLibrary
{
    public class MessageByteArgs : EventArgs
    {
        public byte SOF { get; set; }
        public byte functionMsb { get; set; }
        public byte functionLsb { get; set; }
        public byte lenghtMsb { get; set; }
        public byte lenghtLsb { get; set; }
        public byte checksum { get; set; }
        public ushort msgFunction { get; set; }
        public ushort msgPayloadLenght { get; set; }
        public byte[] msgPayload { get; set; }

        public MessageByteArgs(byte SOF_a, byte functionMsb_a, byte functionLsb_a, byte lenghtMsb_a, byte lenghtLsb_a, byte[] msgPaylaod_a, byte checksum_a)
        {
            SOF = SOF_a;
            functionMsb = functionMsb_a;
            functionLsb = functionLsb_a;
            lenghtMsb = lenghtMsb_a;
            lenghtLsb = lenghtLsb_a;
            msgPayload = msgPaylaod_a;
            checksum = checksum_a;
            ConvertByteToFunction();
        }

        public MessageByteArgs(ushort msgFunction_a, ushort msgPayloadLenght_a, byte[] msgPayload_a, byte checksum_a)
        {
            msgFunction = msgFunction_a;
            msgPayloadLenght = msgPayloadLenght_a;
            msgPayload = msgPayload_a;
            checksum = checksum_a;
            ConvertFunctionToByte();
        }
        private void ConvertByteToFunction()
        {
            msgFunction = (ushort)(functionMsb << 8 + functionLsb << 0);
            msgPayloadLenght = (ushort)(lenghtMsb << 8 + lenghtLsb << 0);
        }

        private void ConvertFunctionToByte()
        {
            functionLsb = (byte)(msgFunction >> 0);
            functionMsb = (byte)(msgFunction >> 8);

            lenghtLsb = (byte)(msgPayloadLenght >> 0);
            lenghtMsb = (byte)(msgPayloadLenght >> 8);
        }
    }
    public class LedMessageArgs : EventArgs
    {
        public ushort led_number { get; set; }
        public bool state { get; set; }

        public LedMessageArgs(ushort led_number_a, bool state_a)
        {
            led_number = led_number_a;
            state = state_a;
        }
    }
    public class MotorMessageArgs : EventArgs
    {
        public sbyte left_speed { get; set; }
        public sbyte right_speed { get; set; }

        public MotorMessageArgs(sbyte left_speed_a, sbyte right_speed_a)
        {
            left_speed = left_speed_a;
            right_speed = right_speed_a;
        }
    }
    public class StateMessageArgs : EventArgs
    {
        public ushort state { get; set; }
        public uint time { get; set; }
        public StateMessageArgs(ushort state_a)
        {
            state = state_a;
        }

        public StateMessageArgs(ushort state_a, uint time_a)
        {
            state = state_a;
            time = time_a;
        }
    }
    public class IRMessageArgs : EventArgs
    {
        public ushort left_IR { get; set; }
        public ushort center_IR { get; set; }
        public ushort right_IR { get; set; }

        public IRMessageArgs(ushort left, ushort center, ushort right)
        {
            left_IR = left;
            center_IR = center;
            right_IR = right;
        }
    }
    public class PositionMessageArgs : EventArgs
    {
        public uint timestamp { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float theta { get; set; }
        public float linearSpeed { get; set; }
        public float angularSpeed { get; set; }

        public PositionMessageArgs(uint time, float x_a, float y_a, float theta_a, float linear, float angular)
        {
            timestamp = time;
            x = x_a;
            y = y_a;
            theta = theta_a;
            linearSpeed = linear;
            angularSpeed = angular;
        }
    }
    public class TextMessageArgs : EventArgs
    {
        public string text { get; set; }
        public TextMessageArgs(string text_a)
        {
            text = text_a;
        }
    }
    public class PolarAsservMessageArgs
    {
        public float Measure_LinearSpeed = 0, Measure_AngularSpeed = 0;
        public float Command_LinearSpeed = 0, Command_AngularSpeed = 0;
        public float Error_LinearSpeed = 0, Error_AngularSpeed = 0;
        public float Order_LinearSpeed = 0, Order_AngularSpeed = 0;

        public float Kp_Linear = 0, Ki_Linear = 0, Kd_Linear = 0;
        public float Correction_P_Linear = 0, Correction_I_Linear = 0, Correction_D_Linear = 0;
        public float Kp_Max_Linear = 0, Ki_Max_Linear = 0, Kd_Max_Linear = 0;

        public float Kp_Angular = 0, Ki_Angular = 0, Kd_Angular = 0;
        public float Correction_P_Angular = 0, Correction_I_Angular = 0, Correction_D_Angular = 0;
        public float Kp_Max_Angular = 0, Ki_Max_Angular = 0, Kd_Max_Angular = 0;

        public PolarAsservMessageArgs(float M_Linear, float M_Angular,
            float C_Linear, float C_Angular, float E_Linear, float E_Angular,
            float O_Linear, float O_Angular, float P_Linear, float P_Angular,
            float CP_Linear, float CP_Angular, float MP_Linear, float MP_Angular,
            float I_Linear, float I_Angular, float CI_Linear, float CI_Angular,
            float MI_Linear, float MI_Angular, float D_Linear, float D_Angular,
            float CD_Linear, float CD_Angular, float MD_Linear, float MD_Angular)
        {
            // Data
            Measure_LinearSpeed = M_Linear;
            Measure_AngularSpeed = M_Angular;
            Command_LinearSpeed = C_Linear;
            Command_AngularSpeed = C_Angular;
            Error_LinearSpeed = E_Linear;
            Error_AngularSpeed = E_Angular;
            Order_LinearSpeed = O_Linear;
            Order_AngularSpeed = O_Angular;


            // Kp
            Kp_Linear = P_Linear;
            Kp_Angular = P_Angular;
            Correction_P_Linear = CP_Linear;
            Correction_P_Angular = CP_Angular;
            Kp_Max_Linear = MP_Linear;
            Kp_Max_Angular = MP_Angular;

            // Ki
            Ki_Linear = I_Linear;
            Ki_Angular = I_Angular;
            Correction_I_Linear = CI_Linear;
            Correction_I_Angular = CI_Angular;
            Ki_Max_Linear = MI_Linear;
            Ki_Max_Angular = MI_Angular;

            // Kd
            Kd_Linear = D_Linear;
            Kd_Angular = D_Angular;
            Correction_D_Linear = CD_Linear;
            Correction_D_Angular = CD_Angular;
            Kd_Max_Linear = MD_Linear;
            Kd_Max_Angular = MD_Angular;

        }

        public PolarAsservMessageArgs()
        {
            // Data
            Measure_LinearSpeed     = 0;
            Measure_AngularSpeed    = 0;
            Command_LinearSpeed     = 0;
            Command_AngularSpeed    = 0;
            Error_LinearSpeed       = 0;
            Error_AngularSpeed      = 0;
            Order_LinearSpeed       = 0;
            Order_AngularSpeed      = 0;


            // Kp
            Kp_Linear               = 0;
            Kp_Angular              = 0;
            Correction_P_Linear     = 0;
            Correction_P_Angular    = 0;
            Kp_Max_Linear           = 0;
            Kp_Max_Angular          = 0;

            // Ki
            Ki_Linear               = 0;
            Ki_Angular              = 0;
            Correction_I_Linear     = 0;
            Correction_I_Angular    = 0;
            Ki_Max_Linear           = 0;
            Ki_Max_Angular          = 0;

            // Kd
            Kd_Linear               = 0;
            Kd_Angular              = 0;
            Correction_D_Linear     = 0;
            Correction_D_Angular    = 0;
            Kd_Max_Linear           = 0;
            Kd_Max_Angular          = 0;
        }
    }
}
