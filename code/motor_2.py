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

sig=deque([1,0,0,0])

GPIO.setup(AIN1,GPIO.OUT,initial=GPIO.LOW)
GPIO.setup(BIN1,GPIO.OUT,initial=GPIO.LOW)
GPIO.setup(AIN2,GPIO.OUT,initial=GPIO.LOW)
GPIO.setup(BIN2,GPIO.OUT,initial=GPIO.LOW)

GPIO.setup(a,GPIO.OUT,initial=GPIO.LOW)
GPIO.setup(b,GPIO.OUT,initial=GPIO.LOW)

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
        
        sig.rotate(r)  
    GPIO.output(a,0)
    GPIO.output(b,0)

global location
location = 2

# 현재 위치 2, 정상=2, 버림=1, 수리가능=3,4
def moving(CLASSIFY_RESULT):
    Way = -1
    global location
    
    if CLASSIFY_RESULT==4:
        CLASSIFY_RESULT=3
        
    move = CLASSIFY_RESULT - location
    if move<0:
        move = -move
        Way = 1

    if move != 0:
        motor(45*int(move),Way)
        
    location = CLASSIFY_RESULT
    

try:
    time.sleep(1)
    moving(1)
    time.sleep(1)
    moving(3)
    time.sleep(1)
    moving(2)
    time.sleep(1)
    moving(4)

        
except KeyboardInterrupt:
    pass
GPIO.cleanup()
