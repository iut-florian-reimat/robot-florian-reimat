/* 
 * File:   QEI..h
 * Author: TABLE 6
 *
 * Created on 1 octobre 2020, 18:31
 */

#ifndef QEI__H
#define	QEI__H

// m
#define DISTROUES 0.1 * 128 * 0.87 * 1.05
// 2 * 0.1050
// 0.218
#define WHEEL_DIAMETER 0.00425 * PI / 8192
// 0.00001630 / 250
// 0.0425 * PI / 8180
// 0.00001620


void InitQEI1();
void InitQEI2();
void QEIUpdateData();
void QEIReset();
void QEISetPosition(float,float,float);
#endif	/* QEI__H */

