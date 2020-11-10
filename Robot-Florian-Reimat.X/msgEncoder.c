#include "msgEncoder.h"
#include "../CB_TX1.h"

unsigned char UartCalculateChecksum(unsigned char msgFunction, unsigned char* msgPayload) {
    unsigned int msgPayloadLenght = sizeof(msgPayload);
    unsigned char msg[6 + msgPayloadLenght];
    EncodeWithoutChecksum(msg, msgFunction, msgPayloadLenght, msgPayload);
    unsigned char checksum = msg[0];
    unsigned int i;
    for (i = 1; i <  5 + msgPayloadLenght; i++) {
        checksum ^= msg[i];
    }
    return checksum;
}

void EncodeWithoutChecksum(unsigned char * msg, unsigned char msgFunction, unsigned int msgPayloadLength, unsigned char* msgPayload) {
    msg[0] = 0xFE;
    msg[1] = (unsigned char) (msgFunction >> 8);
    msg[2] = (unsigned char) (msgFunction >> 0);
    msg[3] = (unsigned char) (msgPayloadLength >> 8);
    msg[4] = (unsigned char) (msgPayloadLength >> 0);
    
    int i;
    for (i = 0; i < msgPayloadLength; i++) {
        msg[5 + i] = msgPayload[i];
    }
}

void UartEncodeAndSendMessage(unsigned char msgFunction, unsigned char* msgPayload) {
    unsigned int msgPayloadLenght = sizeof(msgPayload); 
    unsigned char payload[6 + msgPayloadLenght];
    EncodeWithoutChecksum(payload, msgFunction, msgPayloadLenght, msgPayload);
    unsigned char checksum = UartCalculateChecksum( msgFunction, msgPayload);
    payload[5 + msgPayloadLenght] = checksum;
    SendMessage(payload);
}