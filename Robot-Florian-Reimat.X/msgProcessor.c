#include "xc.h"
#include "Protocol.h"
#include "msgProcessor.h"
#include "main.h"
#include "IO.h"

void UartProcessDecodedMessage(unsigned char function , unsigned char* payload) {
    switch (function) {
        case GET_LED_STATE:
            switch(payload[0]) {
                case 0x01:
                    ORANGE_LED = (payload[1] == 0x01)?1:0;
                    break;
                case 0x02:
                    BLUE_LED = (payload[1] == 0x01)?1:0;
                    break;
                case 0x03:
                    WHITE_LED = (payload[1] == 0x01)?1:0;
                    break;
            }
            break;
        case GET_MOTOR_SPEED:
            SetCustomMotorSpeed(payload[0], payload[1]);
            SetRobotState(STATE_CUSTOM);
            break;
        case GET_ROBOT_STATE:
            SetRobotState(payload[0]);
            break;
        
    }
}