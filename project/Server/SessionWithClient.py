#region ----------   ABOUT   -----------------------------
"""
##################################################################
# Created By:                                     #
# Date: 20/09/2014                                               #
# Name: Server  between GUI and clients                          #
# Version: 1.0                                                   #
# Windows Tested Versions: Win 7 32-bit                          #
# Python Tested Versions: 2.6 32-bit                             #
# Python Environment  : PyCharm                                  #
# pyCrypto Tested Versions: Python 2.7 32-bit                    #
##################################################################
"""
#endregion

#region ----------   IMPORTS   -----------------------------
import threading
import socket
from ServerCrypto import *
import DbManager as dbm
from AES import *
#endregion

#region ----------   C O N S T A N T S  ------------------------------------------------------
PROT_START = "Hello"                                      # Initialization keyword of Protocol Establishment
ERROR_SOCKET = "Socket_Error"                             # Error message If you happened socket error
ERROR_EXCEPT = "Exception"                                # Error message If you happened exception
SERVER_ABORT = "Aborting the server..."
#endregion

#region  -----  SessionWithClient C L A S S  -----
class  SessionWithClient(threading.Thread):
    # -----  D A T A  -----    

    # -----  F U N C T I O N S  -----
    #-----------------------------------------------------------------------------------------------
    #  class definition function
    #-----------------------------------------------------------------------------------------------
    def __init__(self, pythonServer, clientSock, addr ):
        threading.Thread.__init__(self)
        self.crypto = Crypto()
        # reference to parent server
        self.pythonServer = pythonServer
        # new open socket  for client
        self.clientSock = clientSock
        # address connection : IP and Port
        self.addr = addr
        self.key = None
        # Dictionary of ptotocols functions : Key - level  Value - referance to function
 #       self.operFnPtrDict = { 1 : self.oper1Fun, 2 : self.oper1Fun }

    #-----------------------------------------------------------------------------------------------
    # Receive data from input stream from server socket by loop
    # Each step read LEN_UNIT_BUF bytes
    # After loop we want to get only first part of split by '\r\n'
    # Return : content of input stream from server socket
    #-----------------------------------------------------------------------------------------------  
    def recv_buf(self):
        #content=""
        #while True:
        #    data = self.clientSock.recv(LEN_UNIT_BUF)
        #    if not data:  break
        #    content += data
        #print content
        #return content.split(END_LINE)[0]
        return self.clientSock.recv(LEN_UNIT_BUF).split(END_LINE)[0]

    #-----------------------------------------------------------------------------------------------
    # the function for verify Hello at beginning of communication in data
    #-----------------------------------------------------------------------------------------------  
    def verify_hello(self, data):
        if len(data):
            # Verify Hello at beginning of communication
            if not (data == PROT_START):
                self.clientSock.send("Error in protocol establishment ( 'Hello' )" + END_LINE)
                time.sleep(0.5)
                self.clientSock.close()
                return False
            return True
        return False
  
    #-----------------------------------------------------------------------------------------------
    # the main function of the THREAD sessionWithClient class  
    #-----------------------------------------------------------------------------------------------
    def send(self, message):

        self.clientSock.send(encryptAES(self.key, message))

    def recv(self):

        return decryptAES(self.key, self.clientSock.recv(LEN_UNIT_BUF))

    def run(self):

        try:               
            # Wait message beginning of communication from client
            data = self.recv_buf()
            if not self.verify_hello(data):
                return
            self.clientSock.send(PROT_START + END_LINE)
            self.key = self.crypto.key_exchange(self.clientSock)  # in Crypto
            print "here 0"
            if self.key:
                print "here 1"
                while True:
                    request = self.recv()
                    print request
                    if request.split('#')[0] == "GETHASH":
                        password_hash = dbm.GetPassHashByUname(request.split('#')[1])
                        self.send(password_hash)
                        request = self.recv()
                        if request.split('#')[0] == "GETINFO":
                            self.send(dbm.GetLoginInfo(request.split('#')[1]))
                    elif request.split('#')[0] == "GETKEYFOR":
                        self.send(dbm.GetKeyByID(request.split('#')[1]))
                    elif request.split('#')[0] == "GETALLUSERINFO":
                        print dbm.GetInfoForLock()
                        self.send(dbm.GetInfoForLock())
                        request = self.recv()
                        if request.split(':')[0] == "LOCKEDFILEDATA":
                            file_id = request.split(':')[1].split('#')[0]
                            file_key = request.split(':')[1].split('#')[1]
                            dbm.AddFileInfo(file_id, file_key)




            self.clientSock.close()

        except socket.error, e:
            print str(e) + END_LINE + ERROR_SOCKET + "  from " + str(self.addr[0])           
        except Exception as e:
            print str(e) + END_LINE + ERROR_EXCEPT + "  from " + str(self.addr[0])     

    #-----------------------------------------------------------------------------------------------
    #  Operation  1   ---    
    #
    # Description: 
    #-----------------------------------------------------------------------------------------------
    def oper1Fun(self):
       pass   
           
  #endregion







