import cv2

img = cv2.imread("./image/hot2.png")       # 사진 읽기
det = cv2.QRCodeDetector()      # QR 스캔 클래스
info, box_coordinates, _ = det.detectAndDecode(img)     # 스캔하기
                            # detectandDecode() 함수를 사용, 이미지에서 QR 코드를 스캔하고 디코딩

if box_coordinates is None:     # 스캔결과 유무
    print('No Code')
else:
    print(info)                 # 스캔결과 출력
    #print(type(int(info)))

if box_coordinates is not None:     # 결과 있을시 QR 영역에 경계표시
    box_coordinates = [box_coordinates[0].astype(int)]
    n = len(box_coordinates[0])
    for i in range(n):
        cv2.line(img, tuple(box_coordinates[0][i]), tuple(box_coordinates[0][(i+1) % n]), (0,255,0), 3)


cv2.imshow('Output', img)
cv2.waitKey(0)
cv2.destroyAllWindows()
