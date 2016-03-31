import socket
import DbManager as dbm
import File_Manager as fm

HOST = "0.0.0.0"
PORT = 12344
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
    def GetUserInfoForLock(self):
        """gets a long string built like this: uid@fname@lname#uid@fname@lname#uid@fname@lname#... about all users"""
        return dbm.GetInfoForLock()

    def Communicate(self):

        while True:
            self.gui_socket, self.addr = self.socket.accept()
            print "Connected to gui"
            info = self.Recv()
            info = info.split("#")
            state = info[0]
            print state
            message = ""
            if state == "login":
                username = info[1]
                password = info[2]
                password_hash = dbm.GetPassHashByUname(username)
                if password_hash == password:
                    info_str = dbm.GetLoginInfo(username)
                    message = "Signed in#" + info_str
                    self.Send(message)
                else:
                    message = "Not#0"
                    self.Send(message)

            elif state == "register":
                while state == "register":
                    print info
                    uid = info[1]
                    firstname = info[2]
                    lastname = info[3]
                    username = info[4]
                    password = info[5]

                    uname_exists = dbm.UnameExists(username)[0]

                    if not uname_exists:
                        dbm.AddInfo(firstname, uid, lastname, username, password)
                        message = "Signed up"
                        self.Send(message)
                    else:
                        message = "username exists"
                        self.Send(message)
                    info = self.Recv()
                    info = info.split("#")
                    state = info[0]

            elif state == "Unlock":
                uid = info[1]
                path = info[2]
                if path.endswith(".cb"):
                    file_obj = fm.File_Manager(uid)
                    ack = file_obj.Strip_File(path)
                    self.Send(ack)
                else:
                    self.Send("Path error, can only unlock .cb files")


            elif state == "Lock":
                all_user_info = self.GetUserInfoForLock()
                print all_user_info
                self.Send(all_user_info)
                print "sent all user info"
                info = self.Recv()  # info = LockReady#uid#path#uid@uid@...#rbac#optionality

                info = info.split("#")
                print info

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
                        file_obj.Create_New_Format(path, uid_list, rbac, second_uid_list, second_rbac)
                    else:
                        file_obj.Create_New_Format(path, uid_list, rbac)
                    message = "Locked"
                    self.Send(message)
                else:
                    self.Send("Reset")

            elif state == "Delete":
                all_user_info = self.GetUserInfoForLock()
                self.Send(all_user_info)
                user_to_del = self.Recv()
                while user_to_del == "":
                    user_to_del = self.Recv()
                try:
                    user_to_del = int(user_to_del)
                except:
                    self.Send("Reset")
                user_info = dbm.ReadInfoByUID(user_to_del)
                if dbm.DeleteInfo(user_to_del):
                    deleted_user_info = user_info.split('#')[1] + " " + user_info.split('#')[2] + \
                                        " " + user_info.split('#')[3]
                    self.Send("User %s Deleted" % deleted_user_info)
                else:
                    self.Send("User %s can not be deleted, or does not exist" % user_to_del)
            elif state == "Change":
                all_user_info = self.GetUserInfoForLock()
                self.Send(all_user_info)
                change_str = self.Recv()
                while change_str == "":
                    change_str = self.Recv()
                if change_str.split('#')[0] == "Change":
                    what_to_change = change_str.split('#')[1]
                    user_id = change_str.split('#')[2]
                    new_item = change_str.split('#')[3]
                    if what_to_change == "fname":
                        if dbm.UpdateInfo(1, new_item, user_id):
                            self.Send("User info updated")
                        else:
                            self.Send("Could not Change...")
                    elif what_to_change == "lname":
                        if dbm.UpdateInfo(3, new_item, user_id):
                            self.Send("User info updated")
                        else:
                            self.Send("Could not Change...")
                    elif what_to_change == "password":
                        if dbm.UpdateInfo(5, new_item, user_id):
                            self.Send("User info updated")
                        else:
                            self.Send("Could not Change...")




socket_obj = Socket()
socket_obj.StartServer()
socket_obj.Communicate()




