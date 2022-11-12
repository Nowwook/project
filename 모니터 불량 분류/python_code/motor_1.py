import RPi.GPIO as GPIO
import time
from collections import deque

GPIO.setwarnings(False)

GPIO.setmode(GPIO.BOARD)

AIN1=5
BIN1=7
AIN2=11
BIN2=13
a=3
b=15

# # L298N 듀얼 H 브리지 최대 20W

sig=deque([1,0,0,0])

GPIO.setup(AIN1,GPIO.OUT,initial=GPIO.LOW)
GPIO.setup(BIN1,GPIO.OUT,initial=GPIO.LOW)
GPIO.setup(AIN2,GPIO.OUT,initial=GPIO.LOW)
GPIO.setup(BIN2,GPIO.OUT,initial=GPIO.LOW)

GPIO.setup(a,GPIO.OUT,initial=GPIO.LOW)
GPIO.setup(b,GPIO.OUT,initial=GPIO.LOW)

# s=각도  r=회전방향
def motor(s,r):
    GPIO.output(a,1)
    GPIO.output(b,1)
    t = int(s*5/9)
    for cnt in range(0,t):
        GPIO.output(AIN1,sig[0])
        GPIO.output(BIN1,sig[1])
        GPIO.output(AIN2,sig[2])
        GPIO.output(BIN2,sig[3])
        time.sleep(0.01)
        
        sig.rotate(r)   # 데크를 num만큼 회전(양수면 오른쪽, 음수면 왼쪽)
    GPIO.output(a,0)
    GPIO.output(b,0)

try:
    motor(45,1)
    motor(45,-1)

        
except KeyboardInterrupt:
    pass
GPIO.cleanup()
 
