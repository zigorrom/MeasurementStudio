#include "CmdMessenger.h"

#define MAX_CHANNEL 32
unsigned long previousToggleLed = 0;
bool ledState = 0;
const int LED_PIN = 13;
const int A0_pin = 12;
const int A1_pin = 11;
const int A2_pin = 10;
const int A3_pin = 9;
const int A4_pin = 8;
const int SR_pin = 7;
const int Pulse_pin = 6;
const int delayms = 10;


CmdMessenger cmdMessenger = CmdMessenger(Serial);

enum
{
  kWatchdog,
  kAcknowledge,
  kSwitchChannel,
  kError,
  //ConfirmChannel, 
};

void attachCommandCallbacks()
{
  cmdMessenger.attach(OnUnknownCommand);
  cmdMessenger.attach(kWatchdog, OnWatchdogRequest); 
  cmdMessenger.attach(kSwitchChannel, OnChannelSwitch);
    
}

void OnWatchdogRequest()
{
  cmdMessenger.sendCmd(kWatchdog, "517cea54-8f17-4761-b735-094897c20ffd");
}

void OnUnknownCommand()
{
  cmdMessenger.sendCmd(kError,"Command without attached callback");
}

void OnChannelSwitch()
{
  uint16_t channelNumber = cmdMessenger.readInt16Arg();//.readBinArg<uint16_t>();
  bool state = cmdMessenger.readBoolArg();//.readBinArg<bool>();

  if(cmdMessenger.isArgOk()&&channelNumber>0&&channelNumber<=MAX_CHANNEL)
  {
    SwitchChannel(channelNumber, state);
    cmdMessenger.sendCmdStart(kAcknowledge);
    cmdMessenger.sendCmdArg<uint16_t>(channelNumber);
    cmdMessenger.sendCmdArg<bool>(state);
    cmdMessenger.sendCmdEnd();    
  }else
  {
    cmdMessenger.sendCmd(kError,"Wrong channel number");
  }
  
}
void Initialize()
{
  int i;
  for(;i<=MAX_CHANNEL;++i)
  {
    SwitchChannel(i,0);
   }
}

void SwitchChannel(uint16_t number, uint16_t state)
{
  digitalWrite(A0_pin,number&0x01);
  digitalWrite(A1_pin,number&0x02);
  digitalWrite(A2_pin,number&0x03);
  digitalWrite(A3_pin,number&0x03);
  digitalWrite(A4_pin,number&0x05);
  digitalWrite(SR_pin,state);
  digitalWrite(Pulse_pin,HIGH);
  delay(delayms);
  digitalWrite(Pulse_pin,LOW);
  digitalWrite(SR_pin,LOW);
  digitalWrite(A0_pin,LOW);
  digitalWrite(A1_pin,LOW);
  digitalWrite(A2_pin,LOW);
  digitalWrite(A3_pin,LOW);
  digitalWrite(A4_pin,LOW);
}

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  cmdMessenger.printLfCr(false);

  attachCommandCallbacks();

  pinMode(LED_PIN, OUTPUT);
  pinMode(A0_pin, OUTPUT);
  pinMode(A1_pin, OUTPUT);
  pinMode(A2_pin, OUTPUT);
  pinMode(A3_pin, OUTPUT);
  pinMode(A4_pin, OUTPUT);
  pinMode(SR_pin, OUTPUT);
  pinMode(Pulse_pin, OUTPUT);
  
}



bool hasExpired(unsigned long &prevTime, unsigned long interval) {
  if (  millis() - prevTime > interval ) {
    prevTime = millis();
    return true;
  } else     
    return false;
}



void toggleLed()
{
  ledState=!ledState;
  digitalWrite(LED_PIN,ledState?HIGH:LOW);
}


void loop() {
  // put your main code here, to run repeatedly:
    cmdMessenger.feedinSerialData(); 
    if(hasExpired(previousToggleLed,100))
      toggleLed();
  
}
