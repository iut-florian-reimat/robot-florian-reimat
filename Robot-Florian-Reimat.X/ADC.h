/* 
 * File:   ADC.h
 * Author: TABLE 6
 *
 * Created on 11 septembre 2020, 15:06
 */

#ifndef ADC_H
#define	ADC_H

#ifdef	__cplusplus
extern "C" {
#endif

void InitADC1(void);
void ADC1StartConversionSequence(void);
unsigned int * ADCGetResult(void);
unsigned char ADCIsConversionFinished(void);
void ADCClearConversionFinishedFlag(void);


#ifdef	__cplusplus
}
#endif

#endif	/* ADC_H */

