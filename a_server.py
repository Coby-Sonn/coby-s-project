import socket
from Crypto.PublicKey import RSA
from Crypto.Cipher import AES
def generate_rsa(bits=2048):
    '''
    Generate an RSA keypair with an exponent of 65537 in PEM format
    param: bits The key length in bits
    Return private key and public key
    '''
    new_key = RSA.generate(bits, e=65537) 
    public_key = new_key.publickey().exportKey("PEM") 
    private_key = new_key.exportKey("PEM") 
    return private_key, public_key

def generate_aes():
    return AES.new("0123456789abcdef", AES.MODE_CBC, "1234567890123456")
    

server_socket = socket.socket()

server_socket.bind(('0.0.0.0', 8821))

server_socket.listen(1)

(client_socket, client_address) = server_socket.accept()

private_key, public_key = generate_rsa()

client_socket.send(public_key)#1
##print "sent server public key"

client_public_key = client_socket.recv(1024)#2
##print client_public_key

private_key =  RSA.importKey(private_key) #turning keys back into the right type <key>
public_key =  RSA.importKey(public_key)

symmetric_key = generate_aes()

encrypted = public_key.encrypt(str(symmetric_key), 32)


client_socket.send(str(encrypted))#3
client_socket.close()
server_socket.close()
