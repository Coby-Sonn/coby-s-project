#region ----------   ABOUT   -----------------------------
"""
##################################################################
# Created By:                                                    #
# Date: 20/09/2014                                               #
# Name: Main Server for Clients                                  #
# Version: 1.0                                                   #
# Windows Tested Versions: Win 7 32-bit                          #
# Python Tested Versions: 2.6 32-bit                             #
# Python Environment  : PyCharm                                  #
##################################################################
"""
# endregion

# region ----------   IMPORTS   -----------------------------
import threading,socket, sys, os
from SessionWithClient import *
import subprocess
import socket
import time
# endregion


#region -----  CONSTANTS  -----
# For every client to been thread
THREAD_LIMIT = 50
GUI_PORT = 9669
SERVER_ABORT = "Aborting the server..."
#endregion

# region ----------   CLASSES   -----------------------------
# region -----  PythonServer CLASS  -----


class PythonServer(threading.Thread):
    # -----  DATA  -----
    listenerSock = None
    # Dictionary for client connctions : Key - ip  Value - SessionWithClient
    open_clients = {}       
 
    # constructor 
    def __init__(self, gui, listenerPort):
        self.gui = gui
        # self.listenerPort = listenerPort
        self.listenerPort = 6071

        threading.Thread.__init__(self)
                
    # the main thread function
    def run(self):
        #self.gui.guiSock.send("Server running...Waiting for a connection...#")   # to GUI
        try:
            # Listener socket
            listenerSock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            listenerSock.bind(("0.0.0.0", self.listenerPort))
            listenerSock.listen(5)
            #self.gui.guiSock.send("Listening to clients...#")   # to GUI

            while True:

                clientSock, addr = listenerSock.accept()
                print "accepted"
                # Thread creating loop
                while True:
                    if threading.activeCount() < THREAD_LIMIT:
                        clientIP = addr[0]  # key - IP client
                        # Check if client connected in pass
                        if clientIP in self.open_clients:
                            # Callback Connection by IP clientIP
                            pass
                        else:
                            # First Connection by IP  clientIP
                            sessionWithClient = SessionWithClient(self, clientSock, addr)
                            sessionWithClient.start()
                        break
        except socket.error, er_msg:
            error_code = er_msg[0]
            if error_code == 10048:
                pass 
            else:
                pass 
        except Exception as er_msg:
            pass 

#endregion

#region ----------   MAIN   -----------------------------

def main(args):
    """
    Description: Main function, connect to target server, establish protocol,

    Input:
         args - list of command line arguments
                1) - listener port
    """

    try:
      
        try:  
           
            PythonServer(None, int(args[0])).start()
        except ImportError:
           
            sys.exit("# pyCrypto module is not installed. " + SERVER_ABORT)
    except socket.errors, e:
            print e    

if __name__ == "__main__":
    
    print "start"
    main('6071') #sys.argv[1:])



#endregion
