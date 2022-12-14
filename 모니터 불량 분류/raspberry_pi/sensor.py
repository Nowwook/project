import RPi.GPIO as GPIO
import time

m = 13
n = 15

def INIT():
    GPIO.setwarnings(False)
    GPIO.setmode(GPIO.BOARD)

    GPIO.setup(m, GPIO.IN)
    GPIO.setup(n, GPIO.IN)

def start_pos_detect():
    return GPIO.input(m)

def slide_detect():
    return GPIO.input(n)
