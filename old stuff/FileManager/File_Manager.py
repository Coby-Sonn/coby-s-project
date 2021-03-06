import os, random, string, struct, MyCrypto
from ctypes import *
from Crypto.Hash import SHA256


MAGIC_NUMBER = 0xCB
EXTENSION = '.cb'
FILE_TYPE_CODE_DICTIONARY = {"txt": 1, "docx": 2, "ppt": 3, "mp3": 4, "jpeg": 5, "jpg": 6, "png": 7, "bmp": 8, "pdf": 9,
                             "mp4": 10, "xml": 11}


class FileHeaderStruct(Structure):
    _fields_ = [("magicNumber"       , c_ubyte),
                ("fileID"            , c_ushort),
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
  
            
   

class File_Manager():

    def __init__(self, user_uid):
        self.user_uid = user_uid
        #self.original_key = original_key

    def Create_users_rbac(self, path):
        """receives the path of an encrypted file in order to strip the systems headers from it"""
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

    def get_original_key(self, fileid):
        """sends a message to the server asking for the file opener key"""
        received_key =""

        return received_key

    def Strip_File(self, path):

        try:
            """opening files"""
            current_file = open(path, "rb")
            
            """receives the path of an encrypted file in order to strip the systems headers from it"""
            file_header = FileHeaderStruct()
            current_file.readinto(file_header)
            
            dict_1 = {}
            for ext, index in FILE_TYPE_CODE_DICTIONARY.items():
                dict_1[index] = ext

            """removing old extension and saving it
                and adding the new extension"""
            file_path_list = path.split(".")
            new_path = file_path_list[0]+"." + dict_1[file_header.fileTypeCode]

            fileid = file_header.fileID
            """now the system asks the server for the key to open the file which is written in the file header"""
            original_key = self.get_original_key(fileid)
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

            content = current_file.read()
            current_file.close()
            new_file = open(new_path, "wb")
            os.remove(path)
            aux_obj = AUXGenerator()
            aux = aux_obj.hash_generate(users_rbac)
            crypto_obj = MyCrypto.MyCrypto(original_key, aux)
            crypto_obj.generator()
            new_file = open(new_path, "w")
            validated = crypto_obj.validate(self.user_uid, users_rbac)
            if validated:
                print "validated"
                insertion_content = crypto_obj.decrypt_content(content)
                new_file.write(insertion_content)
                new_file.close()
            else:
                print "the specified user is not allowed to open the file"
                new_file.write(content)
                new_file.close()
                os.rename(new_path, path)
            
            
            
            
            
        except IOError: "Could not strip the file"

    def Create_New_Format(self, path, UID_List, rbac, second_UID_List = None, second_rbac = None):
        """receives the file path, a list of uids allowed to do what the rbac specifies, and a rbac
            attaches the header and uses Encryption's function to encrypt the data"""
        users_rbac = []
        """removing old extension and saving it
            and adding the new extension"""
        file_path_list = path.split(".")
        old_extension = file_path_list[1]
        new_path = file_path_list[0]+".cb"

        if second_rbac is None and second_UID_List is None:
            optional_header_flag = 0
        else:
            optional_header_flag = 1
        fileid = ''.join(random.choice(string.ascii_uppercase + string.digits) for _ in range(8))
        file_header = FileHeaderStruct(MAGIC_NUMBER,
                                       fileid,
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

        original_key = ''.join(random.choice(string.ascii_uppercase + string.digits) for _ in range(32))
        crypto_obj = MyCrypto.MyCrypto(original_key, aux)
        crypto_obj.generator()
        insertion_content = crypto_obj.encrypt_content(content)
        new_file.write(insertion_content)

        """saving new file and removing the old one which was replaced"""
        new_file.close()
        os.remove(path)





path1 = 'C:\Users\User\Desktop\coby.txt'
path2 = 'C:\Users\User\Desktop\\coby.cb'
uid_list = [12345678, 23456789, 67423972]
second_uid_list = [45454545, 75642985]
user_uid = "12345678"

print "starting... "
File_Manager.Create_New_Format(File_Manager(user_uid), path1, uid_list, 1, second_uid_list, 0)
raw_input("continue? ")
File_Manager.Strip_File(File_Manager(user_uid), path2)


