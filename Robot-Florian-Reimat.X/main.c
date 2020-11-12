#include "main.h"
#include <xc.h>
#include "ChipConfig.h"
#include "IO.h"
#include "timer.h"
#include "PWM.h"
#include "Robot.h"
#include "uart.h"
#include "msgGenerator.h"
#include "msgProcessor.h"
#include "msgDecoder.h"
#include "CB_RX1.h"
#include "QEI.h"

unsigned char stateRobot = STATE_ARRET;
signed char customLeftMotorSpeed = 0;
signed char customRightMotorSpeed = 0;

int main(void) {

    InitOscillator();
    InitIO();
    InitPWM();
    InitTimer1();
    InitTimer4();
    InitUART();
    InitQEI1();
    InitQEI2();
    // Boucle Principale
    while (1) {
        unsigned int i;
        for (i = 0; i < CB_RX1_GetDataSize(); i++) {
            unsigned char c = CB_RX1_Get();
            UartDecodeMessage(c);
        }
        //__delay32(1000000);
        // Test Bonjour        
        //unsigned char Bonjour[8] = {0x42,0x6F,0x6E,0x6A,0x6F,0x75,0x72,(unsigned char) timestamp};
        //GenerateTextMessage(Bonjour,8);
    } // fin main
}

void OperatingSystemLoop(void) {
    switch (stateRobot) {
        case STATE_CUSTOM:
            PWMSetSpeedConsigne(NORMAL_SPEED, RIGHT_MOTOR);
            PWMSetSpeedConsigne(NORMAL_SPEED, LEFT_MOTOR);
            break;
            
        case STATE_AVANCE:
            PWMSetSpeedConsigne(NORMAL_SPEED, RIGHT_MOTOR);
            PWMSetSpeedConsigne(NORMAL_SPEED, LEFT_MOTOR);
            break;

        case STATE_TOURNE_GAUCHE:
            PWMSetSpeedConsigne(TURNED_HIGH_SPEED, RIGHT_MOTOR);
            PWMSetSpeedConsigne(TURNED_LOW_SPEED, LEFT_MOTOR);
            break;
            
        case STATE_TOURNE_DROITE:
            PWMSetSpeedConsigne(TURNED_LOW_SPEED, RIGHT_MOTOR);
            PWMSetSpeedConsigne(TURNED_HIGH_SPEED, LEFT_MOTOR);
            break;
            
        case STATE_TOURNE_SUR_PLACE_GAUCHE:
            PWMSetSpeedConsigne(ARROUND_SPEED, RIGHT_MOTOR);
            PWMSetSpeedConsigne(- ARROUND_SPEED, LEFT_MOTOR);
            break;
            
        case STATE_TOURNE_SUR_PLACE_DROITE:
            PWMSetSpeedConsigne(- ARROUND_SPEED, RIGHT_MOTOR);
            PWMSetSpeedConsigne(ARROUND_SPEED, LEFT_MOTOR);
            break;
            
        case STATE_ARRET:
            PWMSetSpeedConsigne(0, RIGHT_MOTOR);
            PWMSetSpeedConsigne(0, LEFT_MOTOR);
            break;
            
        case STATE_RECULE:
            PWMSetSpeedConsigne(BACK_SPEED, RIGHT_MOTOR);
            PWMSetSpeedConsigne(BACK_SPEED, LEFT_MOTOR);
            break;
            
        case STATE_FAST:
            PWMSetSpeedConsigne(FAST_SPEED, RIGHT_MOTOR);
            PWMSetSpeedConsigne(FAST_SPEED, LEFT_MOTOR);
            break;
        
        case STATE_SLOW:
            PWMSetSpeedConsigne(SLOW_SPEED, RIGHT_MOTOR);
            PWMSetSpeedConsigne(SLOW_SPEED, LEFT_MOTOR);
            break;
            
        default:
            stateRobot = STATE_ARRET;
            break;
    }
}

void SetRobotState(unsigned char state) {
    stateRobot = state;
}

void SetCustomMotorSpeed(signed char LeftSpeed, signed char RightSpeed){
    GenerateStateMessage(STATE_CUSTOM);
    customLeftMotorSpeed    = LeftSpeed;
    customRightMotorSpeed   = RightSpeed;
}