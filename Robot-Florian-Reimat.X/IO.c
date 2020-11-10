/*
 * File:   IO.c
 */

#include <xc.h>
#include "IO.h"

void InitIO()
{
    // IMPORTANT : désactiver les entrées analogiques, sinon on perd les entrées numériques
    ANSELA = 0; // 0 desactive
    ANSELB = 0;
    ANSELC = 0;
    ANSELD = 0;
    ANSELE = 0;
    ANSELF = 0;
    ANSELG = 0;

    //********** Configuration des sorties : _TRISxx = 0 ********************************
    // LED
    _TRISC10 = 0;   //  LED Orange
    _TRISG6 = 0;    //  LED Blanche
    _TRISG7 = 0;    //  LED Bleue
    
    // Moteurs 
    _TRISB14 = 0;
    _TRISB15 = 0;
    _TRISC6 = 0;
    _TRISC7 = 0;
    
    // UART
    _U1RXR = 24;
    _RP36R = 1;
        
    //********** Configuration des entrées : _TRISxx = 1 ********************************  
    
    // ???????????????????? QEI ?????????????????
    _QEA2R = 97; // a s s i g n QEI A t o pin RP97
    _QEB2R = 96; // a s s i g n QEI B t o pin RP96
    _QEA1R = 70; // a s s i g n QEI A t o pin RP70
    _QEB1R = 69; // a s s i g n QEI B t o pin RP69

}
