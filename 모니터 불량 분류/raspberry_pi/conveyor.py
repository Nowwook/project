import RPi.GPIO as GPIO
import time
from collections import deque
import sensor
from enum import Enum

AIN1=3
BIN1=7
AIN2=5
BIN2=11
con = 13

sig=deque([1,0,0,0])

def INIT_2():
    GPIO.setwarnings(False)
    GPIO.setmode(GPIO.BOARD)
    GPIO.setup(AIN1,GPIO.OUT,initial=GPIO.LOW)
    GPIO.setup(BIN1,GPIO.OUT,initial=GPIO.LOW)
    GPIO.setup(AIN2,GPIO.OUT,initial=GPIO.LOW)
    GPIO.setup(BIN2,GPIO.OUT,initial=GPIO.LOW)
    GPIO.setup(con,GPIO.OUT,initial=GPIO.LOW)


class Position(Enum):
    TOP = 1
    MIDDLE = 2
    BOTTOM = 3

currnet_pos = Position.MIDDLE

def set_default_postion():
    currnet_pos = Position.MIDDLE

def motor(s,r):
    t = int(s*5/9)
    for cnt in range(0,t):
        GPIO.output(AIN1,sig[0])
        GPIO.output(BIN1,sig[1])
        GPIO.output(AIN2,sig[2])
        GPIO.output(BIN2,sig[3])
        time.sleep(0.01)
        
        sig.rotate(r)

def moving( _position:Position ):
    move_way = currnet_pos - _position
    motor( 45, move_way )
    currnet_pos = _position
    
def run():
    GPIO.output(con,1)

def stop():
    GPIO.output(con,0)
