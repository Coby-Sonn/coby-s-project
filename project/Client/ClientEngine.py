""" this script deals with the communication between the python engine and the communication with the server through
session with client

This is the client's part
communicates with gui through HOST & PORT
communicates with the Client communicator (Client.py) through IP & COM_PORT

"""

import socket
import DriveManager as dm
import File_Manager as fm
import pickle
import os
import Error
import subprocess

# Socket class for gui communication
HOST = "0.0.0.0"
PORT = 12244

# LocalPythonCommunication class (with Client.py)
IP = ""  # a local ip, run my_ip() to get it
COM_PORT = 8484

MAX_CONNECTIONS = 3


def my_ip():
    output = subprocess.check_output(['cmd.exe', '/c ipconfig'])
    output=output[output.find("Ethernet adapter Local Area Connection:"):]
    output=output[output.find("IPv4 Address"):]
    output=output[output.find(":")+1:]
    ip=output[:15].strip()
    return ip.split('\r\n')[0]

class LocalPythonCommunication():

    def __init__(self):

        self.local_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    def StartClient(self):
        connected = False
        i = 0
        while not connected and i != 5:
            try:
                self.local_socket.connect((my_ip(), COM_PORT))
                connected = True
            except: i += 1
        if i >= 5:
            Error.error_msg()
            """ look for the ip again, gil sent a func that gets an ip (the servers ip) by the mac address """


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
        local_socket_obj = LocalPythonCommunication()
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
                # message = "none"
                if state == "login":
                    username = info[1]
                    password = info[2]
                    local_socket_obj.Send("GETHASH#" + username)
                    password_hash = local_socket_obj.Recv()
                    print "printed from engine: " + password_hash
                    if password_hash == password:
                        local_socket_obj.Send("GETINFO#" + username)
                        information = local_socket_obj.Recv()  # fname#lname#uid optionaly it will end with '$'
                                                                # depends if the user is the admin
                        print "printed from engine: " + information
                        if information.endswith("$"):
                            self.Send("Signed in, admin#" + information)
                        else:
                            self.Send("Signed in#" + information)
                        ack = self.Recv()
                        if ack == "ok":
                            local_socket_obj.Send("GETTHISUSERSAVAILABLEFILES@" + information.split('#')[1] + "#")
                            available_files = local_socket_obj.Recv()  # -> filename#filename#...
                            self.Send(available_files)

                    else:
                        message = "Not#0"
                        self.Send(message)
                        local_socket_obj.Send("Reset")
                        print "printed from engine: " + message

                elif state == "Unlock":
                    uid = info[1]
                    path = info[2]
                    if path.endswith(".cb"):
                        file_obj = fm.File_Manager(uid)
                        file_id = file_obj.get_file_id(path)
                        local_socket_obj.Send("GETKEYFOR#" + str(file_id))
                        original_key = local_socket_obj.Recv()
                        print "original key from clientengine " + str(original_key)
                        ack = file_obj.Strip_File(path, original_key)
                        print "from clientengine: " + ack
                        if ack == "File unlocked" or ack == "File unlocked, user can only read the file":
                            self.Send(ack)
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

                elif state == "Upload":  # Upload#path#uid
                    print info
                    path = info[1]
                    uid = info[2]
                    file_data_tuple = dm.get_upload_file_data(uid, path)
                    try:
                        local_socket_obj.Send("UPLOAD#")
                        ack = local_socket_obj.Recv()
                        if ack == "ok":
                            print "here---------------------------------------------------------"
                            pickled_info = pickle.dumps(file_data_tuple)
                            local_socket_obj.Send(pickled_info)
                            ack = local_socket_obj.Recv()
                            print "ack: " + ack
                            if ack == "File Successfully Uploaded":
                                os.remove(path)
                                self.Send(ack)
                            else:
                                self.Send("An error has occurred, please try again.")
                        else:
                            self.Send("An error has occurred, please try again.")
                    except:
                        self.Send("An error has occurred, please try again.")

                elif state == "Download":
                    file_name = info[1]
                    uid = info[2]
                    download_path = info[3]
                    local_socket_obj.Send("DOWNLOAD#" + file_name + "#" + uid)
                    file_info_tuple = pickle.loads(local_socket_obj.Recv())
                    self.Send(dm.Create(file_info_tuple, download_path))

                elif state == "register":
                    while state == "register":
                        print info
                        uid = info[1]
                        firstname = info[2]
                        lastname = info[3]
                        username = info[4]
                        password = info[5]
                        local_socket_obj.Send("CHECKIFEXISTS#" + username)
                        uname_exists = local_socket_obj.Recv()  # 0 = does not exist, 1 = does exist
                        if uname_exists == "0":
                            print "sending the new user's data"
                            local_socket_obj.Send(firstname + "#" + uid + "#" + lastname + "#" + username + "#" +
                                                  password)
                            message = local_socket_obj.Recv()
                            self.Send(message)
                            state = ""
                        else:
                            self.Send("Username already exists")
                            state = ""
                        # info = self.Recv()
                        # info = info.split("#")
                        # state = info[0]

                elif state == "Delete":
                    local_socket_obj.Send("GETALLUSERINFO2#")
                    all_user_info = local_socket_obj.Recv()
                    self.Send(all_user_info)
                    user_to_del = self.Recv()
                    while user_to_del == "":
                        user_to_del = self.Recv()
                    try:
                        user_to_del = int(user_to_del)
                    except:
                        self.Send("Reset")
                    local_socket_obj.Send("DELETEUSER#" + str(user_to_del))
                    # user_info = dbm.ReadInfoByUID(user_to_del)
                    ack = local_socket_obj.Recv()
                    self.Send(ack)

                elif state == "Change":
                    local_socket_obj.Send("GETALLUSERINFO3#")
                    all_user_info = local_socket_obj.Recv()
                    self.Send(all_user_info)
                    change_str = self.Recv()
                    while change_str == "":
                        change_str = self.Recv()
                    if change_str.split('#')[0] == "Change":
                        what_to_change = change_str.split('#')[1]
                        user_id = change_str.split('#')[2]
                        new_item = change_str.split('#')[3]
                        if what_to_change == "fname":
                            local_socket_obj.Send("FNAME#" + new_item + "#" + user_id)
                            ack = local_socket_obj.Recv()
                            if ack == "1":
                                self.Send("User info updated")
                            else:
                                self.Send("Could not Change...")
                        elif what_to_change == "lname":
                            local_socket_obj.Send("LNAME#" + new_item + "#" + user_id)
                            ack = local_socket_obj.Recv()
                            if ack == "1":
                                self.Send("User info updated")
                            else:
                                self.Send("Could not Change...")
                        elif what_to_change == "password":
                            local_socket_obj.Send("PASS#" + new_item + "#" + user_id)
                            ack = local_socket_obj.Recv()
                            if ack == "1":
                                self.Send("User info updated")
                            else:
                                self.Send("Could not Change...")














                i += 1
            else:
                break

socket_obj = Socket()
socket_obj.StartServer()
socket_obj.Communicate()

socket_obj.CloseServer()
















