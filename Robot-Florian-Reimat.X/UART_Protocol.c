#include <xc.h>
#include "CB_TX1.h"
#include "CB_RX1.h"
#include "main.h"
#include "UART_Protocol.h"
#include "IO.h"

unsigned char UartCalculateChecksum(int msgFunction, int msgPayloadLength, unsigned char* msgPayload) {
    unsigned char msg[6 + msgPayloadLength];
    EncodeWithoutChecksum(msg, msgFunction, msgPayloadLength, msgPayload);
    unsigned char checksum = msg[0];
    int i;
    for (i = 1; i <  5 + msgPayloadLength; i++) {
        checksum ^= msg[i];
    }
    return checksum;
}

void EncodeWithoutChecksum(unsigned char * msg, int msgFunction, int msgPayloadLength, unsigned char* msgPayload) {
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

void UartEncodeAndSendMessage(int msgFunction,int msgPayloadLength, unsigned char* msgPayload) {
    unsigned char msg[6 + msgPayloadLength];
    EncodeWithoutChecksum(msg, msgFunction, msgPayloadLength, msgPayload);
    unsigned char checksum = UartCalculateChecksum( msgFunction, msgPayloadLength, msgPayload);
    msg[5 + msgPayloadLength] = checksum;
    SendMessage(msg,6 + msgPayloadLength);
}

int msgDecodedFunction = 0;
int msgDecodedPayloadLength = 0;
unsigned char msgDecodedPayload [128];
int msgDecodedPayloadIndex = 0;

typedef enum{
    Waiting,
    FunctionMSB,
    FunctionLSB,
    PayloadLengthMSB,
    PayloadLengthLSB,
    Payload,
    CheckSum
} StateReception;

StateReception rcvState = Waiting;

void UartDecodeMessage(unsigned char c) {
    unsigned char calculatedChecksum, receivedChecksum;
    switch (rcvState) {
        case Waiting:
            if (c == 0xFE) {
                // Message begin
                msgDecodedFunction = 0;
                msgDecodedPayloadLength = 0;
                msgDecodedPayloadIndex = 0;
                rcvState = FunctionMSB;
            }
            break;
        case FunctionMSB:
            msgDecodedFunction = (int) (c << 8);
            rcvState = FunctionLSB;
            break;
        case FunctionLSB:
            msgDecodedFunction += (int) (c << 0);
            rcvState = PayloadLengthMSB;
            break;
        case PayloadLengthMSB:
            msgDecodedPayloadLength = (int) (c << 8);
            rcvState = PayloadLengthLSB;
            break;
        case PayloadLengthLSB:
            msgDecodedPayloadLength += (int) (c << 0);
            if (msgDecodedPayloadLength > 0) {
                rcvState = Payload;
            } else {
                rcvState = CheckSum;
            }
            break;

        case Payload:
            msgDecodedPayload[msgDecodedPayloadIndex] = c;
            msgDecodedPayloadIndex++;
            if (msgDecodedPayloadIndex >= msgDecodedPayloadLength) {
                rcvState = CheckSum;
            }
            break;
            
        case CheckSum:
            receivedChecksum = c;
            calculatedChecksum = UartCalculateChecksum(msgDecodedFunction, msgDecodedPayloadLength, msgDecodedPayload);
            if (calculatedChecksum == receivedChecksum) {
                // Success, on a un message 
                UartProcessDecodedMessage(msgDecodedFunction,msgDecodedPayloadLength,msgDecodedPayload);
            }
            rcvState = Waiting;
            break;
        default:
            rcvState = Waiting;
            break;
    }
}

void UartProcessDecodedMessage(int function, int payloadLength, unsigned char* payload) {
    switch (function) {
        case SET_LED_STATE:
            switch(payload[0]) {
                case 0x01:
                    LED_ORANGE = (payload[1] == 0x01)?1:0;
                    break;
                case 0x02:
                    LED_BLEUE = (payload[1] == 0x01)?1:0;
                    break;
                case 0x03:
                    LED_BLANCHE = (payload[1] == 0x01)?1:0;
                    break;
            }
            break;
        case SET_ROBOT_STATE:
            SetRobotState(payload[0]);
            break;
        case SET_ROBOT_MANUAL_CONTROL:
            SetRobotAutoControlState(payload[0]);
            break;
        
    }
}
