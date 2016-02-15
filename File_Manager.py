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

class AUXGenerator:

    def hash_generate(self, users_rbac):
        """users_rbac = [(1, [12345678,23456789]), (0, [23544445, 87342914])]"""
        first_UID_list = users_rbac[0][1]
        print "first_UID_list " + str(first_UID_list)
        try:
            second_UID_List = users_rbac[1][1]
            uid_string = self.uid_string_generate(first_UID_list, second_UID_List)
            aux_str = str(users_rbac[0][0]) + str(users_rbac[1][0]) + uid_string
        except:
            uid_string = self.uid_string_generate(first_UID_list)

            aux_str = str(users_rbac[0][0]) + uid_string
            print "aux_str " + aux_str

        my_hash = SHA256.new()
        my_hash.update(aux_str)
        first_hash_str = my_hash.hexdigest()

        my_hash_str = ''.join(chr(int(first_hash_str[i:i+2], 16)) for i in range(0, len(first_hash_str), 2))
        middle = (len(my_hash_str)/2)
        my_hash_str = my_hash_str[:middle]
        if len(my_hash_str) > 16:
            middle = (len(my_hash_str)/2)
            my_hash_str = my_hash_str[:middle]
            print "my hash str: " + my_hash_str
        return my_hash_str

    def uid_string_generate(self, UID_List, second_UID_List=None):
        uid_string = ""
        print str(UID_List) + " uid list"
        for uid in UID_List:
            uid_string += str(uid)
        if second_UID_List is not None:
            for uid in second_UID_List:
                uid_string += str(uid)
        print "uid_string " + uid_string
        return uid_string

class Encryption:
    def __init__(self, original_key, aux):
        self.original_key = original_key
        self.aux = aux
    def generate(self):
        key = AES.new(self.original_key, AES.MODE_CBC, self.aux)
        print "key: " + str(key)
        return key
    def validate(self, path, user_uid, user_rbac):
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
            print "you made it!"
            self.decrypt_stripped_file_content(path)
            print "Found, this user is allowed to do: " + str(user_rbac[0][0])
            return True
        else:
            try:
                if len(user_rbac) == 1:
                    return False
                uid_list = user_rbac[1][1]
                if user_uid in uid_list:
                    self.decrypt_stripped_file_content(path)
                    print "Found, this user is allowed to do: " + str(user_rbac[1][0])
                    return True
            except:
                pass
    def encrypt_original_file_content(self, original_content):
        """recieves the content to encrypt"""
        key = self.generate()
        #print "o-g len: " + str(len(original_content)) + " " + original_content
        content_list = []
        content_list = [original_content[i:i+16] for i in range(0, len(original_content), 16)]
        i = 0
        char_str = ""
        for char in original_content:
            ##print char
            if len(char_str) < 16:
                char_str += char
                ##print char_str
            else:
                content_list.append(char_str)
        
        encrypted_content_list = []
        for chunk in content_list:
            if len(chunk) < 16:
                print  "length of chunk: " + str(len(chunk))
                number_to_add = 16 - len(chunk)
                chunk = chunk + (" " * number_to_add)
                print  "length of chunk: " + str(len(chunk))

            encrypted_chunk = key.encrypt(chunk)
            ##print "encrypted_chunk" + encrypted_chunk
            encrypted_content_list.append(encrypted_chunk)
        content_to_insert = ''.join(encrypted_content_list)
        print "content_to_insert: " + str(len(content_to_insert))
        return content_to_insert
    def decrypt_stripped_file_content(self, path):
        """recieves the already stripped encrypted file in order to decrypt"""


        key = self.generate()
        file_to_decrypt = open(path, "r")
        cipher_content = file_to_decrypt.read()
        file_to_decrypt.close()
        print "cipher_content: " + cipher_content + " " + str(len(cipher_content))
        content_to_insert = key.decrypt(cipher_content)
        print "content_to_insert: " + content_to_insert
        insertion_file = open(path, "w")
        insertion_file.write(content_to_insert)
        insertion_file.close()

