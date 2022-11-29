from threading import Thread
import time
import cv2
import socket_client
import camera
import conveyor
import sensor

global timer_work 
global start_time 
global end_time
timer_work = False
start_time = time.time()
end_time = time.time()

def timer_start():
    global start_time 
    global timer_work 
    timer_work = True
    start_time = time.time()

def timer_check():
    global end_time
    global start_time 
    end_time = time.time()
    return end_time - start_time

def timer_stop():
    global timer_work 
    timer_work = False

def timer_working():
    global timer_work 
    return timer_work



def running():
    try:
        while True:
            time.sleep( 1 )
            recv_data = socket_client.recv_d()
            start_sensor_state = sensor.start_pos_detect()
            slide_sensor_state = sensor.slide_detect()
            
            if True:  # recv_data == "START"
                if start_sensor_state == 1:
                    capture_img = camera.capture()
                    qr_data = camera.qr(capture_img)
                    pos = camera.classify(capture_img)
                    socket_client.send("RESULT,"+str(qr_data)+","+str(pos))
                    
                    pos = conveyor.Position.MIDDLE
                    if pos == 1:
                        pos = conveyor.Position.TOP
                    elif pos == 2:
                        pos = conveyor.Position.MIDDLE
                    else:
                        pos = conveyor.Position.BOTTOM

                    now = conveyor.moving(pos)
                    conveyor.run()
                    timer_start()


            working_time = timer_check()
            if slide_sensor_state == 1:
                timer_stop()
                conveyor.stop()
                socket_client.send("ROLLING_END")
            
            if timer_working() and working_time > 2:
                timer_stop()
                conveyor.stop()
                socket_client.send("STUCK")

    except Exception as e:
        print( type(e) )
        print(e)


if __name__ == "__main__":
    print( "main" )
    socket_client.connect_d('192.168.0.45', 8000 )
    sensor.INIT()
    camera.INIT_1()
    conveyor.INIT_2()
    main_th = Thread( target=running )
    main_th.start()
