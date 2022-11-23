import RPi.GPIO as GPIO
import time

m = 2

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)

GPIO.setup(m, GPIO.IN)

# 5v 전원, 근접시 input(m)==0

while True:
    if GPIO.input(m)==0:
        print("1")
    else:
        print("0")
    time.sleep(1)
