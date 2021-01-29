//Partie PWM
#include <xc.h> // library xc.h inclut tous les uC
#include "IO.h"
#include "PWM.h"
#include "Robot.h"
#include "Utilities.h"
#include "timer.h"
#include "QEI.h"

#define PWMPER 40.0
float acceleration = 5;

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

void PWMUpdateSpeed() {
    // Cette fonction est appelée sur timer et permet de suivre des rampes d?accélération
    if (robotState.vitesseDroiteCommandeCourante < robotState.vitesseDroiteConsigne){
        robotState.vitesseDroiteCommandeCourante = Min(robotState.vitesseDroiteCommandeCourante + acceleration,robotState.vitesseDroiteConsigne);
    }
    if (robotState.vitesseDroiteCommandeCourante > robotState.vitesseDroiteConsigne){
        robotState.vitesseDroiteCommandeCourante = Max(robotState.vitesseDroiteCommandeCourante - acceleration,robotState.vitesseDroiteConsigne);
    }
    if (robotState.vitesseDroiteCommandeCourante < 0) {
        RIGHT_MOTOR_ENL = 0; //pilotage de la pin en mode IO
        RIGHT_MOTOR_INL = 1; //Mise à 1 de la pin
        RIGHT_MOTOR_ENH = 1; //Pilotage de la pin en mode PWM
    } else {
        RIGHT_MOTOR_ENH = 0; //pilotage de la pin en mode IO
        RIGHT_MOTOR_INH = 1; //Mise à 1 de la pin
        RIGHT_MOTOR_ENL = 1; //Pilotage de la pin en mode PWM
    }
    RIGHT_MOTOR_DUTY_CYCLE = Abs(robotState.vitesseDroiteCommandeCourante) * PWMPER;

    if (robotState.vitesseGaucheCommandeCourante < robotState.vitesseGaucheConsigne){
        robotState.vitesseGaucheCommandeCourante = Min(robotState.vitesseGaucheCommandeCourante + acceleration,robotState.vitesseGaucheConsigne);
    }
    if (robotState.vitesseGaucheCommandeCourante > robotState.vitesseGaucheConsigne){
        robotState.vitesseGaucheCommandeCourante = Max(robotState.vitesseGaucheCommandeCourante - acceleration,robotState.vitesseGaucheConsigne);
    }
    if (robotState.vitesseGaucheCommandeCourante > 0) {
        LEFT_MOTOR_ENL = 0; //pilotage de la pin en mode IO
        LEFT_MOTOR_INL = 1; //Mise à 1 de la pin
        LEFT_MOTOR_ENH = 1; //Pilotage de la pin en mode PWM
    } else {
        LEFT_MOTOR_ENH = 0; //pilotage de la pin en mode IO
        LEFT_MOTOR_INH = 1; //Mise à 1 de la pin
        LEFT_MOTOR_ENL = 1; //Pilotage de la pin en mode PWM
    }
    LEFT_MOTOR_DUTY_CYCLE = Abs(robotState.vitesseGaucheCommandeCourante) * PWMPER;
}

void PWMSetSpeedConsigne(float vitesseEnPourcents, char moteur){
    if (moteur == LEFT_MOTOR) {
        robotState.vitesseGaucheConsigne = vitesseEnPourcents;
    } else {
        robotState.vitesseDroiteConsigne = vitesseEnPourcents;
    }
}



void PWMSetSpeedConsignePolaire() {
    
    // Correction Angulaire
    robotState.vitesseAngulaireErreur = robotState.vitesseAngulaireConsigne - robotState.vitesseAngulaireFromOdometry * 0;
    robotState.CorrectionAngulaireKp = robotState.KpAngulaire * robotState.vitesseAngulaireErreur;
    // robotState.CorrectionAngulaireKp = LimitToInterval(robotState.CorrectionAngulaireKp,- robotState.KpAngulaireMax, robotState.KpAngulaireMax);
    robotState.CorrectionAngulaireKi =  (robotState.KiAngulaire * robotState.vitesseAngulaireErreur)/FREQ_ECH_QEI + robotState.CorrectionAngulaireKi;
    // robotState.CorrectionAngulaireKi = LimitToInterval(robotState.CorrectionAngulaireKi,-robotState.KiAngulaireMax, robotState.KiAngulaireMax);
    
    robotState.vitesseAngulaireCorrection = robotState.CorrectionAngulaireKp + robotState.CorrectionAngulaireKi;
    robotState.vitesseAngulaireCommande = robotState.vitesseAngulaireCorrection * COEFF_VITESSE_ANGULAIRE_PERCENT;
    
    // Correction Lineaire
    robotState.vitesseLineaireErreur = robotState.vitesseLineaireConsigne - robotState.vitesseLineaireFromOdometry * 0;
    robotState.CorrectionLineaireKp = robotState.KpLineaire * robotState.vitesseLineaireErreur;
    // robotState.CorrectionLineaireKp = LimitToInterval(robotState.CorrectionLineaireKp,- robotState.KpLineaireMax, robotState.KpLineaireMax);
    robotState.CorrectionLineaireKi =  (robotState.KiLineaire * robotState.vitesseLineaireErreur)/FREQ_ECH_QEI + robotState.CorrectionLineaireKi;
    // robotState.CorrectionLineaireKi = LimitToInterval(robotState.CorrectionLineaireKi,-robotState.KiLineaireMax, robotState.KiLineaireMax);
    
    robotState.vitesseLineaireCorrection = robotState.CorrectionLineaireKp + robotState.CorrectionLineaireKi;
    robotState.vitesseLineaireCommande = robotState.vitesseLineaireCorrection * COEFF_VITESSE_LINEAIRE_PERCENT;
        
    
    // Generation des consignes droite et gauche
    robotState.vitesseDroiteConsigne = robotState.vitesseLineaireCommande + robotState.vitesseAngulaireCommande * DISTROUES / 2;
    robotState.vitesseDroiteConsigne = LimitToInterval(robotState.vitesseDroiteConsigne, -100, 100);
    robotState.vitesseGaucheConsigne = robotState.vitesseLineaireCommande - robotState.vitesseAngulaireCommande * DISTROUES / 2;
    robotState.vitesseGaucheConsigne = LimitToInterval(robotState.vitesseGaucheConsigne, -100, 100);
}
