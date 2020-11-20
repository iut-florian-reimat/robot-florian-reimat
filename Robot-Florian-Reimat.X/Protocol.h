/* 
 * File:   Protocol.h
 * Author: Florian Reimat
 *
 * Created on 10 novembre 2020, 09:13
 * This file is for Generate Serial Message
 */

#ifndef PROTOCOL_H
#define	PROTOCOL_H

#define SOF_BYTE 0xFE
#define MAX_PAYLOAD_LENGHT 255


#define GET_LED_STATE               0x0020
#define GET_MOTOR_SPEED             0x0040
#define SEND_ROBOT_STATE            0x0050
#define GET_ROBOT_STATE             0x0051
#define SEND_POSITION               0x0061
#define RESET_POSITION              0x0062
#define SEND_MESSAGE                0x0080

#endif	/* PROTOCOL_H */

