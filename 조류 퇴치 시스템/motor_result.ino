#include <Stepper.h>
#include <Wire.h>
#include <BH1750FVI.h>

BH1750FVI::eDeviceMode_t DEVICEMODE = BH1750FVI::k_DevModeContHighRes;
BH1750FVI LightSensor(DEVICEMODE);

const int Steps = 4800;       

Stepper myStepper(Steps, 8, 9, 10, 11);

int A =0;

void setup() 
{
  Serial.begin(9600);
  LightSensor.begin();
  myStepper.setSpeed(12);	   // rpm
}

void loop() 
{
  uint16_t lux = LightSensor.GetLightIntensity();
  delay(250);

  if(lux >= 10)    // 빛 세기가 10 이상이면 실행
  {
      if(A<2)
     {  
       myStepper.step(Steps);   // 전진 후 5초 대기 
       delay(5000);
       A+=1;
     }
      else
     {
       myStepper.step(-Steps);  // 후진 후 5초 대기 
       delay(5000);
       A+=1;
       if(A==4)
       {
         A=0;
       } 
     }
  }
}
