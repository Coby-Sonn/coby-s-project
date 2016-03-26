""" this script deals with the communication between the python engine and the communication with the server through
session with client
http://stackoverflow.com/questions/15396628/sending-strings-between-to-python-scripts-using-subprocess-pipes"""
import socket

import File_Manager as fm

# Socket class
HOST = "0.0.0.0"
PORT = 12347

# local_python_communication class
IP = "192.168.4.127"
COM_PORT = 12345

class local_python_communication():

    def __init__(self):

        self.local_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    def StartClient(self):

        self.local_socket.connect((IP, COM_PORT))

    def Send(self, data):

        self.local_socket.send(data)

    def Recv(self):

        return self.local_socket.recv(1024)

    def CloseClient(self):

        self.local_socket.close()
        self.socket.close()



class Socket:

    def __init__(self):

        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.gui_socket = ""
        self.addr = ""

    def StartServer(self):

        self.socket.bind((HOST, PORT))
        self.socket.listen(1)

    def Send(self, data):

        self.gui_socket.send(data)

    def Recv(self):

        return self.gui_socket.recv(1024)

    def CloseServer(self):

        self.gui_socket.close()
        self.socket.close()

    def Communicate(self):
        i = 1
        local_socket_obj = local_python_communication()
        local_socket_obj.StartClient()
        while True:
            print i
            self.gui_socket, self.addr = self.socket.accept()
            info = self.Recv()
            print "printed from engine: " + info
            if info != "":
                info = info.split("#")
                state = info[0]
                print "printed from engine: " + state
                message = "none"
                if state == "login":
                    username = info[1]
                    password = info[2]
                    local_socket_obj.Send("GETHASH#" + username)
                    password_hash = local_socket_obj.Recv()
                    print "printed from engine: " + password_hash
                    if password_hash == password:
                        local_socket_obj.Send("GETINFO#" + username)
                        information = local_socket_obj.Recv()  # fname#lname#uid
                        print "printed from engine: " + information
                        self.Send("Signed in#" + information)
                    else:
                        message = "Not#0"
                        self.Send(message)
                        print "printed from engine: " + message

                elif state == "Unlock":
                    uid = info[1]
                    path = info[2]
                    if path.endswith(".cb"):
                        file_obj = fm.File_Manager(uid)
                        file_id = file_obj.get_file_id(path)
                        local_socket_obj.Send("GETKEYFOR#" + str(file_id))
                        original_key = local_socket_obj.Recv()
                        ack = file_obj.Strip_File(path, original_key)
                        if ack == "File unlocked":
                            message = ack
                        elif ack == "The specified user is not allowed to open the file":
                             self.Send(ack)
                    else:
                         self.Send("Path error, system can only unlock .cb files")


                elif state == "Lock":
                    local_socket_obj.Send("GETALLUSERINFO#")
                    print "printed from engine: sent request for user info"
                    all_user_info = local_socket_obj.Recv()
                    print "printed from engine: " + all_user_info
                    self.Send(all_user_info)  # sends to gui
                    info = self.Recv()  # info = LockReady#uid#path#otheruid@otheruid@...#rbac#optionality
                                        # received back from the gui
                    print "printed from engine: " + info
                    info = info.split("#")
                    if info[0] == "LockReady":
                        uid = info[1]
                        path = info[2]
                        uid_list = info[3].split("@")
                        uid_list = uid_list[:-1]
                        rbac = info[4]
                        optionality = info[5]  # 0 or 1
                        file_obj = fm.File_Manager(uid)
                        if optionality == "1":
                            second_uid_list = info[6].split("@")
                            second_rbac = info[7]
                            file_data = file_obj.Create_New_Format(path, uid_list, rbac, second_uid_list, second_rbac)
                        else:
                            file_data = file_obj.Create_New_Format(path, uid_list, rbac)
                        local_socket_obj.Send("LOCKEDFILEDATA: " + str(file_data[0]) + "#" + str(file_data[1]))
                        ack = local_socket_obj.Recv()
                        if ack == "Locked":
                            self.Send(ack)

                i += 1
            else:
                break

socket_obj = Socket()
socket_obj.StartServer()
socket_obj.Communicate()

socket_obj.CloseServer()















