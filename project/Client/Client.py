import socket
from ClientCrypto import *
from AES import *
import subprocess

# region ----------   CONSTANTS   ---------------------------------------------------------------
"""remote server data"""
SERVER_ADDRESS = '10.100.102.6'             # The default target server ip
SERVER_PORT = 6071                      # The default target server port
PROT_START = "Hello"

"""local communication stream (withClientEngine.py)"""
COMMUNICATION_HOST = "0.0.0.0"
COMMUNICATION_PORT = 8484

# endregion


# ======================================================================================================
class Client(object):
    def __init__(self):
        self.socket = socket.socket()
        self.key = Random.new().read(int(16))

        self.crypto = Crypto()
        print "key created"

    # ==================================================================================================
    def verify_hello(self, data):
        if len(data):
            if not (data == PROT_START):
                print self.socket.recv(LEN_UNIT_BUF).split(END_LINE)[0]
                return False
            return True
        return False

    # ==================================================================================================
    def start(self):
        # print "started start"
        self.socket.connect((SERVER_ADDRESS, SERVER_PORT))
        print "connected"
        self.socket.send('Hello\r\n')
        data = self.socket.recv(LEN_UNIT_BUF).split(END_LINE)[0]
        if not self.verify_hello(data):
            self.socket.close()
            return
        if Crypto().key_exchange(self.key, self.socket):
            print "finished key exchange"
            subprocess.Popen("ClientEngine.py 1", shell=True)
            socket_obj = local_python_communication()
            socket_obj.StartServer()
            i = 0
            while True:
                i += 1
                request = socket_obj.Recv()
                while request == "":
                    request = socket_obj.Recv()
                self.send(request)
                print "from client " + request + " request %d" % i

                answer = self.recv()
                socket_obj.Send(answer)
                print "from client " + answer + " answer %d" % i






   
    def send(self, msg):
        self.socket.send(encryptAES(self.key, msg))

    def recv(self):
        return decryptAES(self.key, self.socket.recv(LEN_UNIT_BUF))

# ======================================================================================================


class local_python_communication():

    def __init__(self):

        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)


    def StartServer(self):

        self.socket.bind((COMMUNICATION_HOST, COMMUNICATION_PORT))
        self.socket.listen(1)
        self.local_socket, self.addr = self.socket.accept()

    def Send(self, data):

        self.local_socket.send(data)

    def Recv(self):

        return self.local_socket.recv(1024)

    def CloseServer(self):

        self.local_socket.close()
        self.socket.close()


def main():
    client = Client()
    client.start()

if __name__ == "__main__":
    print "start"
    main()
print "done."
