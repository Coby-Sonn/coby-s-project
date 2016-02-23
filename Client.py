import socket
from Crypto.PublicKey import RSA
from Crypto.Cipher import AES
import MyGenerator as mg




class Client:
    def __init__(self):
        self.c_socket = socket.socket()

    def connect(self, ip, port):
        self.c_socket.connect((ip, port))
        self.KeyExchange(self.c_socket)
    def KeyExchange(self,c_socket):
        generator_obj = mg.generate()
        private_key, public_key = generator_obj.generate_rsa()
        server_public_key = c_socket.recv(1024)#1
        server_public_key = RSA.importKey(server_public_key)
        private_key = RSA.importKey(private_key)
        c_socket.send(public_key)#2
        symmetric_key_string = c_socket.recv(1024)#3
        self.key = private_key.decrypt(symmetric_key_string)
        print "done"




    def Send(self, data):
        self.c_socket.send(self.key.encrypt(data))
    def Recv(self,size = 1024):
       return self.key.decrypt(self.c_socket.recv(size))




client_obj = Client()
client_obj.connect('192.168.4.116', 5000)

##client_obj.Send("hello there")



##my_socket = socket.socket()
##my_socket.connect(('10.92.5.51', PORT))
##private_key, public_key = generate_RSA()
##server_public_key = my_socket.recv(1024)#1
##print "1"
##connection = True
##if connection:
##    my_socket.send(public_key)#2
##    print "2"
##    symmetric_key = my_socket.recv(1024)#3
##    print "symmetric_key: " + symmetric_key


my_socket.close()