#include <Stepper.h>
#include <Wire.h>
#include <BH1750FVI.h>

BH1750FVI::eDeviceMode_t DEVICEMODE = BH1750FVI::k_DevModeContHighRes;
BH1750FVI LightSensor(DEVICEMODE);
const int stepsPerRevolution = 4800;
Stepper myStepper(stepsPerRevolution, 8, 9, 10, 11);
int A =0;

void setup() {
  Serial.begin(9600);
  LightSensor.begin();
  myStepper.setSpeed(12);
}

void loop() {
  uint16_t lux = LightSensor.GetLightIntensity();
  delay(250);
  if(lux <= 10)
  {  
  }
  else
  {
    if(A<2)
    {  
      myStepper.step(stepsPerRevolution);
      delay(5000);
      A+=1;
    }
    else
    {
      myStepper.step(-stepsPerRevolution);
      delay(5000);
      A+=1;
      if(A==4)
      {
        A=0;
      }
    }
  }
}
