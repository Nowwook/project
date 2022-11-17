from threading import Thread
import time

import socket_client
import camera
import conveyor
import sensor

def running():
    try:
        while True:
            time.sleep( 1 )
            recv_data_d = socket_client.recv_d()
            if recv_data_d == "REQUEST_RESULT":
                CLASSIFY_RESULT_d = camera.capture(str("stuck1"))
                socket_client.send(CLASSIFY_RESULT_d)
                # conveyor.moving(CLASSIFY_RESULT)
                socket_client.send("ROLLING_END")
    
    except Exception as e:
        print( type(e) )


if __name__ == "__main__":
    print( "main" )
    socket_client.connect_d('192~~, 8000 )
    
    main_th = Thread( target=running )
    main_th.start()
