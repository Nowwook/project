import RPi.GPIO as GPIO
import time

m = 2

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)

GPIO.setup(m, GPIO.IN)

# 5v 전원

while True:
    if GPIO.input(m)==1:
        print("감지 O")
    else:
        print("감지 X")
    time.sleep(1)
