from Crypto.Cipher import AES


CHUNK_SIZE = 64*1024

class  MyCrypto:

        def __init__(self, original_key, iv):
            self.original_key = original_key
            self.iv = iv

        def generator(self):
            print "origin key: " + str(self.original_key)
            self.encryptor = AES.new(self.original_key, AES.MODE_CBC, self.iv)
    
        def encrypt_content(self,original_content):
                """ Encrypts a file using AES (CBC mode) with the
                    given key.
                    key:
                        The enchunksizechunksizecryption key - a string that must be
                        either 16, 24 or 32 bytes long. Longer keys
                        are more secure.
                    in_filename:
                        Name of the input file
                    out_filename:
                        If None, '<in_filename>.enc' will be used.
                    chunksize:
                        Sets the size of the chunk which the function
                        uses to read and encrypt the file. Larger chunk
                        sizes can be fasterlength for some files and machines.
                        chunksize must be divisimport os, random, struct
            from Crypto.Cipher import AESible by 16.
                """
           
                
                parts = [original_content[i:i+CHUNK_SIZE] for i in range(0, len(original_content), CHUNK_SIZE)]
                encrypted_content = ""
                for part in parts:
                    if len(part) % 16 != 0:
                        part += ' ' * (16 - len(part) % 16)
                    encrypted_content += self.encryptor.encrypt(part)
                    
                return encrypted_content

        def decrypt_content(self,encrypted_content):
                """ Decrypts a file using AES (CBC mode) with the
                    given key. Parameters are similar to encrypt_file,
                    with one difference: out_filename, if not supplied
                    will be in_filename without its last extension
                    (i.e. if in_filename is 'aaa.zip.enc' then
                    out_filename will be 'aaa.zip')
                """

                parts = [encrypted_content[i:i+CHUNK_SIZE] for i in range(0, len(encrypted_content), CHUNK_SIZE)]
                decrypted_content = ""
                for part in parts:
                    if len(part) % 16 != 0:
                        part += ' ' * (16 - len(part) % 16)
                    decrypted_content += self.encryptor.decrypt(part)

                return decrypted_content

        def validate(self, user_uid, user_rbac):
                """recieves the list of users and the local users uid
                    and send the path to get decrypted"""
                """user_rbac = [(1, [12345678,23456789]), (0, [23544445, 87342914])]"""
                uid_list = user_rbac[0][1]
                print uid_list
                string_uid_list = []
                for uid in uid_list:
                    string_uid_list.append(str(uid))
                print user_uid
                if str(user_uid) in string_uid_list:
                    print "Found, this user is allowed to do: " + str(user_rbac[0][0])
                    return True
                else:
                    try:
                        if len(user_rbac) == 1:
                            return False
                        uid_list = user_rbac[1][1]
                        if user_uid in uid_list:
                            print "Found, this user is allowed to do: " + str(user_rbac[1][0])
                            return True
                    except:
                        pass

