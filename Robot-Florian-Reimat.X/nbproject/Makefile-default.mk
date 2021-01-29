#
# Generated Makefile - do not edit!
#
# Edit the Makefile in the project folder instead (../Makefile). Each target
# has a -pre and a -post target defined where you can add customized code.
#
# This makefile implements configuration specific macros and targets.


# Include project Makefile
ifeq "${IGNORE_LOCAL}" "TRUE"
# do not include local makefile. User is passing all local related variables already
else
include Makefile
# Include makefile containing local settings
ifeq "$(wildcard nbproject/Makefile-local-default.mk)" "nbproject/Makefile-local-default.mk"
include nbproject/Makefile-local-default.mk
endif
endif

# Environment
MKDIR=gnumkdir -p
RM=rm -f 
MV=mv 
CP=cp 

# Macros
CND_CONF=default
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
IMAGE_TYPE=debug
OUTPUT_SUFFIX=elf
DEBUGGABLE_SUFFIX=elf
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/Robot-Florian-Reimat.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}
else
IMAGE_TYPE=production
OUTPUT_SUFFIX=hex
DEBUGGABLE_SUFFIX=elf
FINAL_IMAGE=dist/${CND_CONF}/${IMAGE_TYPE}/Robot-Florian-Reimat.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}
endif

ifeq ($(COMPARE_BUILD), true)
COMPARISON_BUILD=-mafrlcsj
else
COMPARISON_BUILD=
endif

ifdef SUB_IMAGE_ADDRESS
SUB_IMAGE_ADDRESS_COMMAND=--image-address $(SUB_IMAGE_ADDRESS)
else
SUB_IMAGE_ADDRESS_COMMAND=
endif

# Object Directory
OBJECTDIR=build/${CND_CONF}/${IMAGE_TYPE}

# Distribution Directory
DISTDIR=dist/${CND_CONF}/${IMAGE_TYPE}

# Source Files Quoted if spaced
SOURCEFILES_QUOTED_IF_SPACED=msgDecoder.c msgEncoder.c msgGenerator.c msgProcessor.c CB_RX1.c CB_TX1.c main.c ChipConfig.c IO.c timer.c Robot.c Utilities.c PWM.c ADC.c uart.c QEI.c Asservissement.c

# Object Files Quoted if spaced
OBJECTFILES_QUOTED_IF_SPACED=${OBJECTDIR}/msgDecoder.o ${OBJECTDIR}/msgEncoder.o ${OBJECTDIR}/msgGenerator.o ${OBJECTDIR}/msgProcessor.o ${OBJECTDIR}/CB_RX1.o ${OBJECTDIR}/CB_TX1.o ${OBJECTDIR}/main.o ${OBJECTDIR}/ChipConfig.o ${OBJECTDIR}/IO.o ${OBJECTDIR}/timer.o ${OBJECTDIR}/Robot.o ${OBJECTDIR}/Utilities.o ${OBJECTDIR}/PWM.o ${OBJECTDIR}/ADC.o ${OBJECTDIR}/uart.o ${OBJECTDIR}/QEI.o ${OBJECTDIR}/Asservissement.o
POSSIBLE_DEPFILES=${OBJECTDIR}/msgDecoder.o.d ${OBJECTDIR}/msgEncoder.o.d ${OBJECTDIR}/msgGenerator.o.d ${OBJECTDIR}/msgProcessor.o.d ${OBJECTDIR}/CB_RX1.o.d ${OBJECTDIR}/CB_TX1.o.d ${OBJECTDIR}/main.o.d ${OBJECTDIR}/ChipConfig.o.d ${OBJECTDIR}/IO.o.d ${OBJECTDIR}/timer.o.d ${OBJECTDIR}/Robot.o.d ${OBJECTDIR}/Utilities.o.d ${OBJECTDIR}/PWM.o.d ${OBJECTDIR}/ADC.o.d ${OBJECTDIR}/uart.o.d ${OBJECTDIR}/QEI.o.d ${OBJECTDIR}/Asservissement.o.d

