#include "CmdMessenger.h"

#define MAX_CHANNEL 32
unsigned long previousToggleLed = 0;
bool ledState = 0;
uint16_t currentChannel = 1;

//const int LED_PIN = 13;
const int A0_pin = 2;
const int A1_pin = 4;
const int A2_pin = 5;
const int A3_pin = 6;
const int A4_pin = 7;
const int SR_pin = A4;
const int Pulse_pin = A5;
const int delayms = 10;


const int ChAdir = 12;
const int ChBdir = 13;

const int ChAbrake = 9;
const int ChBbrake = 8;

const int ChApwm = 3;
const int ChBpwm = 11;

CmdMessenger cmdMessenger = CmdMessenger(Serial);

enum
{
  kWatchdog,
  kAcknowledge,
  kSwitchChannel,
  kError,
  kMotorCommand
  //ConfirmChannel, 
};

void attachCommandCallbacks()
{
  cmdMessenger.attach(OnUnknownCommand);
  cmdMessenger.attach(kWatchdog, OnWatchdogRequest); 
  cmdMessenger.attach(kSwitchChannel, OnChannelSwitch);
  cmdMessenger.attach(kMotorCommand, OnMotorCommand);  
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
  uint16_t channelNumber = cmdMessenger.readInt16Arg();//.readBinArg<uint16_t>();  ////
  bool state = cmdMessenger.readBoolArg();//.readBinArg<bool>();// 

  bool isArgOk = cmdMessenger.isArgOk();
  if(isArgOk&&channelNumber>0&&channelNumber<=MAX_CHANNEL)
  {
    SwitchChannel(channelNumber, state);
    cmdMessenger.sendCmdStart(kAcknowledge);
    cmdMessenger.sendCmdArg<uint16_t>(channelNumber);
    cmdMessenger.sendCmdArg<bool>(state);
    cmdMessenger.sendCmdEnd();    
  }else
  {
    
    cmdMessenger.sendCmd(kError,"Error. ArgIsOk:"+String(isArgOk)+"Ch:"+String(channelNumber)+"St:"+String(state));
  }
  
}

void OnMotorCommand()
{
  int16_t channelNumber = cmdMessenger.readInt16Arg();//.readBinArg<uint16_t>();
  int16_t channelSpeed = cmdMessenger.readInt16Arg();//.readBinArg<bool>();

  if(cmdMessenger.isArgOk()&&channelNumber>0&&channelNumber<=2)
  {
    MoveMotor(channelNumber, channelSpeed);
    cmdMessenger.sendCmdStart(kAcknowledge);
    cmdMessenger.sendCmdArg<int16_t>(channelNumber);
    cmdMessenger.sendCmdArg<int16_t>(channelSpeed);
    cmdMessenger.sendCmdEnd();    
  }else
  {
    cmdMessenger.sendCmd(kError,"Wrong channel number");
  }
}



void MoveMotor(int16_t channel, int16_t cspeed)
{
    if(channel == 1)
    {
      if(cspeed==0)
      {
        digitalWrite(ChAbrake, HIGH);
      }else if(cspeed<0)
      {
        digitalWrite(ChAdir, HIGH);
        digitalWrite(ChAbrake,LOW);
        cspeed*=-1;
        if(cspeed>255)cspeed = 255;
        analogWrite(ChApwm,cspeed);
      } else if(cspeed>0)
      {
         digitalWrite(ChAdir, LOW);
         digitalWrite(ChAbrake,LOW);
         if(cspeed>255)cspeed = 255;
        analogWrite(ChApwm,cspeed);
      }
      
      
     }
     else
     {
       if(cspeed==0)
      {
        digitalWrite(ChBbrake, HIGH);
      }else if(cspeed<0)
      {
        digitalWrite(ChBdir, HIGH);
        digitalWrite(ChBbrake,LOW);
        cspeed*=-1;
        if(cspeed>255)cspeed = 255;
        analogWrite(ChBpwm,cspeed);
      } else if(cspeed>0)
      {
         digitalWrite(ChBdir, LOW);
         digitalWrite(ChBbrake,LOW);
         if(cspeed>255)cspeed = 255;
        analogWrite(ChBpwm,cspeed);
      }  
     }
}


/*
const int ChAdir = 12;
const int ChBdir = 13;

const int ChAbrake = 9;
const int ChBbrake = 8

const int ChApwm = 3;
const int ChBpwm = 11;
*/
void Initialize()
{
  int i;
  for(i=1;i<=MAX_CHANNEL;++i)
  {
    SwitchChannel(i,0);
  }
}
//currentChannel
void SwitchChannel(uint16_t number, uint16_t state)
{
  state = !state;
  number = number - 1;// channel numeration starts from 1
  digitalWrite(A0_pin,number&0x01);
  digitalWrite(A1_pin,number&0x02);
  digitalWrite(A2_pin,number&0x04);
  digitalWrite(A3_pin,number&0x08);
  digitalWrite(A4_pin,number&0x10);
  digitalWrite(SR_pin,state);
  digitalWrite(Pulse_pin,HIGH);
  delay(delayms);
  digitalWrite(Pulse_pin,LOW);

  //delay(1000);
  digitalWrite(SR_pin,LOW);
  digitalWrite(A0_pin,LOW);
  digitalWrite(A1_pin,LOW);
  digitalWrite(A2_pin,LOW);
  digitalWrite(A3_pin,LOW);
  digitalWrite(A4_pin,LOW);

  delay(100);
}

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  cmdMessenger.printLfCr(false);

  attachCommandCallbacks();

 // pinMode(LED_PIN, OUTPUT);
  pinMode(A0_pin, OUTPUT);
  pinMode(A1_pin, OUTPUT);
  pinMode(A2_pin, OUTPUT);
  pinMode(A3_pin, OUTPUT);
  pinMode(A4_pin, OUTPUT);
  pinMode(SR_pin, OUTPUT);
  pinMode(Pulse_pin, OUTPUT);

  pinMode(ChAdir,OUTPUT);
  pinMode(ChBdir,OUTPUT); 
  pinMode(ChAbrake,OUTPUT);
  pinMode(ChBbrake,OUTPUT);
  pinMode(ChApwm,OUTPUT);
  pinMode(ChBpwm,OUTPUT);
  Initialize();
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
  //digitalWrite(LED_PIN,ledState?HIGH:LOW);
}


void loop() {
  // put your main code here, to run repeatedly:
    cmdMessenger.feedinSerialData(); 
    if(hasExpired(previousToggleLed,100))
      toggleLed();
  
}
