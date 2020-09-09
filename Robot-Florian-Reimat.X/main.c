#include <stdio.h>
#include <stdlib.h>
#include <xc.h>
#include "ChipConfig.h"
#include "IO.h"
#include "timer.h"
#include "PWM.h"

int main ( void) {
    // Initialisation de l?oscillateur
    InitOscillator() ;

    // Configuration des entres sorties
    InitIO ();
    LED_BLANCHE = 1 ;
    LED_BLEUE = 1 ;
    LED_ORANGE = 1 ;
    InitTimer23();
    //InitTimer1();
    InitPWM();
    
    // Boucle Principale
    while ( 1 ) {

    } // fin main
}