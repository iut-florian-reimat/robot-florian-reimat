# HerculeX.NET, a C# library to manage Herkulex DRS servo via Serial Port
This library allows control over Herkulex DRS Servos via a serial port in C# .NET based applications.
### Thanks to:

## How to use it:
### Required:
You need to incude a reference to **ExtendedSerialPort.dll** 

### Getting Started:

```Csharp
using System;
using System.IO.Ports;
using ExtendedSerialPort;

string serialPort = "COMx" // The COM of your Serial Port
int baudRate = 115200; // Default Baud Speed 

void Main() {
	static HerkulexController herculexController = new HerculexController(serialPort, baudRate, Parity.None, 8 , StopBits.One);
}
```

### Documentation:
#### High-Level Methods
```csharp
void SetID(byte oldID, byte newID)
```
Edit the ID of a Servo.

```csharp
byte[] ScanForServoIDs(int timeout, int minID, int maxID)
```
Scan all Servo from minID to maxID and return
 - **timeout**: The timeout in ms (Default: 70ms)
 - **minID**: The minimum ID of the Search (Default: 1)
 - **maxID**: The maximum ID of the Search (Default: 253/0xFD)

```csharp
void SetPollingFreq(int frequency)
```
Set the polling frequency
 - **frequency**: The frequency (Max : 50)

```csharp 
void SetAckTimeout(int timeout)
```
Set the Ack timeout
 - **timeout** The timeout in ms

```csharp
void SetTorqueMode(byte ID, HerkulexDescription.TorqueControl mode)
```
Set the torque mode
 - **ID**: ID of the Servo
 - **mode:** The torque mode (see ##)

```csharp
void SetLedColor(byte ID, HerkulexDescription.LedColor color)
```
Set the LED color of the Servo
 - **ID**: ID of the Servo
 - **color**: new color of the LED (see ###)

```csharp
void SetPosition(byte ID, ushort absolutePosition, byte playTime, bool IsSynchronous)
```
Set the absolute position of the Servo
 - **ID**: ID of the Servo
 - **absolutePosition**: new absolute position of the Servo
 - **playtime**: the play time in ms (min = ~10) [this is a *felling* value]
 - **isSynchronous**: Set if the Servo need to be Synchronous (default: false)

```csharp
void SetMaximumPosition(byte ID, ushort position, bool keepAfterReboot)
```
Set the Maximum Position of the Servo
 - **ID**: ID of the Servo
 - **position**: Maximum absolute position
 - **keepAfterReboot**: Weither to keep the change after a servo reboot (default: true)

```csharp
void SetMinimumPosition(byte ID, UInt16 position, bool keepAfterReboot)
```
Set the Minimum Position of the Servo
 - **ID**: ID of the Servo
 - **position**: Minimum absolute position
 - **keepAfterReboot**: Weither to keep the change after a servo reboot (default: true)

``` csharp
void RecoverErrors(Servo servo)
```
