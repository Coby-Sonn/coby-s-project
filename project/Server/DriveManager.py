import os

PATH_START = "TheDrive\\"


def CheckifExists(uid):
    """checks if a directorey exists
        if it does not then it will create it"""
    path = PATH_START + uid
    if not os.path.exists(path):
        os.makedirs(path)
        print "created"
    else:
        print "exists"

def deletedir(uid):
    path = PATH_START + uid
    try:
        if not os.path.exists(path):
            os.remove(path)
            print "Deleted"
    except: pass


def Create(file_info_tuple):

    uid = file_info_tuple[0].split('#')[0]
    file_name = file_info_tuple[0].split('#')[1]
    file_content = file_info_tuple[1]

    path = PATH_START + uid + "\\" + file_name
    try:
        new_file = open(path, "wb")
        new_file.write(file_content)
        new_file.close()
        return "File Successfully Downloaded"
    except:
        return "Error"


def get_download_file_data(uid, file_name):
    path = PATH_START + uid + "\\" + file_name
    the_file = open(path, "rb")
    file_content = the_file.read()
    print "file_content: " + file_content
    the_file.close()
    file_info = uid + "#" + file_name
    file_tuple = (file_info, file_content)  # (uid#file_name.ext, file_content)
    os.remove(path)
    return file_tuple






