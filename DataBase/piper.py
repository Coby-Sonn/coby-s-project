import time
import struct
import DbManager as dbm


def send(f, s):
    f.write(struct.pack('I', len(s)) + s)   # Write str length and str
    f.seek(0)                               # EDIT: This is also necessary
def recv(f):
    n = struct.unpack('I', f.read(4))[0]    # Read str length
    message = f.read(n)                           # Read str
    f.seek(0)
    return message

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
        message = "Signed in"
    else:
        message = "Not"

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





send(pipe, message)



##time.sleep(2)

    