# Object Files
OBJECTFILES=${OBJECTDIR}/msgDecoder.o ${OBJECTDIR}/msgEncoder.o ${OBJECTDIR}/msgGenerator.o ${OBJECTDIR}/msgProcessor.o ${OBJECTDIR}/CB_RX1.o ${OBJECTDIR}/CB_TX1.o ${OBJECTDIR}/main.o ${OBJECTDIR}/ChipConfig.o ${OBJECTDIR}/IO.o ${OBJECTDIR}/timer.o ${OBJECTDIR}/Robot.o ${OBJECTDIR}/Utilities.o ${OBJECTDIR}/PWM.o ${OBJECTDIR}/ADC.o ${OBJECTDIR}/uart.o ${OBJECTDIR}/QEI.o ${OBJECTDIR}/Asservissement.o

# Source Files
SOURCEFILES=msgDecoder.c msgEncoder.c msgGenerator.c msgProcessor.c CB_RX1.c CB_TX1.c main.c ChipConfig.c IO.c timer.c Robot.c Utilities.c PWM.c ADC.c uart.c QEI.c Asservissement.c



CFLAGS=
ASFLAGS=
LDLIBSOPTIONS=

############# Tool locations ##########################################
# If you copy a project from one host to another, the path where the  #
# compiler is installed may be different.                             #
# If you open this project with MPLAB X in the new host, this         #
# makefile will be regenerated and the paths will be corrected.       #
#######################################################################
# fixDeps replaces a bunch of sed/cat/printf statements that slow down the build
FIXDEPS=fixDeps

.build-conf:  ${BUILD_SUBPROJECTS}
ifneq ($(INFORMATION_MESSAGE), )
	@echo $(INFORMATION_MESSAGE)
endif
	${MAKE}  -f nbproject/Makefile-default.mk dist/${CND_CONF}/${IMAGE_TYPE}/Robot-Florian-Reimat.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}

