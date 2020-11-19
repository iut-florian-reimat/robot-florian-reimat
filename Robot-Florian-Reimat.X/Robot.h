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
            
            double vitesseLineaireFromOdometry;
            double vitesseLineaireConsigne;
            
            double vitesseAngulaireFromOdometry;
            double vitesseAngulaireConsigne;
            
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