import os
import struct
from ctypes import *
from Crypto.Hash import SHA256
from Crypto.Cipher import AES

MAGIC_NUMBER = 0xCB
EXTENSION = '.cb'
FILE_TYPE_CODE_DICTIONARY = {"txt": 1, "docx": 2, "ppt": 3, "mp3": 4, "jpeg": 5, "jpg": 6, "png": 7, "bmp": 8, "pdf": 9,
                             "mp4": 10, "xml": 11}


class FileHeaderStruct(Structure):
    _fields_ = [("magicNumber"       , c_ubyte),
                ("fileTypeCode"      , c_ubyte),
                ("rBac"              , c_ubyte),
                ("optionalHeaderFlag", c_ubyte),
                ("lenUIDS"           , c_ushort)
                ]

class OptionalHeaderStructAdditions(Structure):
    _fields_ = [("secondRbac"        , c_ubyte),
                ("secondLenUIDS"     , c_ushort)

                ]

class AUXGenerator():
    def __init__(self, path):
        self.path = path
        self.users_rbac = File_Manager().Strip_File()
        self.my_hash_str = None

    def hash_generate(self, users_rbac= None):

        users_rbac = self.users_rbac
        first_UID_list = users_rbac[0][1]
        try:
            second_UID_List = users_rbac[1][1]
            uid_string = AUXGenerator().uid_string_generate(first_UID_list, second_UID_List)
            aux_str = str(users_rbac[0][0]) + str(users_rbac[1][0]) + uid_string
        except:
            uid_string = AUXGenerator().uid_string_generate(first_UID_list)
            aux_str = str(users_rbac[0][0]) + uid_string

        my_hash = SHA256.new()
        my_hash.update(aux_str)
        first_hash_str = my_hash.hexdigest()
        my_hash_str = ''.join(chr(int(first_hash_str[i:i+2], 16)) for i in range(0, len(first_hash_str), 2))
        if len(my_hash_str) > 16:
            my_hash_str = ''.join(chr(int(my_hash_str[i:i+2], 16)) for i in range(0, len(my_hash_str), 2))
        self.my_hash_str = my_hash_str

    def uid_string_generate(self, UID_List, second_UID_List=None):
        uid_string = ""
        for uid in UID_List:
            uid_string += uid
        if second_UID_List is not None:
            for uid in second_UID_List:
                uid_string += uid
        return uid_string

class Encryption():

    def __init__(self, original_key):
        self.original_key = original_key
        self.aux = AUXGenerator.my_hash_str
        self.key = Encryption.generate(self.original_key, self.aux)

    def generate(self, original_key):
        return AES.new(original_key, AES.MODE_CBC, AUXGenerator.my_hash_str)

    def encrypt_original_file_content(self, original_content):
        """receivs the content to encrypt"""
        key = Encryption.key
        content_to_insert = key.encrypt(original_content)
        return content_to_insert

    def decrypt_stripped_file_content(self, path):
        """receivs the already stripped encrypted file in order to decrypt"""
        key = Encryption.key
        file_to_decrypt = open(path, "r")
        cipher_content = file_to_decrypt.read()
        file_to_decrypt.close()
        content_to_insert = key.decrypt(cipher_content)
        insertion_file = open(path, "w")
        insertion_file.write(content_to_insert)
        insertion_file.close()

class File_Manager():

    def validate(self, path, user_uid, uid_list):
        """receivs the list of users and the local users uid
            and send the path to get decrypted"""
        if user_uid in uid_list:
            Encryption.decrypt_stripped_file_content(path)
            return True
        else:
            return False

    def Strip_File(self, path):
        try:
            """opening files"""
            current_file = open(path, "rb")
            """recieves the path of an encrypted file in order to strip the systems headers from it"""
            file_header = FileHeaderStruct()
            current_file.readinto(file_header)

            dict_1 = {}
            for ext, index in FILE_TYPE_CODE_DICTIONARY.items():
                dict_1[index] = ext

            """removing old extension and saving it
                and adding the new extension"""
            file_path_list = path.split(".")
            new_path = file_path_list[0]+"." + dict_1[file_header.fileTypeCode]

            UID_List = []
            for i in xrange(file_header.lenUIDS):
                uid_s = current_file.read(4)
                UID_List.append(struct.unpack('<L', uid_s)[0])

            first_rbac_users = (file_header.rBac, UID_List)

            file_optional_header = None
            if file_header.optionalHeaderFlag:
                file_optional_header = OptionalHeaderStructAdditions()
                current_file.readinto(file_optional_header)
                second_UID_List = []
                for i in xrange(file_optional_header.secondLenUIDS):
                    uid_s = current_file.read(4)
                    second_UID_List.append(struct.unpack('<L', uid_s)[0])
                second_rbac_users = (file_optional_header.secondRbac, second_UID_List)

            try:
                users_rbac = [first_rbac_users, second_rbac_users]
            except:
                users_rbac = [first_rbac_users]

            content = current_file.read()
            current_file.close()
            new_file = open(new_path, "wb")
            new_file.write(content)
            new_file.close()
            os.remove(path)
            return users_rbac
        except IOError: pass

    def Create_New_Format(self, path, UID_List, rbac, second_UID_List = None, second_rbac = None):
        """receives the file path, a list of uids allowed to do what the rbac specifies, and a rbac
            attaches the header and uses Encryption's function to encrypt the data"""

        """removing old extension and saving it
            and adding the new extension"""
        file_path_list = path.split(".")
        old_extension = file_path_list[1]
        new_path = file_path_list[0]+".cb"

        if second_rbac is None and second_UID_List is None:
            optional_header_flag = 0
        else:
            optional_header_flag = 1
        file_header = FileHeaderStruct(MAGIC_NUMBER,
                                       FILE_TYPE_CODE_DICTIONARY[old_extension],
                                       rbac,
                                       optional_header_flag,
                                       len(UID_List))
        
        """opening files"""
        new_file = open(new_path, "wb")
        current_file = open(path, "rb")

        """writing header to the file"""
        new_file.write(file_header)

        """adding hexed uids:"""
        for uid in UID_List:
            new_file.write(struct.pack('i', uid))

        if optional_header_flag == 1:
            addition_to_file_header = OptionalHeaderStructAdditions(second_rbac, len(second_UID_List))
            new_file.write(addition_to_file_header)
            for uid in second_UID_List:
                new_file.write(struct.pack('i', uid))

        """reading the original content
            and putting it in the file"""
        content = current_file.read()
        current_file.close()
        insertion_content = Encryption().encrypt_original_file_content(content)
        new_file.write(insertion_content)

        """saving new file and removing the old one which was replaced"""
        new_file.close()
        os.remove(path)



path = 'C:\Users\User\Desktop\coby.txt'

File_Manager().Create_New_Format('C:\Users\User\Desktop\coby.txt', [12345678, 23456789], 1, [62723342, 45678902], 0)
#File_Manager().Strip_File('C:\Users\User\Desktop\coby.cb')

