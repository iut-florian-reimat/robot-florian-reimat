#ifndef MAIN_H

#define	MAIN_H
#define FCY 40000000

#define STATE_CUSTOM 0
#define STATE_AVANCE 1
#define STATE_TOURNE_GAUCHE 2
#define STATE_TOURNE_DROITE 3
#define STATE_TOURNE_SUR_PLACE_GAUCHE 4
#define STATE_TOURNE_SUR_PLACE_DROITE 5
#define STATE_ARRET 6
#define STATE_RECULE 7
#define STATE_FAST 8
#define STATE_SLOW 9


#define BACK_SPEED -20
#define NORMAL_SPEED 30

#define ARROUND_SPEED 15
#define TURNED_HIGH_SPEED 30
#define TURNED_LOW_SPEED 5
#define FAST_SPEED 80
#define SLOW_SPEED 15


void OperatingSystemLoop(void);
void SetNextRobotStateInAutomaticMode();
void SetRobotState(unsigned char);
void SetCustomMotorSpeed(signed char, signed char);
#endif
