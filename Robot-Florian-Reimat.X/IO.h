#ifndef IO_H
#define IO_H

//Affectation des pins des LEDS
#define ORANGE_LED  _LATC10 
#define BLUE_LED    _LATG7
#define WHITE_LED   _LATG6

// Definitions des pins pour les hacheurs moteurs
#define MOTOR1_IN1 _LATB14
#define MOTOR1_IN2 _LATB15

#define MOTOR6_IN1 _LATC6
#define MOTOR6_IN2 _LATC7 

// Configuration  specifique du moteur gauche
#define LEFT_MOTOR_INH MOTOR1_IN1
#define LEFT_MOTOR_INL MOTOR1_IN2
#define LEFT_MOTOR_ENL IOCON1bits.PENL
#define LEFT_MOTOR_ENH IOCON1bits.PENH
#define LEFT_MOTOR_DUTY_CYCLE PDC1

// Configuration  specifique du moteur droit
#define RIGHT_MOTOR_INH MOTOR6_IN1
#define RIGHT_MOTOR_INL MOTOR6_IN2
#define RIGHT_MOTOR_ENL IOCON6bits.PENL
#define RIGHT_MOTOR_ENH IOCON6bits.PENH
#define RIGHT_MOTOR_DUTY_CYCLE PDC6

// Motor alias
#define RIGHT_MOTOR 1
#define LEFT_MOTOR 0

// Prototypes fonctions
void InitIO();

#endif /* IO_H */