from Crypto.PublicKey import RSA
from Crypto.Cipher import AES

class generate:
    def generate_rsa(self, bits=2048):
        '''
        Generate an RSA keypair with an exponent of 65537 in PEM format
        param: bits The key length in bits
        Return private key and public key
        '''
        new_key = RSA.generate(bits, e=65537) 
        public_key = new_key.publickey().exportKey("PEM") 
        private_key = new_key.exportKey("PEM") 
        return private_key, public_key

    def generate_aes(self):
        return AES.new("0123456789abcdef", AES.MODE_CBC, "1234567890123456")


##a = generate()
##symmetric_key = a.generate_aes()
##public_key, private_key = a.generate_rsa()
##private_key =  RSA.importKey(private_key) #turning keys back into the right type <key>
##public_key =  RSA.importKey(public_key)
##print public_key.encrypt(str(symmetric_key), 32)
