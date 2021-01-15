#include "QEI.h"
#include "main.h"
#include "Robot.h"
#include "Utilities.h"
#include "msgGenerator.h"
#include <xc.h>
#include <math.h>
#include "timer.h"
#include "IO.h"

void InitQEI1() {
    QEI1IOCbits.SWPAB = 1; //QEAx and QEBx are swapped
    QEI1GECL = 0xFFFF;
    QEI1GECH = 0xFFFF;
    QEI1CONbits.QEIEN = 1; // Enable QEI Module
}

void InitQEI2() {
    QEI2IOCbits.SWPAB = 1; //QEAx and QEBx are not swapped
    QEI2GECL = 0xFFFF;
    QEI2GECH = 0xFFFF;
    QEI2CONbits.QEIEN = 1; // Enable QEI Module
}

double QeiDroitPosition_T_1, QeiGauchePosition_T_1, delta_d, delta_g, delta_theta, dx;

void QEIUpdateData() {
    //On sauvegarde les anciennes valeurs
    QeiDroitPosition_T_1 = robotState.distanceDroitFromOdometry;
    QeiGauchePosition_T_1 = robotState.distanceGaucheFromOdometry;
    
    //On reactualise les valeurs des positions
    long QEI1RawValue = POS1CNTL;
    QEI1RawValue += ((long) POS1HLD << 16);

    long QEI2RawValue = POS2CNTL;
    QEI2RawValue += ((long) POS2HLD << 16);
    
    // Conversion en m ( regle pour la taille des roues codeuses)
    robotState.distanceDroitFromOdometry    =   POINT_TO_METER * QEI1RawValue;
    robotState.distanceGaucheFromOdometry   = - POINT_TO_METER * QEI2RawValue;
    
    /*
    if((robotState.distanceDroitFromOdometry - QeiDroitPosition_T_1 > 100)    || (robotState.distanceDroitFromOdometry - QeiDroitPosition_T_1 < -100))
        robotState.distanceDroitFromOdometry = QeiDroitPosition_T_1;
    if((robotState.distanceGaucheFromOdometry - QeiGauchePosition_T_1 > 100)  || (robotState.distanceGaucheFromOdometry - QeiGauchePosition_T_1 < -100))
        robotState.distanceGaucheFromOdometry = QeiGauchePosition_T_1;
    */
    
    // Calcul des deltas de position
    delta_d = robotState.distanceDroitFromOdometry  - QeiDroitPosition_T_1;
    delta_g = robotState.distanceGaucheFromOdometry - QeiGauchePosition_T_1;
    
    // delta_theta = atan((delta_d - delta_g) / DISTROUES);
    delta_theta = (delta_d - delta_g) / DISTROUES;
    dx = (delta_d + delta_g) / 2;
    
    // Calcul des vitesses
    // attention a remultiplier par la  frequence d'echantillonnage
    robotState.vitesseDroitFromOdometry = delta_d * FREQ_ECH_QEI;
    robotState.vitesseGaucheFromOdometry = delta_g * FREQ_ECH_QEI;
    robotState.vitesseLineaireFromOdometry = dx * FREQ_ECH_QEI;//(robotState.vitesseDroitFromOdometry + robotState.vitesseGaucheFromOdometry) / 2;
    robotState.vitesseAngulaireFromOdometry = delta_theta * FREQ_ECH_QEI;

    //Mise à jour du positionnement terrain a t ? 1
    robotState.xPosFromOdometry_1 = robotState.xPosFromOdometry;
    robotState.yPosFromOdometry_1 = robotState.yPosFromOdometry;
    robotState.angleRadianFromOdometry_1 = robotState.angleRadianFromOdometry;
    
    // Calcul des positions dans le referentiel du terrain
    robotState.xPosFromOdometry = robotState.xPosFromOdometry_1 + robotState.vitesseLineaireFromOdometry / FREQ_ECH_QEI * cos(robotState.angleRadianFromOdometry_1);
    robotState.yPosFromOdometry = robotState.yPosFromOdometry_1 + robotState.vitesseLineaireFromOdometry / FREQ_ECH_QEI * sin(robotState.angleRadianFromOdometry_1);
    robotState.angleRadianFromOdometry = robotState.angleRadianFromOdometry_1 + delta_theta;
    if (robotState.angleRadianFromOdometry > PI)
        robotState.angleRadianFromOdometry -= 2 * PI;
    if (robotState.angleRadianFromOdometry < -PI)
        robotState.angleRadianFromOdometry += 2 * PI;
}

void QEIReset() {
    WHITE_LED = !WHITE_LED;    
    QEISetPosition(1.45,0.95,0);
}

void QEISetPosition(float x, float y, float theta) {
    robotState.xPosFromOdometry = x;
    robotState.yPosFromOdometry = y;
    robotState.angleRadianFromOdometry = theta;
}

