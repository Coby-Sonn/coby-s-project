##client side
import socket
from Crypto.PublicKey import RSA
from Crypto.Cipher import AES


def generate_RSA(bits=2048):
    '''
    Generate an RSA keypair with an exponent of 65537 in PEM format
    param: bits The key length in bits
    Return private key and public key
    '''
    new_key = RSA.generate(bits, e=65537) 
    public_key = new_key.publickey().exportKey("PEM") 
    private_key = new_key.exportKey("PEM") 
    return private_key, public_key

my_socket = socket.socket()
my_socket.connect(('10.92.5.27', 8821))
private_key, public_key = generate_RSA()
server_public_key = my_socket.recv(1024)#1
server_public_key
connection = True
if connection:
    my_socket.send(public_key)#2
    symmetric_key = my_socket.recv(1024)#3
    print symmetric_key
    

my_socket.close()
