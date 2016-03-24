import sys

""" this script deals with the communication between the python engine and the communication with the server through
session with client
http://stackoverflow.com/questions/15396628/sending-strings-between-to-python-scripts-using-subprocess-pipes"""


def pipe_send(message):
    sys.stdout.write(message + "\n")
    sys.stdout.flush()
def pipe_recv():
    return sys.stdin.readline()


pipe_send("hello man")

print pipe_recv()





