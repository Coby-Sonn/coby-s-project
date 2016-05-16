import os


def get_upload_file_data(uid, file_path):

    file_name = file_path.split("\\")
    the_file = open(file_path, "rb")
    file_content = the_file.read()
    the_file.close()
    file_info = uid + "#" + file_name
    file_tuple = (file_info, file_content)  # (uid#file_name.ext, file_content)

    return file_tuple
