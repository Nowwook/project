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

try :
    cnt = 1
    while True:
        distanceCm = distance()
        if(distanceCm >=0) and (distanceCm<10):
            # 사진 촬영     
            camera.start_preview()
            sleep(2)
            #camera.capture('./img/1.jpg')
            camera.capture(str(cnt) +'.jpg')
            camera.stop_preview()
            
            #########################          
            # 사진 읽기
            #img = cv2.imread('./img/1.jpg') # 사진 읽기
            img = cv2.imread(str(cnt) +'.jpg') # 사진 읽기
            det = cv2.QRCodeDetector()      # QR 스캔 클래스
            info, box_coordinates, _ = det.detectAndDecode(img)
            if box_coordinates is None:     # 스캔결과 유무
                
                print('No Code')
                
            else:
                print(info)                 # 스캔결과 출력
                print(type(int(info)))
            
            
            
            cnt+=1
        
        print("cm : ", distanceCm)
        time.sleep(0.5)
        
except KeyboardInterrupt :
    pass

GPIO.cleanup()
