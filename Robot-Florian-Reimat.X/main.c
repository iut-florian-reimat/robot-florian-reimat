#include <stdio.h>
#include <stdlib.h>
#include <xc.h>
#include "ChipConfig.h"
#include "IO.h"
#include "timer.h"
#include "PWM.h"
#include "Robot.h"
#include "ADC.h"

int main ( void) {
    // Initialisation de l?oscillateur
    InitOscillator() ;

    // Configuration des entres sorties
    InitIO ();
    LED_BLANCHE = 1 ;
    LED_BLEUE = 1 ;
    LED_ORANGE = 1 ;
    InitPWM();
    
    InitTimer1();
    InitTimer23();
    InitADC1();
    //robotState.vitesseGaucheConsigne = 50.0f;
    // Boucle Principale
    while ( 1 ) {

    } // fin main
}