MP_PROCESSOR_OPTION=33EP512GM706
MP_LINKER_FILE_OPTION=,--script=p33EP512GM706.gld
# ------------------------------------------------------------------------------------
# Rules for buildStep: compile
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
${OBJECTDIR}/msgDecoder.o: msgDecoder.c  .generated_files/6fcf5df76e36d8c4b458495e1864b4e7ec1e909f.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/msgDecoder.o.d 
	@${RM} ${OBJECTDIR}/msgDecoder.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  msgDecoder.c  -o ${OBJECTDIR}/msgDecoder.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/msgDecoder.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/msgEncoder.o: msgEncoder.c  .generated_files/983474468ac02c898df9ca739f1026ae52132fde.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/msgEncoder.o.d 
	@${RM} ${OBJECTDIR}/msgEncoder.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  msgEncoder.c  -o ${OBJECTDIR}/msgEncoder.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/msgEncoder.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/msgGenerator.o: msgGenerator.c  .generated_files/122bf01b97ed75b33a3d118f91e7aa1eb3d41ab1.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/msgGenerator.o.d 
	@${RM} ${OBJECTDIR}/msgGenerator.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  msgGenerator.c  -o ${OBJECTDIR}/msgGenerator.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/msgGenerator.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/msgProcessor.o: msgProcessor.c  .generated_files/e7fd63813f12293ecab42e7da7ddc73566f95755.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/msgProcessor.o.d 
	@${RM} ${OBJECTDIR}/msgProcessor.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  msgProcessor.c  -o ${OBJECTDIR}/msgProcessor.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/msgProcessor.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/CB_RX1.o: CB_RX1.c  .generated_files/89a7ed708a3446af43454687543da426cef996e7.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/CB_RX1.o.d 
	@${RM} ${OBJECTDIR}/CB_RX1.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  CB_RX1.c  -o ${OBJECTDIR}/CB_RX1.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/CB_RX1.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/CB_TX1.o: CB_TX1.c  .generated_files/f2ba603889f06c6cd9c15ba505af01070a338539.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/CB_TX1.o.d 
	@${RM} ${OBJECTDIR}/CB_TX1.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  CB_TX1.c  -o ${OBJECTDIR}/CB_TX1.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/CB_TX1.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/main.o: main.c  .generated_files/8527953851fa18eb7a197ae19ec22b8e40aac16c.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/main.o.d 
	@${RM} ${OBJECTDIR}/main.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  main.c  -o ${OBJECTDIR}/main.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/main.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/ChipConfig.o: ChipConfig.c  .generated_files/2555b917fc487c0ca2bbd2169ce1b7eb33b0dfdc.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ChipConfig.o.d 
	@${RM} ${OBJECTDIR}/ChipConfig.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ChipConfig.c  -o ${OBJECTDIR}/ChipConfig.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/ChipConfig.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/IO.o: IO.c  .generated_files/67c6c775126f5a9357edafdb4725edad8713ca98.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/IO.o.d 
	@${RM} ${OBJECTDIR}/IO.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  IO.c  -o ${OBJECTDIR}/IO.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/IO.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/timer.o: timer.c  .generated_files/9971822eac498b0c80ecd2d705847ca07e32de43.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/timer.o.d 
	@${RM} ${OBJECTDIR}/timer.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  timer.c  -o ${OBJECTDIR}/timer.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/timer.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/Robot.o: Robot.c  .generated_files/a140d59bf15c790bcb26bcc988de4ab17779a649.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/Robot.o.d 
	@${RM} ${OBJECTDIR}/Robot.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  Robot.c  -o ${OBJECTDIR}/Robot.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/Robot.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/Utilities.o: Utilities.c  .generated_files/5ba63396d549430b64b36156a8e23f27434148ac.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/Utilities.o.d 
	@${RM} ${OBJECTDIR}/Utilities.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  Utilities.c  -o ${OBJECTDIR}/Utilities.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/Utilities.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/PWM.o: PWM.c  .generated_files/8dba7d02ed7be5be520b305a502ece48a3584e00.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/PWM.o.d 
	@${RM} ${OBJECTDIR}/PWM.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  PWM.c  -o ${OBJECTDIR}/PWM.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/PWM.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/ADC.o: ADC.c  .generated_files/e828b925e4440510e06539fd6511ef994f6bc535.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ADC.o.d 
	@${RM} ${OBJECTDIR}/ADC.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ADC.c  -o ${OBJECTDIR}/ADC.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/ADC.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/uart.o: uart.c  .generated_files/3033ec44d6820d06d01c638743eb875391a8081a.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/uart.o.d 
	@${RM} ${OBJECTDIR}/uart.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  uart.c  -o ${OBJECTDIR}/uart.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/uart.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/QEI.o: QEI.c  .generated_files/4b73ad6a853ca071409a7bf435b189286341185f.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/QEI.o.d 
	@${RM} ${OBJECTDIR}/QEI.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  QEI.c  -o ${OBJECTDIR}/QEI.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/QEI.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/Asservissement.o: Asservissement.c  .generated_files/9ce1ba13cdd559400f6e3d1e505cedeaf03aa1b1.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/Asservissement.o.d 
	@${RM} ${OBJECTDIR}/Asservissement.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  Asservissement.c  -o ${OBJECTDIR}/Asservissement.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/Asservissement.o.d"      -g -D__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -mno-eds-warn  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
