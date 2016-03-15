import time
import struct
import DbManager as dbm
import File_Manager as fm

"""server side"""
def send(f, s):
    f.write(struct.pack('I', len(s)) + s)   # Write str length and str
    f.seek(0)                               # EDIT: This is also necessary
def recv(f):
    n = struct.unpack('I', f.read(4))[0]    # Read str length
    message = f.read(n)                           # Read str
    f.seek(0)
    return message
def GetUserInfoForLock():
    """gets a long string built like this: uid@fname@lname#uid@fname@lname#uid@fname@lname#... about all users"""
    return dbm.GetInfoForLock()


pipe = open(r'\\.\pipe\Communicate', 'r+b', 0)
i = 1

info = recv(pipe)
info = info.split("#")
state = info[0]

message = ""
if state == "login":
    username = info[1]
    password = info[2]
    password_hash = dbm.GetPassHashByUname(username)
    if password_hash == password:
        info_str = dbm.GetLoginInfo(username)
        message = "Signed in#" + info_str
    else:
        message = "Not#0"

elif state == "register":
    uid = info[1]
    firstname = info[2]
    lastname = info[3]
    username = info[4]
    password = info[5]

    uname_exists = dbm.UnameExists(username)[0]
    if not uname_exists:
        dbm.AddInfo(firstname, uid, lastname, username, password)
        message = "Signed up"
    else:
        message = "username exists"

elif state == "Unlock":
    uid = info[1]
    path = info[2]
    if path.endswith(".cb"):
        file_obj = fm.File_Manager(uid)
        ack = file_obj.Strip_File(path)
        if ack == "File unlocked":
            message = ack
        elif ack == "The specified user is not allowed to open the file":
            message = ack
    else:
        message = "path error, can only unlock .cb files"
elif state == "Lock":
    AllUserInfo = GetUserInfoForLock()
    send(pipe, AllUserInfo)
    info = recv(pipe)
    if info[0] == "LockReady":
        uid = info[1]
        path = info[2]
        uid_list = info[3].split("@")
        rbac = info[4]
        optionality = info[5]#0 or 1
        file_obj = fm.File_Manager(uid)
        if optionality:
            second_uid_list = info[6].split("@")
            second_rbac = info[7]
            file_obj.Create_New_Format(path, uid_list, rbac, second_uid_list, second_rbac)
        else:
            file_obj.Create_New_Format(path, uid_list, rbac)
        message = "Locked"












send(pipe, message)



##time.sleep(2)

    

