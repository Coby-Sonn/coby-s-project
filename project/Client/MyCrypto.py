import os, struct
from Crypto.Cipher import AES

#---------------------------------------------------------------------------------------------------------------------
def encrypt_file(key, iv, in_filename, out_filename=None, chunksize=16*1024):
    """ Encrypts a file using AES (CBC mode) with the
    given key.

    key:
        The encryption key - a string that must be
        either 16, 24 or 32 bytes long. Longer keys
        are more secure.

    in_filename:
        Name of the input file

    out_filename:
        If None, '<in_filename>.enc' will be used.

    chunksize:
        Sets the size of the chunk which the function
        uses to read and encrypt the file. Larger chunk
        sizes can be faster for some files and machines.
        chunksize must be divisible by 16.
    """
    if not out_filename:
        out_filename = "temp.txt"

    encryptor = AES.new(key, AES.MODE_CBC, iv)
    filesize = os.path.getsize(in_filename)

    with open(in_filename, 'rb') as infile:
        with open(out_filename, 'wb') as outfile:
            while True:
                chunk = infile.read(chunksize)
                if len(chunk) == 0:
                    break
                elif len(chunk) % 16 != 0:
                    chunk += ' ' * (16 - len(chunk) % 16)

                outfile.write(encryptor.encrypt(chunk))

#---------------------------------------------------------------------------------------------------------------------

def decrypt_file(key, iv, in_filename, out_filename=None, chunksize=16*1024):
    """ Decrypts a file using AES (CBC mode) with the
    given key. Parameters are similar to encrypt_file,
    with one difference: out_filename, if not supplied
    will be in_filename without its last extension
    (i.e. if in_filename is 'aaa.zip.enc' then
    out_filename will be 'aaa.zip')
    """
    if not out_filename:
        out_filename = "temp.txt"
    if not os.path.isdir(in_filename):
        with open(in_filename, 'rb') as infile:
            decryptor = AES.new(key, AES.MODE_CBC, iv)

            with open(out_filename, 'wb') as outfile:
                while True:
                    chunk = infile.read(chunksize)
                    if len(chunk) == 0:
                        break
                    outfile.write(decryptor.decrypt(chunk))

def validate(user_uid, user_rbac):
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