else
${OBJECTDIR}/msgDecoder.o: msgDecoder.c  .generated_files/c41dd4d8580bb6bd1f93e56652a59d5bfe5c23ad.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/msgDecoder.o.d 
	@${RM} ${OBJECTDIR}/msgDecoder.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  msgDecoder.c  -o ${OBJECTDIR}/msgDecoder.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/msgDecoder.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/msgEncoder.o: msgEncoder.c  .generated_files/60e6b8444fc07ccd3e5a694388db4c622f16c109.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/msgEncoder.o.d 
	@${RM} ${OBJECTDIR}/msgEncoder.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  msgEncoder.c  -o ${OBJECTDIR}/msgEncoder.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/msgEncoder.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/msgGenerator.o: msgGenerator.c  .generated_files/7a322dd1199d97f129796b583319a89506166be2.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/msgGenerator.o.d 
	@${RM} ${OBJECTDIR}/msgGenerator.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  msgGenerator.c  -o ${OBJECTDIR}/msgGenerator.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/msgGenerator.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/msgProcessor.o: msgProcessor.c  .generated_files/f1789a14bd577389537f49edd192640edc8945da.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/msgProcessor.o.d 
	@${RM} ${OBJECTDIR}/msgProcessor.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  msgProcessor.c  -o ${OBJECTDIR}/msgProcessor.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/msgProcessor.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/CB_RX1.o: CB_RX1.c  .generated_files/f3a21aa300d908bd293759ca3edb5faf4afd2651.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/CB_RX1.o.d 
	@${RM} ${OBJECTDIR}/CB_RX1.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  CB_RX1.c  -o ${OBJECTDIR}/CB_RX1.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/CB_RX1.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/CB_TX1.o: CB_TX1.c  .generated_files/a0cf9efe9ee8738086b49adc40f0a989021f57df.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/CB_TX1.o.d 
	@${RM} ${OBJECTDIR}/CB_TX1.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  CB_TX1.c  -o ${OBJECTDIR}/CB_TX1.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/CB_TX1.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/main.o: main.c  .generated_files/f336ea67f84aaea06e1774b917735fa43a70bb2.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/main.o.d 
	@${RM} ${OBJECTDIR}/main.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  main.c  -o ${OBJECTDIR}/main.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/main.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/ChipConfig.o: ChipConfig.c  .generated_files/6cd268d34789345680ba3b85d82777c945b8004d.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ChipConfig.o.d 
	@${RM} ${OBJECTDIR}/ChipConfig.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ChipConfig.c  -o ${OBJECTDIR}/ChipConfig.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/ChipConfig.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/IO.o: IO.c  .generated_files/be6d5fd794390331f6c1e05c718f190cea9d1e1d.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/IO.o.d 
	@${RM} ${OBJECTDIR}/IO.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  IO.c  -o ${OBJECTDIR}/IO.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/IO.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/timer.o: timer.c  .generated_files/6aa7f3a1f1101fcba87dcc1610a3c2237d09a514.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/timer.o.d 
	@${RM} ${OBJECTDIR}/timer.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  timer.c  -o ${OBJECTDIR}/timer.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/timer.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/Robot.o: Robot.c  .generated_files/8dc9925e0dcf8545f37155b5653afed24380824b.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/Robot.o.d 
	@${RM} ${OBJECTDIR}/Robot.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  Robot.c  -o ${OBJECTDIR}/Robot.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/Robot.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/Utilities.o: Utilities.c  .generated_files/613286a05fee9a83079e78260f7ccf136c07a2fa.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/Utilities.o.d 
	@${RM} ${OBJECTDIR}/Utilities.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  Utilities.c  -o ${OBJECTDIR}/Utilities.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/Utilities.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/PWM.o: PWM.c  .generated_files/2dfe651091ea23f24d7f8a15d44b6561a98491e8.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/PWM.o.d 
	@${RM} ${OBJECTDIR}/PWM.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  PWM.c  -o ${OBJECTDIR}/PWM.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/PWM.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/ADC.o: ADC.c  .generated_files/faeb31496c2ec739696e5777ae1c97b147c943c2.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/ADC.o.d 
	@${RM} ${OBJECTDIR}/ADC.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  ADC.c  -o ${OBJECTDIR}/ADC.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/ADC.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/uart.o: uart.c  .generated_files/b87e4dab92e35c7f8f99eb7d45dbcd24b82bfb32.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/uart.o.d 
	@${RM} ${OBJECTDIR}/uart.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  uart.c  -o ${OBJECTDIR}/uart.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/uart.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/QEI.o: QEI.c  .generated_files/93c83ec83eb4237553a5f715ea23b0ef5471c88b.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/QEI.o.d 
	@${RM} ${OBJECTDIR}/QEI.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  QEI.c  -o ${OBJECTDIR}/QEI.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/QEI.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
