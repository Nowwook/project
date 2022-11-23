import RPi.GPIO as GPIO
import time

TrigPin_1 = 16
EchoPin_1 = 20
# TrigPin_2 = 16
# EchoPin_2 = 20
m = 15

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)

GPIO.setup(TrigPin_1, GPIO.OUT)
GPIO.setup(EchoPin_1, GPIO.IN)
# GPIO.setup(TrigPin_2, GPIO.OUT)
# GPIO.setup(EchoPin_2, GPIO.IN)

def distance_1():   
    GPIO.output(TrigPin_1, True)
    time.sleep(0.00001)
    GPIO.output(TrigPin_1, False)
    
    while GPIO.input(EchoPin_1) == 0:
        start_time = time.time()
        
    while GPIO.input(EchoPin_1) == 1 :
        end_time = time.time()
        
    duration = end_time - start_time
    
    distanceCm = duration* 17000
    distanceCm = round(distanceCm, 2)
    return distanceCm

def distance_2():   
    GPIO.output(TrigPin_2, True)
    time.sleep(0.00001)
    GPIO.output(TrigPin_2, False)
    
    while GPIO.input(EchoPin_2) == 0:
        start_time = time.time()
        
    while GPIO.input(EchoPin_2) == 1 :
        end_time = time.time()
        
    duration = end_time - start_time
    
    distanceCm = duration* 17000
    distanceCm = round(distanceCm, 2)
    return distanceCm

def distance_3():
    GPIO.setmode(GPIO.BOARD)
    GPIO.setup(m, GPIO.IN)
    return GPIO.input(m)
    
GPIO.cleanup()
