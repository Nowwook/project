import RPi.GPIO as GPIO
import time

TrigPin = 16
EchoPin = 20

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)

GPIO.setup(TrigPin, GPIO.OUT)
GPIO.setup(EchoPin, GPIO.IN)

def distance():   
    GPIO.output(TrigPin, True)
    time.sleep(0.00001)
    GPIO.output(TrigPin, False)
    
    while GPIO.input(EchoPin) == 0:
        start_time = time.time()
        
    while GPIO.input(EchoPin) == 1 :
        end_time = time.time()
        
    duration = end_time - start_time
    
    distanceCm = duration* 17000
    distanceCm = round(distanceCm, 2)
    return distanceCm

GPIO.cleanup()
