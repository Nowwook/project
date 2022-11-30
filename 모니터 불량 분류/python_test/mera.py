import cv2
import RPi.GPIO as GPIO

capture = cv2.VideoCapture(0)
capture.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)
capture.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)

while True:
    ret, frame = capture.read()  
    cv2.imshow("original", frame)  
    if cv2.waitKey(1) == ord('q'): 
            break

capture.release()             
cv2.destroyAllWindows()


# con = 19
# on = 21
# GPIO.setwarnings(False)
# GPIO.setmode(GPIO.BOARD)
# GPIO.setup(con,GPIO.OUT,initial=GPIO.LOW)
# GPIO.setup(on,GPIO.OUT,initial=GPIO.LOW)

# GPIO.output(con,0)
# GPIO.output(on,0)