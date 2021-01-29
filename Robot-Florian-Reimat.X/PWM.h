/* 
 * File:   PWM.h
 * Author: TP-EO-1
 *
 * Created on 7 septembre 2020, 10:24
 */

#ifndef PWM_H
#define	PWM_H

#define COEFF_VITESSE_LINEAIRE_PERCENT 20.
#define COEFF_VITESSE_ANGULAIRE_PERCENT 31.

void InitPWM(void);
//void PWMSetSpeed(float,unsigned char);
void PWMUpdateSpeed();
void PWMSetSpeedConsigne(float, char);
void PWMSetSpeedConsignePolaire();
#endif	/* PWM_H */