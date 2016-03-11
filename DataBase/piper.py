import time
import struct
import DbManager as dbm

def send(f, s):
    f.write(struct.pack('I', len(s)) + s)   # Write str length and str
    f.seek(0)                               # EDIT: This is also necessary
def recv(f):
    n = struct.unpack('I', f.read(4))[0]    # Read str length
    message = f.read(n)                           # Read str
    f.seek(0)# Important!!!
    return message

f = open(r'\\.\pipe\Communicate', 'r+b', 0)
i = 1

##while True:
##message = 'hello there'.format(i)

info = recv(f)  
info = info.split("#")
username = info[0]
password = info[1]

password_hash = dbm.GetPassHashByUname(username)
if password_hash == password:
    message = "1"
else:
    message = "0"
    

send(f, message)


time.sleep(2)

    

