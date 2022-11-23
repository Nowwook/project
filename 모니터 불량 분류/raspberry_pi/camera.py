import cv2
import numpy as np
import time
from picamera import PiCamera
import RPi.GPIO as GPIO

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BOARD)

on = 19
GPIO.setup(on,GPIO.OUT,initial=GPIO.LOW)

def capture():
    GPIO.output(on,1)
    PiCamera.capture('a.png')
    img = cv2.imread('a.png')
    GPIO.output(on,0)
    return img
    
def qr(img):
    decoded = pyzbar.decode(img)

    for d in decoded: 
        x, y, w, h = d.rect

        barcode_data = d.data.decode("utf-8")
        return barcode_data

def classify(img):
    # image_gray = cv2.imread(img, cv2.IMREAD_GRAYSCALE) 

    blur = cv2.GaussianBlur(img, ksize=(5,5), sigmaX=0)
    edged = cv2.Canny(blur, 50, 250)

    kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (7,7))
    closed = cv2.morphologyEx(edged, cv2.MORPH_CLOSE, kernel)

    contours, _ = cv2.findContours(closed.copy(),cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

    contours_xy = np.array(contours)
    contours_xy.shape
    x_min, x_max = 0,0

    value = list()
    for i in range(len(contours_xy)):
        for j in range(len(contours_xy[i])):
            value.append(contours_xy[i][j][0][0]) 
            x_min = min(value)
            x_max = max(value)
            
    y_min = 0
    value = list()

    for i in range(len(contours_xy)):
        for j in range(len(contours_xy[i])):
            value.append(contours_xy[i][j][0][1]) 
            y_min = min(value)

    if (x_max - x_min) <=1000:
        x_min=30
        y_min=30

    img_trim = img[y_min+30:y_min+610, x_min+30:x_min+1210]
    bgr = img_trim[:,:,:3]

    CLASSIFY_RESULT = 4

    color_mean = cv2.mean(bgr)
    # print(color_mean)
    if color_mean[0]<20 and color_mean[1]<20 and color_mean[2]<20:
        CLASSIFY_RESULT = 3
    if color_mean[0]>240 and color_mean[1]>240 and color_mean[2]>240:
        CLASSIFY_RESULT = 1
    
    
    mask = cv2.inRange( bgr, (0,0,0), (0,255,0))
    bgr[mask==255] = (0,0,0)

    mask = cv2.inRange( bgr, (0,0,0), (255,0,0))
    bgr[mask==255] = (0,0,0)

    mask = cv2.inRange( bgr, (0,0,0), (0,0,255))
    bgr[mask==255] = (0,0,0)

    blurred = cv2.GaussianBlur(bgr, (5, 5), 0)

    gray = cv2.cvtColor(blurred, cv2.COLOR_BGR2GRAY)
    ret, thresh = cv2.threshold(gray, 10, 250, cv2.THRESH_BINARY)
    thresh = cv2.erode(thresh, None, iterations=2)

    contours = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

    if len(contours) == 2:
        contours = contours[0]
    elif len(contours) == 3:
        contours = contours[1]

    cnt = len(contours)
    if color_mean[0]>250 and color_mean[1]>250 and color_mean[2]>250:
        cnt = cnt-1

    # print('cnt:', cnt)

    if cnt==0:
        CLASSIFY_RESULT=2
        
    return CLASSIFY_RESULT
