//Partie PWM
#include <xc.h> // library xc.h inclut tous les uC
#include "IO.h"
#include "PWM.h"
#include "Robot.h"
#include "Utilities.h"

#define PWMPER 40.0
unsigned char acceleration = 20;

void InitPWM(void)
{
    PTCON2bits.PCLKDIV = 0b000; //Divide by 1
    PTPER = 100*PWMPER; //Période en pourcentage

    //Réglage PWM moteur 1 sur hacheur 1
    IOCON1bits.POLH = 1; //High = 1 and active on low =0
    IOCON1bits.POLL = 1; //High = 1
    IOCON1bits.PMOD = 0b01; //Set PWM Mode to Redundant
    FCLCON1 = 0x0003; //Désactive la gestion des faults

    //Reglage PWM moteur 2 sur hacheur 6
    IOCON6bits.POLH = 1; //High = 1
    IOCON6bits.POLL = 1; //High = 1
    IOCON6bits.PMOD = 0b01; //Set PWM Mode to Redundant
    FCLCON6 = 0x0003; //Désactive la gestion des faults

    /* Enable PWM Module */
    PTCONbits.PTEN = 1;
}

void PWMSetSpeed(float vitesseEnPourcents)
{
    robotState.vitesseGaucheCommandeCourante = vitesseEnPourcents;
    MOTEUR_GAUCHE_ENL = 0; //Pilotage de la pin en mode IO
    MOTEUR_GAUCHE_INL = 1; //Mise à 1 de la pin
    MOTEUR_GAUCHE_ENH = 1; //Pilotage de la pin en mode PWM
    MOTEUR_GAUCHE_DUTY_CYCLE = Abs(robotState.vitesseGaucheCommandeCourante*PWMPER);
    
    robotState.vitesseDroiteCommandeCourante = vitesseEnPourcents;
    MOTEUR_DROITE_ENL = 0; //Pilotage de la pin en mode IO
    MOTEUR_DROITE_INL = 1; //Mise à 1 de la pin
    MOTEUR_DROITE_ENH = 1; //Pilotage de la pin en mode PWM
    MOTEUR_DROITE_DUTY_CYCLE = Abs(robotState.vitesseDroiteCommandeCourante*PWMPER);
}