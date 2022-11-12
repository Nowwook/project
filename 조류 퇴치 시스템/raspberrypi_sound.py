import time
import pygame as p

# 다운 sudo apt-get install python3-pygame
  
p.mixer.init()
# 모듈 초기화

# -1 무한 ,2 2번

# 음향 조절 0~1
# p.mixer.music.set_volume(1)

while True:
    p.mixer.music.load( "2.mp3" )
    p.mixer.music.play(2)
    time.sleep(15)
    p.mixer.music.load( "1.mp3" )
    p.mixer.music.play(2)
    time.sleep(15)

# 끝까지 재생할때까지 기다린다.
# if(p.mixser.music.get_busy()==False): 