class File_Manager():
    def __init__(self, user_uid, original_key):
        self.user_uid = user_uid
        self.original_key = original_key
    def Create_users_rbac(self, path):
        """recieves the path of an encrypted file in order to strip the systems headers from it"""
        print "starting Create users rbac func"
        ##print "path: " + path
        current_file = open(path, "rb")
        file_header = FileHeaderStruct()
        current_file.readinto(file_header)
        print file_header.lenUIDS
        UID_List = []
        for i in xrange(file_header.lenUIDS):
            uid_s = current_file.read(4)
            print uid_s
            UID_List.append(struct.unpack('<L', uid_s)[0])

        first_rbac_users = (file_header.rBac, UID_List)
        print first_rbac_users
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
    def Strip_File(self, path):
        print "start"
        try:
            """opening files"""
            current_file = open(path, "rb")
            ##print "file opened"
            """recieves the path of an encrypted file in order to strip the systems headers from it"""
            file_header = FileHeaderStruct()
            current_file.readinto(file_header)
            ##print "header read into"
            dict_1 = {}
            for ext, index in FILE_TYPE_CODE_DICTIONARY.items():
                dict_1[index] = ext

            """removing old extension and saving it
                and adding the new extension"""
            file_path_list = path.split(".")
            new_path = file_path_list[0]+"." + dict_1[file_header.fileTypeCode]
            ##print "path saved"
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
            print "lists made: " + str(users_rbac)
            content = current_file.read()
            current_file.close()
            new_file = open(new_path, "wb")
            new_file.write(content)
            new_file.close()
            os.remove(path)
            b = AUXGenerator()
            aux = b.hash_generate(users_rbac)
            ##print "created hash"
            a = Encryption(self.original_key, aux)
            validated = a.validate(new_path, self.user_uid, users_rbac)
            if validated:
                print "validated"
            else:
                print "the specified user is not allowed to open the file"
            
        except IOError: "here"
    def Create_New_Format(self, path, UID_List, rbac, second_UID_List = None, second_rbac = None):
        """receives the file path, a list of uids allowed to do what the rbac specifies, and a rbac
            attaches the header and uses Encryption's function to encrypt the data"""
        users_rbac = []
        """removing old extension and saving it
            and adding the new extension"""
        file_path_list = path.split(".")
        old_extension = file_path_list[1]
        new_path = file_path_list[0]+".cb"
        ##print "ext changed"
        if second_rbac is None and second_UID_List is None:
            optional_header_flag = 0
        else:
            optional_header_flag = 1
        file_header = FileHeaderStruct(MAGIC_NUMBER,
                                       FILE_TYPE_CODE_DICTIONARY[old_extension],
                                       rbac,
                                       optional_header_flag,
                                       len(UID_List))
        ##print "header created"
        """opening files"""
        new_file = open(new_path, "wb")
        current_file = open(path, "rb")
        ##print "files open"
        """writing header to the file"""
        new_file.write(file_header)
        ##print "header written"
        """adding hexed uids:"""
        for uid in UID_List:
            new_file.write(struct.pack('i', uid))
        ##print "uids written"
        if optional_header_flag == 1:
            addition_to_file_header = OptionalHeaderStructAdditions(second_rbac, len(second_UID_List))
            new_file.write(addition_to_file_header)
            for uid in second_UID_List:
                new_file.write(struct.pack('i', uid))
                users_rbac = [(rbac, UID_List), (second_rbac, second_UID_List)]
        else:
            users_rbac = [(rbac, UID_List)]
        ##print "second header and uids written"
        """reading the original content
            and putting it in the file"""
        print "-------------look here--------------"
        ##print "users_rbac: " + str(users_rbac)
        content = current_file.read()
        print "content saved: " + content + "len: " + str(len(content))
        current_file.close()
        b = AUXGenerator()
        aux = b.hash_generate(users_rbac)
        ##print "aux created"
        a = Encryption(self.original_key, aux)

        insertion_content = a.encrypt_original_file_content(content)
        new_file.write(insertion_content)
        ##print "content written"
        """saving new file and removing the old one which was replaced"""
        new_file.close()
        os.remove(path)





path1 = 'C:\Users\David\Desktop\coby.txt'
path2 = 'C:\Users\David\Desktop\\coby.cb'
uid_list = [12345678, 23456789]
user_uid = "12345678"
original_key = "12345678909876543212345678909876"
print "starting... "
File_Manager.Create_New_Format(File_Manager(user_uid, original_key), path1, uid_list, 1)
raw_input("continue? ")
File_Manager.Strip_File(File_Manager(user_uid, original_key), path2)

