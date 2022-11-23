from threading import Thread
import time
import cv2
import socket_client
import camera
import conveyor
import sensor

def running():
    try:
        global now # 벨트 위치
        now = 2
        
        while True:
            time.sleep( 1 )
            recv_data_1 = socket_client.recv_d()
            if recv_data_1 == "START":
                while 1:
                    data_1 = sensor.distance_1()
                    if data_1 <= 10:
                        time.sleep(3) # 놓는 시간
                        capture = camera.capture()
                        qr = camera.qr(capture)
                        CLASSIFY_RESULT_d = camera.classify(capture)
                        
                        # print(qr, CLASSIFY_RESULT_d)
                        socket_client.send("RESULT,"+qr+","+CLASSIFY_RESULT_d)
                        conveyor.moving(CLASSIFY_RESULT_d,now)
                        result_1 = conveyor.run()

                        while 1:
                            recv_data_2 = socket_client.recv_d()
                            if recv_data_2=="DB_END":
                                break
                                
                        if result_1 == 0:
                            socket_client.send("ROLLING_END")
                        else:
                            socket_client.send("STUCK")
                        break

    except Exception as e:
        print( type(e) )


if __name__ == "__main__":
    print( "main" )
    socket_client.connect_d('192.168.0.35', 8000 )
    
    main_th = Thread( target=running )
    main_th.start()
    # running()
