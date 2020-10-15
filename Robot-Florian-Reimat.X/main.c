#include "main.h"
#include <stdio.h>
#include <stdlib.h>
#include <xc.h>
#include <libpic30.h>
#include "ChipConfig.h"
#include "IO.h"
#include "timer.h"
#include "PWM.h"
#include "Robot.h"
#include "ADC.h"
#include "uart.h"
#include "CB_TX1.h"
#include "CB_RX1.h"
#include "UART_Protocol.h"

unsigned char stateRobot = STATE_ARRET;
signed char customLeftMotorSpeed = 0;
signed char customRightMotorSpeed = 0;

int main(void) {

    InitOscillator();
    InitIO();
    InitPWM();
    InitTimer1();
    InitTimer4();
    InitADC1();
    InitUART();
    // Boucle Principale
    while (1) {
        int i;
        ADCConversionLoop();

        for (i = 0; i < CB_RX1_GetDataSize(); i++) {
            unsigned char c = CB_RX1_Get();
            UartDecodeMessage(c);
        }
        //__delay32(40000000);

        // Test Bonjour        
        //unsigned char Bonjour[7] = {0x42,0x6F,0x6E,0x6A,0x6F,0x75,0x72};
        //UartEncodeAndSendMessage(128, 7,Bonjour);
    } // fin main
}
void OperatingSystemLoop(void) {
    switch (stateRobot) {
        case STATE_CUSTOM:
            PWMSetSpeedConsigne(NORMAL_SPEED, MOTEUR_DROIT);
            PWMSetSpeedConsigne(NORMAL_SPEED, MOTEUR_GAUCHE);
            break;
            
        case STATE_AVANCE:
            PWMSetSpeedConsigne(NORMAL_SPEED, MOTEUR_DROIT);
            PWMSetSpeedConsigne(NORMAL_SPEED, MOTEUR_GAUCHE);
            break;

        case STATE_TOURNE_GAUCHE:
            PWMSetSpeedConsigne(TURNED_HIGH_SPEED, MOTEUR_DROIT);
            PWMSetSpeedConsigne(TURNED_LOW_SPEED, MOTEUR_GAUCHE);
            break;
            
        case STATE_TOURNE_DROITE:
            PWMSetSpeedConsigne(TURNED_LOW_SPEED, MOTEUR_DROIT);
            PWMSetSpeedConsigne(TURNED_HIGH_SPEED, MOTEUR_GAUCHE);
            break;
            
        case STATE_TOURNE_SUR_PLACE_GAUCHE:
            PWMSetSpeedConsigne(ARROUND_SPEED, MOTEUR_DROIT);
            PWMSetSpeedConsigne(- ARROUND_SPEED, MOTEUR_GAUCHE);
            break;
            
        case STATE_TOURNE_SUR_PLACE_DROITE:
            PWMSetSpeedConsigne(- ARROUND_SPEED, MOTEUR_DROIT);
            PWMSetSpeedConsigne(ARROUND_SPEED, MOTEUR_GAUCHE);
            break;
            
        case STATE_ARRET:
            PWMSetSpeedConsigne(0, MOTEUR_DROIT);
            PWMSetSpeedConsigne(0, MOTEUR_GAUCHE);
            break;
            
        case STATE_RECULE:
            PWMSetSpeedConsigne(BACK_SPEED, MOTEUR_DROIT);
            PWMSetSpeedConsigne(BACK_SPEED, MOTEUR_GAUCHE);
            break;
            
        case STATE_FAST:
            PWMSetSpeedConsigne(FAST_SPEED, MOTEUR_DROIT);
            PWMSetSpeedConsigne(FAST_SPEED, MOTEUR_GAUCHE);
            break;
        
        case STATE_SLOW:
            PWMSetSpeedConsigne(SLOW_SPEED, MOTEUR_DROIT);
            PWMSetSpeedConsigne(SLOW_SPEED, MOTEUR_GAUCHE);
            break;
            
        default:
            stateRobot = STATE_ARRET;
            break;
    }
}

void SetRobotState(unsigned char msg) {
    stateRobot = (int) msg;
    unsigned char payload[5] = {stateRobot, timestamp >> 24, timestamp >> 16, timestamp >> 8, timestamp >> 0};
    UartEncodeAndSendMessage(SEND_ROBOT_STATE, 5, payload);
}

void SetCustomMotorSpeed(signed char LeftSpeed, signed char RightSpeed){
    SetRobotState(STATE_CUSTOM);
    customLeftMotorSpeed    = LeftSpeed;
    customRightMotorSpeed   = RightSpeed;
}