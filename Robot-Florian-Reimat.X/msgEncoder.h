/* 
 * File:   msgEncoder.h
 * Author: Florian REIMAT
 *
 * Created on 10 novembre 2020, 10:19
 */

#ifndef MSGENCODER_H
#define	MSGENCODER_H

unsigned char UartCalculateChecksum(unsigned char, unsigned char*); 
void EncodeWithoutChecksum(unsigned char * , unsigned char msgFunction, unsigned int, unsigned char*);
void UartEncodeAndSendMessage(unsigned char , unsigned char*);

#endif	/* MSGENCODER_H */

