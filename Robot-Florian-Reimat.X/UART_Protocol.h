/* 
 * File:   Uart_Protocol.h
 * Author: TABLE 6
 *
 * Created on 25 septembre 2020, 13:30
 */

#ifndef UART_PROTOCOL_H
#define	UART_PROTOCOL_H

#define GET_LED_STATE               0x0020
#define GET_MOTOR_SPEED             0x0040
#define SEND_ROBOT_STATE            0x0050
#define GET_ROBOT_STATE             0x0051
#define SEND_MESSAGE                0x0080

unsigned char UartCalculateChecksum(int, int, unsigned char*);
void UartEncodeAndSendMessage(int ,int, unsigned char*);
void UartDecodeMessage(unsigned char);
void UartProcessDecodedMessage(int ,int , unsigned char*);
void EncodeWithoutChecksum(unsigned char *, int, int, unsigned char*);
#endif	/* UART_PROTOCOL_H */

