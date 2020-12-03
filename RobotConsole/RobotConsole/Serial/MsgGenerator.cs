using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotConsole
{
    class MsgGenerator
    {
        public MsgGenerator()
        {
            OnMessageGeneratorCreated();
        }
        public void GenerateMessageSetLed(ushort led_number, bool state)
        {
            byte[] msgPayload = new byte[] {(byte) led_number,(byte)(state?0x01:0x00) };
            Serial.msgEncoder.UartEncodeAndSendMessage((ushort) Protocol.FunctionName.SET_LED, msgPayload);
        }

        public void GenerateMessageSetMotorSpeed(sbyte left_motor_speed, sbyte right_motor_speed)
        {
            byte[] msgPayload = new byte[2];
            if (left_motor_speed <= 100 && left_motor_speed >= -100)
            {
                msgPayload[0] = (byte)left_motor_speed;
            } else {
                msgPayload[0] = (byte) ((left_motor_speed > 0) ? 100 : -100);
            }
            if (right_motor_speed <= 100 && right_motor_speed >= -100)
            {
                msgPayload[1] = (byte) right_motor_speed;
            }
            else
            {
                msgPayload[1] = (byte)((right_motor_speed > 0) ? 100 : -100);
            }
            Serial.msgEncoder.UartEncodeAndSendMessage((ushort)Protocol.FunctionName.SET_MOTOR, msgPayload);
        }

        public void GenerateMessageSetState(ushort state)
        {
            byte[] msgPayload = new byte[] { (byte) state};
            Serial.msgEncoder.UartEncodeAndSendMessage((ushort)Protocol.FunctionName.SET_STATE, msgPayload);
        }

        public void GenerateMessageSetResetPosition()
        {
            byte[] msgPayload = new byte[] { 0x00 };
            Serial.msgEncoder.UartEncodeAndSendMessage((ushort)Protocol.FunctionName.SET_RESET_POSITION, msgPayload);
        }

        public void GenerateMessageSetOdometryParam(double diameter, double distance)
        {
            
            byte[] diam = BitConverter.GetBytes(diameter);
            byte[] dist = BitConverter.GetBytes(distance);
            byte[] msgPayload = new byte[diam.Length + dist.Length];
            uint i = 0;
            for (i = 0; i < diam.Length; i++)
            {
                msgPayload[i] = diam[i];
            }
            for (i = 0; i < dist.Length; i++)
            {
                msgPayload[diam.Length + i] = dist[i];
            }
            Serial.msgEncoder.UartEncodeAndSendMessage((ushort)Protocol.FunctionName.SET_ODOMETRY_PARAM, msgPayload);
        }

        public void GenerateMessageSetOdometryLockY(bool isLocked)
        {
            byte[] msgPayload = new byte[] { (byte) ((isLocked)? 0x01 : 0x00) };
            Serial.msgEncoder.UartEncodeAndSendMessage((ushort)Protocol.FunctionName.LOCK_ODOMETRY_Y, msgPayload);
        }

        //public void GenerateMessageSetPosition(float GetPos)
        public event EventHandler<EventArgs> OnMessageGeneratorCreatedEvent;

        public virtual void OnMessageGeneratorCreated()
        {
            OnMessageGeneratorCreatedEvent?.Invoke(this, new EventArgs());
        }
    }
}