${OBJECTDIR}/Asservissement.o: Asservissement.c  .generated_files/b0073d092bcf6c1548ea6825e2c0f7d92d626412.flag .generated_files/770bfaae211635eb1896a846f36edbb0fa131f01.flag
	@${MKDIR} "${OBJECTDIR}" 
	@${RM} ${OBJECTDIR}/Asservissement.o.d 
	@${RM} ${OBJECTDIR}/Asservissement.o 
	${MP_CC} $(MP_EXTRA_CC_PRE)  Asservissement.c  -o ${OBJECTDIR}/Asservissement.o  -c -mcpu=$(MP_PROCESSOR_OPTION)  -MP -MMD -MF "${OBJECTDIR}/Asservissement.o.d"      -mno-eds-warn  -g -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -O0 -msmart-io=1 -Wall -msfr-warn=off   
	
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemble
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: assemblePreproc
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
else
endif

# ------------------------------------------------------------------------------------
# Rules for buildStep: link
ifeq ($(TYPE_IMAGE), DEBUG_RUN)
dist/${CND_CONF}/${IMAGE_TYPE}/Robot-Florian-Reimat.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk    
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -o dist/${CND_CONF}/${IMAGE_TYPE}/Robot-Florian-Reimat.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      -mcpu=$(MP_PROCESSOR_OPTION)        -D__DEBUG=__DEBUG -D__MPLAB_DEBUGGER_ICD3=1  -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)   -mreserve=data@0x1000:0x101B -mreserve=data@0x101C:0x101D -mreserve=data@0x101E:0x101F -mreserve=data@0x1020:0x1021 -mreserve=data@0x1022:0x1023 -mreserve=data@0x1024:0x1027 -mreserve=data@0x1028:0x104F   -Wl,--local-stack,,--defsym=__MPLAB_BUILD=1,--defsym=__MPLAB_DEBUG=1,--defsym=__DEBUG=1,-D__DEBUG=__DEBUG,--defsym=__MPLAB_DEBUGGER_ICD3=1,$(MP_LINKER_FILE_OPTION),--stack=16,--check-sections,--data-init,--pack-data,--handles,--isr,--no-gc-sections,--fill-upper=0,--stackguard=16,--no-force-link,--smart-io,-Map="${DISTDIR}/${PROJECTNAME}.${IMAGE_TYPE}.map",--report-mem,--memorysummary,dist/${CND_CONF}/${IMAGE_TYPE}/memoryfile.xml$(MP_EXTRA_LD_POST)  
	
else
dist/${CND_CONF}/${IMAGE_TYPE}/Robot-Florian-Reimat.X.${IMAGE_TYPE}.${OUTPUT_SUFFIX}: ${OBJECTFILES}  nbproject/Makefile-${CND_CONF}.mk   
	@${MKDIR} dist/${CND_CONF}/${IMAGE_TYPE} 
	${MP_CC} $(MP_EXTRA_LD_PRE)  -o dist/${CND_CONF}/${IMAGE_TYPE}/Robot-Florian-Reimat.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX}  ${OBJECTFILES_QUOTED_IF_SPACED}      -mcpu=$(MP_PROCESSOR_OPTION)        -omf=elf -DXPRJ_default=$(CND_CONF)  -legacy-libc  $(COMPARISON_BUILD)  -Wl,--local-stack,,--defsym=__MPLAB_BUILD=1,$(MP_LINKER_FILE_OPTION),--stack=16,--check-sections,--data-init,--pack-data,--handles,--isr,--no-gc-sections,--fill-upper=0,--stackguard=16,--no-force-link,--smart-io,-Map="${DISTDIR}/${PROJECTNAME}.${IMAGE_TYPE}.map",--report-mem,--memorysummary,dist/${CND_CONF}/${IMAGE_TYPE}/memoryfile.xml$(MP_EXTRA_LD_POST)  
	${MP_CC_DIR}\\xc16-bin2hex dist/${CND_CONF}/${IMAGE_TYPE}/Robot-Florian-Reimat.X.${IMAGE_TYPE}.${DEBUGGABLE_SUFFIX} -a  -omf=elf   
	
endif


# Subprojects
.build-subprojects:


# Subprojects
.clean-subprojects:

# Clean Targets
.clean-conf: ${CLEAN_SUBPROJECTS}
	${RM} -r build/default
	${RM} -r dist/default

# Enable dependency checking
.dep.inc: .depcheck-impl

DEPFILES=$(shell mplabwildcard ${POSSIBLE_DEPFILES})
ifneq (${DEPFILES},)
include ${DEPFILES}
endif
