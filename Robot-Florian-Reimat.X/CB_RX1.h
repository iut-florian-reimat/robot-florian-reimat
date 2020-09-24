/* 
 * File:   CB_RX1.h
 * Author: TABLE 6
 *
 * Created on 24 septembre 2020, 11:02
 */

#ifndef CB_RX1_H
#define	CB_RX1_H

void CB_RX1_Add(unsigned char);
unsigned char CB_RX1_Get(void);
unsigned char CB_RX1_IsDataAvailable(void);
int CB_RX1_GetRemainingSize(void);
int CB_RX1_GetDataSize(void);

#endif	/* CB_RX1_H */

