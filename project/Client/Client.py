import socket
from ClientCrypto import *
from AES import *
import subprocess

# region ----------   CONSTANTS   ---------------------------------------------------------------
SERVER_ADDRESS = '10.92.5.51'             # The default target server ip
SERVER_PORT = 6071                      # The default target server port
PROT_START = "Hello"
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
        self.socket.connect((SERVER_ADDRESS, SERVER_PORT))
        print "connected"
        self.socket.send('Hello\r\n')
        data = self.socket.recv(LEN_UNIT_BUF).split(END_LINE)[0]
        if not self.verify_hello(data):
            self.socket.close()
            return
        if Crypto().key_exchange(self.key, self.socket):
            pipe_obj = PipeCommunication()
            while True:
                request = pipe_obj.piperecv()
                self.send(request)
                print request

                answer = self.recv()
                pipe_obj.pipesend(answer)
                print answer


   
    def send(self, msg):
        self.socket.send(encryptAES(self.key, msg))

    def recv(self):
        return decryptAES(self.key, self.socket.recv(LEN_UNIT_BUF))

# ======================================================================================================

class PipeCommunication():
    def __init__(self):
        self.pipe = subprocess.Popen(['python', 'ClientEngine.py'])

    def pipesend(self, message):
        self.pipe.stdin.write(message + "\n")
        self.pipe.stdin.flush()
    def piperecv(self):
        return self.pipe.stdout.readline().strip()


def main():
    client = Client()
    client.start()

if __name__ == "__main__":
    print "start"
    main()
print "done."