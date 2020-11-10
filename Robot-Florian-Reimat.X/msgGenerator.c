#include "../Protocol.h"
#include "msgGenerator.h"
#include "msgEncoder.h"

void GenerateStateMessage(unsigned char stateRobot) {
    unsigned char payload[5] = {stateRobot, timestamp >> 24, timestamp >> 16, timestamp >> 8, timestamp >> 0};
    UartEncodeAndSendMessage(SEND_ROBOT_STATE, payload);
}

void GenerateTextMessage(unsigned char* message){
    UartEncodeAndSendMessage(SEND_MESSAGE, message);
}

