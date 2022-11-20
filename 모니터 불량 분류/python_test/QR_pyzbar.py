import cv2
import pyzbar.pyzbar as pyzbar

img = cv2.imread("../Image/a.png") 

decoded = pyzbar.decode(img)

for d in decoded: 
    x, y, w, h = d.rect

    barcode_data = d.data.decode("utf-8")
    barcode_type = d.type
    
    cv2.rectangle(img, (x, y), (x + w, y + h), (0, 0, 255), 2)

    text = '%s (%s)' % (barcode_data, barcode_type)
    cv2.putText(img, text, (x, y), cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 255, 255), 2, cv2.LINE_AA)
    
    print(barcode_data)
    print(barcode_type)

cv2.imshow('img', img)
cv2.waitKey(0)
cv2.destroyAllWindows()
