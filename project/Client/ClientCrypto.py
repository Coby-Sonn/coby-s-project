# region--------------------------------------------IMPORTS-----------------------------------------
from Crypto.PublicKey import RSA

from Crypto.Cipher.AES import AESCipher
from Crypto.Random.random import getrandbits, randint
from Crypto import Random
from Crypto.Hash import SHA256
from base64 import b64encode, b64decode
import pickle
import time

# endregion

# region-------------------------------------------CONSTANTS----------------------------------------
KEY_LENGTH = 1024
LEN_UNIT_BUF = 1024                      # Min len of buffer for recieve from server socket
MAX_RSA_MSG = 128                        # Maximum length of message encrypted in RSA module (pyCrypto limitation)
MAX_ENCRYPTED_MSG_SIZE = 128
END_LINE = "\r\n"                        # End of line

# endregion

class Crypto:
    private_key = None
    sym_key = None

    # ----------------------------------------------------------
    def __init__(self):
        self.private_key = RSA.generate(KEY_LENGTH)

    # region-----------------FUNCTIONS--------------------------
    ##==================================================================================================
    def  key_exchange(self, key, socket):
        #--------------------  1 ------------------------------------------------------------------------
        # --------------  Wait server Public_Key --------------------------------------------------------
        pickled_server_public_key = socket.recv(LEN_UNIT_BUF).split(END_LINE)[0]
        #print pickled_server_public_key
        server_public_key = pickle.loads(pickled_server_public_key)
        socket.send('OK'+END_LINE)
        # --------------  Wait server hash Public_Key ---------------------------------------------------------------------------
        # Hashing original Public_Key
        calculated_hash_server_pickled_public_key = SHA256.new(pickle.dumps(server_public_key)).hexdigest()
        declared_hash_server_pickled_public_key = b64decode( socket.recv(LEN_UNIT_BUF).split(END_LINE)[0] )
        if calculated_hash_server_pickled_public_key != declared_hash_server_pickled_public_key:
                    return False

        #--------------------  2 ------------------------------------------------------------------------
        # ------------  Send  client private key
        socket.send(pickle.dumps(self.private_key.exportKey()) + END_LINE)
        time.sleep(0.5)
        # -----------  send  Base64 Hash of self.crypto.private_key
        socket.send( b64encode(SHA256.new(pickle.dumps(self.private_key.exportKey())).hexdigest()) + END_LINE)
        time.sleep(0.5)

        #--------------------  3 ------------------------------------------------------------------------
        # -------------- Send  encrypted by server public key info containing symmetric key and hash symmetric key encrypted by client public key ---------------------
        if self.private_key.can_encrypt():
            hash_sym_key = SHA256.new(key).hexdigest()
            pickle_encrypt_hash_sym_key = pickle.dumps(self.private_key.publickey().encrypt(hash_sym_key, 32))
            message = b64encode(key) + "#" + b64encode( pickle_encrypt_hash_sym_key )
            splitted_pickled_message = [message[i:i+MAX_ENCRYPTED_MSG_SIZE] for i in xrange(0, len(message), MAX_ENCRYPTED_MSG_SIZE)]
            #   Sending to server number of encrypted message parts
            socket.send(str(len(splitted_pickled_message)) + END_LINE)
            pickled_encrypted_message = ''
            for part in splitted_pickled_message:
                   part_encrypted_pickled_message = server_public_key.encrypt(part, 32)
                   pickled_part_encrypted_pickled_message = pickle.dumps(part_encrypted_pickled_message)
                   socket.send(pickled_part_encrypted_pickled_message + END_LINE)
                   pickled_encrypted_message += pickled_part_encrypted_pickled_message
                   time.sleep(0.5)
            return True
        return False
    # endregion
