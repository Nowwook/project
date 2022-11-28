# sudo apt-get install python-pygame

import RPi.GPIO as GPIO	# 모듈 선언 (  as pg <= 모듈을 pg로 이름 설정 )
import time
import pygame as pg

GPIO.setmode(GPIO.BOARD)		# 핀번호 기준 정하기, 그림 1 참조

GPIO.setup(3,GPIO.IN)   		# 3번 핀으로 C58 조도센서 출력값 들어오게 설정

GPIO.setwarnings(False) 		# setwarning false 오류가 뜨면 작성

pg.mixer.init()    			# mixer 모듈 초기화

try:
    while True:     		# 반복문
        a = GPIO.input(3)   	# a에 조도센서 출력값 넣기 (어두우면1 ,밝으면 0)
        if a == 0:          	# a가 0 이면 코드 실행
            pg.mixer.music.load("1.mp3")		# 같은 폴더 내 1.mp3 파일
            pg.mixer.music.play(1)          	# 1번재생
            time.sleep(5)                   		# 재생 시간(5초) 기다려주기
            
            pg.mixer.music.load("2.mp3")
            pg.mixer.music.play(1)
            time.sleep(10)                  		# 재생 시간(5초) 기다리고 5초 더 정지
            
            pg.mixer.music.load("4.mp3")
            pg.mixer.music.play(1)
            time.sleep(5)
            
            pg.mixer.music.load("5.mp3")
            pg.mixer.music.play(1)
            time.sleep(10)
            
except KeyboardInterrupt:
    pass
GPIO.cleanup()    
