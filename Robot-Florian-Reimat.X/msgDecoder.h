/* 
 * File:   msgDecoder.h
 * Author: TABLE 6
 *
 * Created on 10 novembre 2020, 10:05
 */

#ifndef MSGDECODER_H
#define	MSGDECODER_H

unsigned char UartCalculateChecksum(unsigned char , unsigned char* );
void UartDecodeMessage(unsigned char );
#endif	/* MSGDECODER_H */

