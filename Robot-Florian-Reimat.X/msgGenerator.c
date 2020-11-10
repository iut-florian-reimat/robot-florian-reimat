#include "Protocol.h"
#include "main.h"
#include "msgGenerator.h"
#include "msgEncoder.h"
#include "timer.h"
#include "Utilities.h"
#include "Robot.h"

void GenerateStateMessage(unsigned char stateRobot) {
    unsigned char payload[5] = {stateRobot, timestamp >> 24, timestamp >> 16, timestamp >> 8, timestamp >> 0};
    UartEncodeAndSendMessage(SEND_ROBOT_STATE,5, payload);
}

void GenerateTextMessage(unsigned char* message,unsigned int lenght){
    UartEncodeAndSendMessage(SEND_MESSAGE,lenght, message);
}

void SendPositionData() {
    unsigned char positionPayload [24];
    getBytesFromInt32(positionPayload, 0, timestamp);
    getBytesFromFloat(positionPayload, 4, (float) (robotState.xPosFromOdometry));
    getBytesFromFloat(positionPayload, 8, (float) (robotState.yPosFromOdometry));
    getBytesFromFloat(positionPayload, 12, (float) (robotState.angleRadianFromOdometry));
    getBytesFromFloat(positionPayload, 16, (float) (robotState.vitesseLineaireFromOdometry));
    getBytesFromFloat(positionPayload, 20, (float) (robotState.vitesseAngulaireFromOdometry));
    UartEncodeAndSendMessage(SEND_POSITION, 24, positionPayload);
}
