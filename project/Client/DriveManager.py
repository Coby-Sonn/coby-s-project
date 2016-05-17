import os

ADDR_START = "TheDrive\\"
PATH_START = "C:\\"

def get_upload_file_data(uid, file_path):

    file_name = file_path.split("\\")[-1]
    """Keep on working here, need to check this part altogether"""
    the_file = open(file_path, "rb")
    file_content = the_file.read()
    the_file.close()
    file_info = uid + "#" + file_name
    file_tuple = (file_info, file_content)  # (uid#file_name.ext, file_content)

    return file_tuple


def Create(file_info_tuple, path):

    file_name = file_info_tuple[0].split('#')[1]
    file_content = file_info_tuple[1]
    full_path = path + file_name
    print path
    # print "content: " + file_content
    try:
        """ problem here, wouldn't literaly download the file to the folder."""
        with open(full_path, "wb") as new_file:
            new_file.write(file_content)
        """new_file = open(full_path, "wb")
        print "file opened"
        new_file.write(file_content)
        print "file written in"
        new_file.close()
        print "done"""""
        return "File Successfully Downloaded to " + path
    except:
        return "Error"
