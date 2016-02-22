import socket
import threading
import MyGenerator as mg
from Crypto.PublicKey import RSA
from Crypto.Cipher import AES

class ThreadedServer(object):
    def __init__(self, host, port):
        self.host = host
        self.port = port

        self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        self.sock.bind((self.host, self.port))
    def Send(self, data, client_socket):
        client_socket.send(self.key.encrypt(data))
    def Recv(self, client_socket, size = 1024):
               return self.key.decrypt(client_socket.recv(size))


    def listen(self):
        self.sock.listen(5)
        while True:
            client, address = self.sock.accept()
            client.settimeout(60)
            threading.Thread(target=self.listenToClient, args=(client, address)).start()
    def KeyExchange(self, client_socket):
        generator_obj = mg.generate()
        private_key, public_key = generator_obj.generate_rsa()
        client_socket.send(public_key)#1
        client_public_key = client_socket.recv(1024)#2
        symmetric_key = generator_obj.generate_aes()
        self.key = symmetric_key
        private_key =  RSA.importKey(private_key) #turning keys back into the right type <key>
        public_key = RSA.importKey(public_key)
        encrypted = public_key.encrypt(str(symmetric_key), 32)
        client_socket.send(encrypted)#3


    def listenToClient(self, client_socket, address):
        size = 1024
        while True:
            try:
                self.KeyExchange(client_socket)
                self.Recv(client_socket)

            except:
                client_socket.close()
                return False

if __name__ == "__main__":
    port_num = input("Port? ")
    ThreadedServer('', port_num).listen()
