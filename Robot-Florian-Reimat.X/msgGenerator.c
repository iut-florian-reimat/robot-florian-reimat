#include "Protocol.h"
#include "main.h"
#include "msgGenerator.h"
#include "msgEncoder.h"
#include "timer.h"
#include "Utilities.h"
#include "Robot.h"

void GenerateStateMessage(unsigned char stateRobot) 
{
    unsigned char payload[5] = {stateRobot, timestamp >> 24, timestamp >> 16, timestamp >> 8, timestamp >> 0};
    UartEncodeAndSendMessage(SEND_ROBOT_STATE,5, payload);
}

void GenerateTextMessage(unsigned char* message,unsigned int lenght)
{
    UartEncodeAndSendMessage(SEND_MESSAGE,lenght, message);
}

void GeneratePositionMessage() 
{
    unsigned char positionPayload [24];
    getBytesFromInt32(positionPayload, 0, 0);
    getBytesFromFloat(positionPayload, 4,  (float) (robotState.xPosFromOdometry));
    getBytesFromFloat(positionPayload, 8,  (float) (robotState.yPosFromOdometry));
    getBytesFromFloat(positionPayload, 12, (float) (robotState.angleRadianFromOdometry));
    getBytesFromFloat(positionPayload, 16, (float) (robotState.vitesseLineaireFromOdometry));
    getBytesFromFloat(positionPayload, 20, (float) (robotState.vitesseAngulaireFromOdometry));
    UartEncodeAndSendMessage(SEND_POSITION, 24, positionPayload);
}

void GenerateAsservPolarMessage()
{
    unsigned char asservPayload[104];

    getBytesFromFloat(asservPayload, 4*00, (float) (robotState.vitesseLineaireFromOdometry));
    getBytesFromFloat(asservPayload, 4*01, (float) (robotState.vitesseAngulaireFromOdometry));
    getBytesFromFloat(asservPayload, 4*02, (float) (robotState.vitesseLineaireCommande));
    getBytesFromFloat(asservPayload, 4*03, (float) (robotState.vitesseAngulaireCommande));
    getBytesFromFloat(asservPayload, 4*04, (float) (robotState.vitesseLineaireErreur));
    getBytesFromFloat(asservPayload, 4*05, (float) (robotState.vitesseAngulaireErreur));
    getBytesFromFloat(asservPayload, 4*06, (float) (robotState.vitesseLineaireConsigne));
    getBytesFromFloat(asservPayload, 4*07, (float) (robotState.vitesseAngulaireConsigne));
    
    getBytesFromFloat(asservPayload, 4*8, (float) (robotState.KpLineaire));
    getBytesFromFloat(asservPayload, 4*9, (float) (robotState.KpAngulaire));
    getBytesFromFloat(asservPayload, 4*10, (float) (robotState.CorrectionLineaireKp));
    getBytesFromFloat(asservPayload, 4*11, (float) (robotState.CorrectionAngulaireKp));
    getBytesFromFloat(asservPayload, 4*12, (float) (robotState.KpLineaireMax));
    getBytesFromFloat(asservPayload, 4*13, (float) (robotState.KpAngulaireMax));
    
    getBytesFromFloat(asservPayload, 4*14, (float) (robotState.KiLineaire));
    getBytesFromFloat(asservPayload, 4*15, (float) (robotState.KiAngulaire));
    getBytesFromFloat(asservPayload, 4*16, (float) (robotState.CorrectionLineaireKi));
    getBytesFromFloat(asservPayload, 4*17, (float) (robotState.CorrectionAngulaireKi));
    getBytesFromFloat(asservPayload, 4*18, (float) (robotState.KiLineaireMax));
    getBytesFromFloat(asservPayload, 4*19, (float) (robotState.KiAngulaireMax));
    
    getBytesFromFloat(asservPayload, 4*20, (float) (robotState.KdLineaire));
    getBytesFromFloat(asservPayload, 4*21, (float) (robotState.KdAngulaire));
    getBytesFromFloat(asservPayload, 4*22, (float) (robotState.CorrectionLineaireKd));
    getBytesFromFloat(asservPayload, 4*23, (float) (robotState.CorrectionAngulaireKd));
    getBytesFromFloat(asservPayload, 4*24, (float) (robotState.KdLineaireMax));
    getBytesFromFloat(asservPayload, 4*25, (float) (robotState.KdAngulaireMax));
    
    
    
    UartEncodeAndSendMessage(SEND_POLAR_ASSERV, 104, asservPayload);
}