""" this script deals with the communication between the python engine and the communication with the server through
session with client
http://stackoverflow.com/questions/15396628/sending-strings-between-to-python-scripts-using-subprocess-pipes"""
import socket
import sys
import File_Manager as fm

def pipe_send(message):

    sys.stdout.write(message + "\n")
    sys.stdout.flush()
def pipe_recv():

    return sys.stdin.readline()

HOST = "0.0.0.0"
PORT = 1234

class Socket:
    def __init__(self):
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
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
        while True:
            self.gui_socket, self.addr = self.socket.accept()
            info = self.Recv()
            info = info.split("#")
            state = info[0]
            print state
            message = ""
            if state == "login":
                username = info[1]
                password = info[2]
                pipe_send("GETHASH#" + username)
                password_hash = pipe_recv()
                if password_hash == password:
                    pipe_send("GETINFO#" + username)
                    information = pipe_recv()  # fname#lname#uid
                    self.Send("Signed in#" + information)
                else:
                    message = "Not#0"
                    self.Send(message)
            elif state == "Unlock":
                uid = info[1]
                path = info[2]
                if path.endswith(".cb"):
                    file_obj = fm.File_Manager(uid)
                    file_id = file_obj.get_file_id(path)
                    pipe_send("GETKEY FOR#" + str(file_id))
                    original_key = pipe_recv()
                    ack = file_obj.Strip_File(path, original_key)
                    if ack == "File unlocked":
                        message = ack
                    elif ack == "The specified user is not allowed to open the file":
                        message = ack
                else:
                    message = "path error, can only unlock .cb files"
                self.Send(message)
            elif state == "Lock":
                pipe_send("GETALLUSERINFO")
                all_user_info = pipe_recv()
                self.Send(all_user_info)  # sends to gui
                info = self.Recv()  # info = LockReady#uid#path#otheruid@otheruid@...#rbac#optionality
                                    # received back from the gui
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
                    message = "Locked"
                    self.Send(message)
                    pipe_send("LOCKEDFILEDATA: " + str(file_data[0]) + "#" + str(file_data[1]))

socket_obj = Socket()
socket_obj.StartServer()
socket_obj.Communicate()
















