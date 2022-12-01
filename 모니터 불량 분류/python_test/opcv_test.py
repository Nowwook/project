import cv2
import numpy as np

a = str("bb")
img = cv2.imread("../Image/"+a+".png")

hsv_img = cv2.cvtColor(img, cv2.COLOR_BGR2HSV) 
# image_gray = cv2.imread("../Image/"+a+".png", cv2.IMREAD_GRAYSCALE) 
bound_lower = np.array([0,0,0]) 
bound_upper = np.array([100, 255, 255]) 

mask = cv2.inRange(hsv_img, bound_lower, bound_upper) 

kernel = np.ones((5,5),np.uint8) 
mask = cv2.morphologyEx(mask, cv2.MORPH_CLOSE, kernel)
mask = cv2.morphologyEx(mask, cv2.MORPH_OPEN, kernel)

seg_img = cv2.bitwise_and(img, img, mask=mask)
    
blur = cv2.GaussianBlur(img, ksize=(5,5), sigmaX=0)
edged = cv2.Canny(blur, 50, 250)
cv2.imshow('Edged', edged)
cv2.waitKey(0)

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
      
y_min, y_max = 0,0
value = list()
for i in range(len(contours_xy)):
   for j in range(len(contours_xy[i])):
      value.append(contours_xy[i][j][0][1]) 
      y_min = min(value)
      y_max = max(value)

if (x_max - x_min) <=1000:
   x_min=30
   y_min=30

img_trim = img[y_min+20:y_min+480, x_min+30:x_min+1100]
bgr = img_trim[:,:,:3]

CLASSIFY_RESULT = 4

cv2.imshow("Image1", bgr)
cv2.waitKey(0)

color_mean = cv2.mean(bgr)
print(color_mean)

aver = sum(color_mean)/3
print(aver)
    
# if max(color_mean)<aver+10 :
        
# if round(color_mean[0])==round(color_mean[1])==round(color_mean[2]):
if max(color_mean)<aver+10 :
    if color_mean[0]<25:
        CLASSIFY_RESULT = 3     # hot
    else:
        CLASSIFY_RESULT = 1     # dead
        out = bgr.copy()
        bgr = 255 - out
        
    blurred = cv2.GaussianBlur(bgr, (5, 5), 0)

    gray = cv2.cvtColor(blurred, cv2.COLOR_BGR2GRAY)

    ret, thresh = cv2.threshold(gray, 10, 250, cv2.THRESH_BINARY)
    thresh = cv2.erode(thresh, None, iterations=2)

    contours = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
    
else:
    if round(color_mean[0]) > (round(color_mean[1]) and round(color_mean[2])): 
        mask = cv2.inRange( bgr, (0,0,0), (255,50,120))
        bgr[mask==255] = (0,0,0)
        mask = cv2.inRange( bgr, (0,0,0), (255,120,50))
        bgr[mask==255] = (0,0,0)
    if round(color_mean[1]) > (round(color_mean[0]) and round(color_mean[2])): 
        mask = cv2.inRange( bgr, (0,0,0), (120,255,50))
        bgr[mask==255] = (0,0,0)    
        mask = cv2.inRange( bgr, (0,0,0), (50,255,120))
        bgr[mask==255] = (0,0,0)
    if round(color_mean[2]) > (round(color_mean[1]) and round(color_mean[0])): 
        mask = cv2.inRange( bgr, (0,0,0), (120,50,255))
        bgr[mask==255] = (0,0,0)
        mask = cv2.inRange( bgr, (0,0,0), (50,120,255))
        bgr[mask==255] = (0,0,0)

    blurred = cv2.GaussianBlur(bgr, (5, 5), 0)

    gray = cv2.cvtColor(blurred, cv2.COLOR_BGR2GRAY)

    ret, thresh = cv2.threshold(gray, 10, 250, cv2.THRESH_BINARY)
    thresh = cv2.erode(thresh, None, iterations=2)

    contours = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

cv2.imshow("Image2", gray)
cv2.waitKey(0)
    
if len(contours) == 2:
   contours = contours[0]
elif len(contours) == 3:
   contours = contours[1]

cnt = len(contours)
color_mean2 = cv2.mean(bgr)
if color_mean2[0]>250 and color_mean2[1]>250 and color_mean2[2]>250:
   cnt = cnt-1

print(color_mean2)
print('cnt:', cnt)

if cnt<=0:
   CLASSIFY_RESULT=2
      
print('CLASSIFY_RESULT: ',CLASSIFY_RESULT)
