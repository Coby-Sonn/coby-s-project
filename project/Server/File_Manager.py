""" Deals with the files themselves, creates them (changes their format), sends for encryption and decryption,
and creates the auxiliary"""

import os
import struct
import MyCrypto
from ctypes import *
from Crypto.Hash import SHA256
import DbManager as dbm
from random import randint
import stat


MAGIC_NUMBER = 0xCB
EXTENSION = '.cb'
FILE_TYPE_CODE_DICTIONARY = {"txt": 1, "docx": 2, "ppt": 3, "mp3": 4, "jpeg": 5, "jpg": 6, "png": 7, "bmp": 8, "pdf": 9,
                             "mp4": 10, "xml": 11, "doc": 12, "html": 13, "log": 14}


class FileHeaderStruct(Structure):
    _fields_ = [("magicNumber"       , c_ubyte),
                ("fileTypeCode"      , c_uint),
                ("rBac"              , c_uint),
                ("optionalHeaderFlag", c_uint),
                ("lenUIDS"           , c_uint)
                ]

class OptionalHeaderStructAdditions(Structure):
    _fields_ = [("secondRbac"        , c_uint),
                ("secondLenUIDS"     , c_uint)

                ]

class AUXGenerator:

    def hash_generate(self, users_rbac):
        """users_rbac = [(1, [12345678,23456789]), (0, [23544445, 87342914])]"""
        first_UID_list = users_rbac[0][1]

        try:
            second_UID_List = users_rbac[1][1]
            uid_string = self.uid_string_generate(first_UID_list, second_UID_List)
            aux_str = str(users_rbac[0][0]) + str(users_rbac[1][0]) + uid_string
        except:
            uid_string = self.uid_string_generate(first_UID_list)

            aux_str = str(users_rbac[0][0]) + uid_string


        my_hash = SHA256.new()
        my_hash.update(aux_str)
        first_hash_str = my_hash.hexdigest()

        my_hash_str = ''.join(chr(int(first_hash_str[i:i+2], 16)) for i in range(0, len(first_hash_str), 2))
        middle = (len(my_hash_str)/2)
        my_hash_str = my_hash_str[:middle]
        if len(my_hash_str) > 16:
            middle = (len(my_hash_str)/2)
            my_hash_str = my_hash_str[:middle]
        return my_hash_str

    def uid_string_generate(self, UID_List, second_UID_List=None):
        uid_string = ""
        for uid in UID_List:
            uid_string += str(uid)
        if second_UID_List is not None:
            for uid in second_UID_List:
                uid_string += str(uid)
        return uid_string
  
            
   

