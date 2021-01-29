#ifndef ROBOT_H
#define ROBOT_H
typedef struct robotStateBITS {
    union {
        struct {
            unsigned char taskEnCours;
            float vitesseGaucheConsigne ;
            float vitesseGaucheCommandeCourante;
            float vitesseDroiteConsigne;
            float vitesseDroiteCommandeCourante;
            
            float distanceTelemetreDroit;
            float distanceTelemetreCentre;
            float distanceTelemetreGauche;
            
            double distanceGaucheFromOdometry;
            double distanceDroitFromOdometry;            
            
            double vitesseGaucheFromOdometry;
            double vitesseDroitFromOdometry;
            
            double KpLineaire;
            double KiLineaire;
            double KdLineaire;
            double KpLineaireMax;
            double KiLineaireMax;
            double KdLineaireMax;
            
            double KpAngulaire;
            double KiAngulaire;
            double KdAngulaire;
            double KpAngulaireMax;
            double KiAngulaireMax;
            double KdAngulaireMax;
            
            double vitesseLineaireFromOdometry;
            double vitesseLineaireConsigne;
            double vitesseLineaireCommande;
            double vitesseLineaireErreur;
            double vitesseLineaireCorrection;
            double CorrectionLineaireKp;
            double CorrectionLineaireKi;
            double CorrectionLineaireKd;
                        
            double vitesseAngulaireFromOdometry;
            double vitesseAngulaireConsigne;
            double vitesseAngulaireCommande;
            double vitesseAngulaireErreur;
            double vitesseAngulaireCorrection;
            double CorrectionAngulaireKp;
            double CorrectionAngulaireKi;
            double CorrectionAngulaireKd;         
            
            double xPosFromOdometry_1;
            double yPosFromOdometry_1;
            double xPosFromOdometry;
            double yPosFromOdometry;
            double angleRadianFromOdometry_1;
            double angleRadianFromOdometry;
        };
    };
} ROBOT_STATE_BITS;
extern volatile ROBOT_STATE_BITS robotState ;
#endif /*ROBOT_H*/