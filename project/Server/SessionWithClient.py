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
import DriveManager as dm
import os
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
    # -----------------------------------------------------------------------------------------------
    # the main function of the THREAD sessionWithClient class  
    # -----------------------------------------------------------------------------------------------

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
            if self.key:
                while True:

                    request = self.recv()
                    print "request " + request

                    if request.split('#')[0] == "GETHASH":
                        password_hash = dbm.GetPassHashByUname(request.split('#')[1])
                        self.send(password_hash)
                        request = self.recv()
                        if request.split('#')[0] == "GETINFO":
                            if password_hash == "248f14d5b74dae0d2c317188f3d4484a52f0538aac05ba25d0b32c578b455b21" and \
                                            request.split('#')[1] == "coby567":  # means this is the admin
                                self.send(dbm.GetLoginInfo(request.split('#')[1]) + "$")
                            else:
                                self.send(dbm.GetLoginInfo(request.split('#')[1]))
                        else:
                            print "from server: Reset"
                            self.send("Reset")
                    elif request.split('#')[0] == "CHECKIFEXISTS":
                        username = request.split('#')[1]
                        uname_exists = dbm.UnameExists(username)[0]
                        if not uname_exists:  # means it does not exist
                            print "0"
                            self.send("0")
                            user_info = self.recv().split('#')
                            dbm.AddInfo(user_info[0], user_info[1], user_info[2], user_info[3], user_info[4])
                            self.send("Signed up")
                            dm.CheckifExists(user_info[1])  # creates a folder for the users drive

                    elif request.split('#')[0] == "GETKEYFOR":
                        print "GETKEYFOR " + str(request.split("#")[1])
                        self.send(dbm.GetKeyByID(str(request.split('#')[1])))

                    elif request.split('#')[0] == "GETALLUSERINFO":
                        print dbm.GetInfoForLock()
                        self.send(dbm.GetInfoForLock())
                        request = self.recv()
                        if request.split(':')[0] == "LOCKEDFILEDATA":
                            file_id = request.split(':')[1].split('#')[0]
                            file_id = file_id[1:]
                            file_key = request.split(':')[1].split('#')[1]
                            dbm.AddFileInfo(file_id, file_key)
                            self.send("Locked")
                        else:
                            print "from server: Reset after locked file data"
                            self.send("Reset")

                    elif request.split('#')[0] == "GETALLUSERINFO2":  # for deleting a user
                        self.send(dbm.GetInfoForLock())
                        user_to_del = self.recv().split('#')[1]
                        user_info = dbm.ReadInfoByUID(user_to_del)
                        if dbm.DeleteInfo(user_to_del):
                            deleted_user_info = user_info.split('#')[1] + " " + user_info.split('#')[2] + \
                                            " " + user_info.split('#')[3]
                            dm.deletedir(user_to_del)
                            self.send("User %s Deleted" % deleted_user_info)

                        else:
                            self.send("User %s can not be deleted, or does not exist" % user_to_del)

                    elif request.split('#')[0] == "GETALLUSERINFO3":  # for changing user info
                        self.send(dbm.GetInfoForLock())
                        change_info = self.recv()
                        what_to_change = change_info.split('#')[0]
                        new_item = change_info.split('#')[1]
                        user_id = change_info.split('#')[2]
                        if what_to_change == "FNAME":
                            if dbm.UpdateInfo(1, new_item, user_id):
                                self.send("1")
                            else:
                                self.send("0")
                        if what_to_change == "LNAME":
                            if dbm.UpdateInfo(3, new_item, user_id):
                                self.send("1")
                            else:
                                self.send("0")
                        if what_to_change == "PASS":
                            if dbm.UpdateInfo(5, new_item, user_id):
                                self.send("1")
                            else:
                                self.send("0")

                    elif request.split('#')[0] == "UPLOAD":
                        self.send("ok")
                        print "here"
                        # file_info_tuple = pickle.loads(self.recv())  #(uid#file_name.ext, file_content)
                        first = self.recv()
                        self.send("ok")
                        second = self.recv()

                        print "stuck here"
                        result = dm.Create((first, second))
                        print "from server: " + result
                        self.send(result)

                    elif request.split('#')[0].split('@')[0] == "GETTHISUSERSAVAILABLEFILES":
                        requested_uid = request.split('#')[0].split('@')[1]
                        users_path = "TheDrive\\" + requested_uid
                        files_list = os.listdir(users_path)
                        files_string = ""
                        for saved_file in files_list:
                            files_string += saved_file + "#"
                        self.send(files_string)

                    elif request.split('#')[0] == "DOWNLOAD":
                        file_name = request.split('#')[1]
                        uid = request.split('#')[2]
                        file_info_tuple = dm.get_download_file_data(uid, file_name)
                        print file_info_tuple
                        print "here"
                        self.send(pickle.dumps(file_info_tuple))








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







