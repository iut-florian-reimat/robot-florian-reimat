#include "msgDecoder.h"
#include "msgEncoder.h"
#include "msgProcessor.h"
#include <xc.h>
#include "main.h"
#include "CB_RX1.h"
#include "Protocol.h"

unsigned char msgDecodedFunction = 0;
unsigned int msgDecodedPayloadLength = 0;
unsigned char msgDecodedPayload [128];
unsigned int msgDecodedPayloadIndex = 0;

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
            if (c == SOF_BYTE) {
                // Message begin
                msgDecodedFunction = 0;
                msgDecodedPayloadLength = 0;
                msgDecodedPayloadIndex = 0;
                rcvState = FunctionMSB;
            }
            break;
        case FunctionMSB:
            msgDecodedFunction = (unsigned char) (c << 8);
            rcvState = FunctionLSB;
            break;
        case FunctionLSB:
            msgDecodedFunction += (unsigned char) (c << 0);
            rcvState = PayloadLengthMSB;
            break;
        case PayloadLengthMSB:
            msgDecodedPayloadLength = (unsigned int) (c << 8);
            rcvState = PayloadLengthLSB;
            break;
        case PayloadLengthLSB:
            msgDecodedPayloadLength += (unsigned int) (c << 0);
            if (msgDecodedPayloadLength >= MAX_PAYLOAD_LENGHT){
                if (msgDecodedPayloadLength > 0) {
                    rcvState = Payload;
                } else {
                    rcvState = CheckSum;
                }
            } else {
                rcvState = Waiting;
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
            calculatedChecksum = UartCalculateChecksum(msgDecodedFunction,msgDecodedPayloadLength, msgDecodedPayload);
            if (calculatedChecksum == receivedChecksum) {
                // Success, on a un message 
                UartProcessDecodedMessage(msgDecodedFunction,msgDecodedPayload);
            }
            rcvState = Waiting;
            break;
        default:
            rcvState = Waiting;
            break;
    }
}


