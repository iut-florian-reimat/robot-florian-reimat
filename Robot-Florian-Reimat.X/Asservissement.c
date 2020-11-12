#include <xc.h>
#include "Asservissement.h"
#include "QEI.h"
#include "timer.h"

double Te = 1 / FREQ_ECH_QEI;

// double ed0, ed1, ed2; //valeurs de l'entrée du correcteur droit à t, t-1, t-2
// double sd0, sd1, sd2; //valeurs de la sortie du correcteur droit à t, t-1, t-2
double eAng0, eAng1, eAng2; //valeurs de l'entrée du correcteur gauche à t, t-1, t-2
double sAng0, sAng1, sAng2; //valeurs de la sortie du correcteur gauche à t, t-1, t-2

double alphaAng;
double betaAng;
double deltaAng;   

void SetUpPiAsservissementVitesseAngulaire(double Ku, double Tu) {
    //Reglage de Ziegler Nichols sans depassements : un tout petit peu mou
    double Kp = 0; // MODIFY
    double Ti = 0; // MODIFY
    double Td = 0; // THIS IS NOT AN ERROR
    double Ki = Kp / Ti;
    double Kd = Kp * Td;
    alphaAng = 0; // MODIFY
    betaAng  = 0; // MODIFY
    deltaAng = 0; // MODIFY
}

    double CorrecteurVitesseAngulaire (double e) {
    eAng2 = eAng1;
    eAng1 = eAng0;
    eAng0 = e;
    sAng1 = sAng0;
    sAng0 = sAng1 + eAng0 * alphaAng + eAng1 * betaAng + eAng2 * deltaAng;
    return sAng0;
}
