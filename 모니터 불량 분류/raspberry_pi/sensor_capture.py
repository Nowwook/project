import cv2
import RPi.GPIO as GPIO
import time
from picamera import PiCamera
from time import sleep

camera = PiCamera()

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

def detected():
    try :
        while True:
            distanceCm = distance()
            if(distanceCm >=0) and (distanceCm<10):
                camera.start_preview()
                sleep(2)
                camera.capture('./img/capture.png')
                camera.stop_preview()
                
                img = cv2.imread('./img/capture.png') 
                det = cv2.QRCodeDetector()  
                info, box_coordinates, _ = det.detectAndDecode(img)
                if box_coordinates is None: 
                    print('No Code')
                else:
                    print(info) 
            print("cm : ", distanceCm)
            time.sleep(0.5)
            
    except KeyboardInterrupt :
        pass

    GPIO.cleanup()
    return False