class File_Manager():

    def __init__(self, user_uid):
        self.user_uid = user_uid
        #self.original_key = original_key

    def Create_users_rbac(self, path):
        """receives the path of an encrypted file in order to strip the systems headers from it"""


        current_file = open(path, "rb")
        file_header = FileHeaderStruct()
        current_file.readinto(file_header)


        UID_List = []
        for i in xrange(file_header.lenUIDS):
            uid_s = current_file.read(4)
            UID_List.append(struct.unpack('<L', uid_s)[0])

        first_rbac_users = (file_header.rBac, UID_List)

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
            """users_rbac = [(1, [12345678,23456789]), (0, [23544445, 87342914])]"""
        except:
            users_rbac = [first_rbac_users]

        current_file.close()
        return users_rbac

    def get_key_or_id(self):
        item = ''
        for i in range(8):
            item = item + str(randint(0,9))

        return item

    def get_original_key(self, file_id):
        """sends a message to the server asking for the file opener key"""
        received_key = dbm.GetKeyByID(file_id)

        return received_key

    def Strip_File(self, path):
        try:
            encrypted_file = open(path, "rb")
            file_header = FileHeaderStruct()
            encrypted_file.readinto(file_header)
            dict_1 = {}
            for ext, index in FILE_TYPE_CODE_DICTIONARY.items():
                dict_1[index] = ext
                file_path_list = path.split(".")

            new_path = file_path_list[0] + "." + dict_1[file_header.fileTypeCode]
            file_id = struct.unpack('<L', encrypted_file.read(4))
            print file_id[0]
            original_key = self.get_original_key(str(file_id[0]))

            UID_List = []
            for i in xrange(file_header.lenUIDS):
                uid_s = encrypted_file.read(4)
                UID_List.append(struct.unpack('<L', uid_s)[0])

            first_rbac_users = (file_header.rBac, UID_List)

            if file_header.optionalHeaderFlag:
                file_optional_header = OptionalHeaderStructAdditions()
                encrypted_file.readinto(file_optional_header)
                second_UID_List = []
                for i in xrange(file_optional_header.secondLenUIDS):
                    uid_s = encrypted_file.read(4)
                    second_UID_List.append(struct.unpack('<L', uid_s)[0])
                second_rbac_users = (file_optional_header.secondRbac, second_UID_List)

            try:
                users_rbac = [first_rbac_users, second_rbac_users]
                """users_rbac = [(1, [12345678,23456789]), (0, [23544445, 87342914])]"""
            except:
                users_rbac = [first_rbac_users]

            encrypted_file_content = encrypted_file.read()
            encrypted_file.close()
            temp_obj = open("temp.txt", "wb")
            temp_obj.write(encrypted_file_content)
            temp_obj.close()
            aux_obj = AUXGenerator()
            aux = aux_obj.hash_generate(users_rbac)
            validated = MyCrypto.validate(self.user_uid, users_rbac)
            if validated:
                MyCrypto.decrypt_file(original_key, aux, "temp.txt")
                temp_obj = open("temp.txt", "rb")
                decrypted_content = temp_obj.read()
                temp_obj.close()
                new_file = open(new_path, "wb")
                new_file.write(decrypted_content)
                new_file.close()
                os.remove(path)
                os.remove("temp.txt")
                if first_rbac_users[0] == 1:
                    os.chmod(new_path, stat.S_IREAD)
                    return "File unlocked, user can only read the file"
                return "File unlocked"
            else:
                return "The specified user is not allowed to open the file "

        except IOError: "Could not strip the file"

    def Create_New_Format(self, path, UID_List, rbac, second_UID_List = None, second_rbac = None):
        users_rbac = []
        if self.user_uid not in UID_List:
            UID_List.append(str(self.user_uid))
        file_path_list = path.split(".")
        old_extension = file_path_list[1]
        new_path = file_path_list[0] + ".cb"
        if second_rbac is None and second_UID_List is None:
            optional_header_flag = 0
        else:
            optional_header_flag = 1

        file_header = FileHeaderStruct(MAGIC_NUMBER,
                                       int(FILE_TYPE_CODE_DICTIONARY[old_extension]),
                                       int(rbac),
                                       int(optional_header_flag),
                                       len(UID_List))
        new_file = open(new_path, "wb")
        new_file.write(file_header)
        fileid = int(self.get_key_or_id())
        new_file.write(struct.pack('i', fileid))
        for uid in UID_List:
            new_file.write(struct.pack('i', int(uid)))

        if optional_header_flag == 1:
            addition_to_file_header = OptionalHeaderStructAdditions(second_rbac, len(second_UID_List))
            new_file.write(addition_to_file_header)
            for uid in second_UID_List:
                new_file.write(struct.pack('i', int(uid)))
                users_rbac = [(rbac, UID_List), (second_rbac, second_UID_List)]
        else:
            users_rbac = [(rbac, UID_List)]
        aux_obj = AUXGenerator()
        aux = aux_obj.hash_generate(users_rbac)
        original_key = self.get_key_or_id() + self.get_key_or_id()
        MyCrypto.encrypt_file(original_key, aux, path)
        temp_obj = open("temp.txt", "rb")
        encrypted_content = temp_obj.read()
        temp_obj.close()
        new_file.write(encrypted_content)
        new_file.close()
        os.chmod(path, stat.S_IWRITE)
        os.remove(path)
        os.remove("temp.txt")
        dbm.AddFileInfo(str(fileid), original_key)




##path1 = 'C:\Users\User\Desktop\coby.txt'
##path2 = 'C:\Users\User\Desktop\\coby.cb'
##uid_list = [12345678, 23456789, 67423972]
##second_uid_list = [45454545, 75642985]
##user_uid = "12345678"

##print "starting... "
##File_Manager.Create_New_Format(File_Manager(user_uid), path1, uid_list, 1, second_uid_list, 0)
##raw_input("continue? ")
##File_Manager.Strip_File(File_Manager(user_uid), path2)


