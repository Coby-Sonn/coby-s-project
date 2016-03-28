import sqlite3
import DbCreator
import os.path


SQLITE_FILE_PATH = 'mydb.sqlite'    # name of the sqlite database file
USERINFO = 'UserInfo'  # name of the table to be created

INT_TYPE = 'INTEGER'  # column data type
STRING_TYPE = 'TEXT'
"""name of the columns"""
FIRST_NAME = 'FNAME'  # 1
UID = 'UID'  # 2
LAST_NAME = 'LNAME'  # 3
USERNAME = 'UNAME'  # 4
PASSWORD_HASH = 'PH'  # 5
COLUMN_LIST = [FIRST_NAME, UID, LAST_NAME, USERNAME, PASSWORD_HASH]



def CheckifExists():
    """checks if the db exists
        if it does not then it will create it"""
    if not os.path.isfile(SQLITE_FILE_PATH):
        DbCreator.CreateTable()

"""user db funcs-------------------------------------------------------------------"""
def AddInfo(fname, uid, lname, uname, ph):
    conn = sqlite3.connect(SQLITE_FILE_PATH)
    c = conn.cursor()
    row = uid, "'"+fname+"'", "'"+lname+"'", "'"+uname+"'", "'"+ph+"'"
    c.execute('insert or ignore into UserInfo values (?,?,?,?,?)', row)
    conn.commit()
    conn.close()
def UpdateInfo(serial_number, what_to_change, uid):
    """cannot get number 2, will not change the uid"""
    if serial_number == 2:
        raise ValueError("Cannot change uid number")
        return False
    try:
        conn = sqlite3.connect(SQLITE_FILE_PATH)
        c = conn.cursor()
        execution_string = 'UPDATE UserInfo SET %s' %COLUMN_LIST[serial_number-1]
        execution_string = execution_string + " = '%s'" %what_to_change
        execution_string = execution_string + " WHERE UID = %s" %uid
        c.execute(execution_string)
        conn.commit()
        conn.close()
        return True
    except:
        return False
def DeleteInfo(uid):
    try:
        conn = sqlite3.connect(SQLITE_FILE_PATH)
        c = conn.cursor()
        execution_string = "DELETE FROM UserInfo WHERE UID = %s" %uid
        c.execute(execution_string)
        conn.commit()
        conn.close()
        return True
    except:
        return False
def ReadAllRows():
    conn = sqlite3.connect(SQLITE_FILE_PATH)
    c = conn.cursor()
    c.execute("SELECT * FROM UserInfo")
    rows = c.fetchall()
    temp = rows
    rows = []
    for row in temp:
        data_string = str(row[0]) + "@" + row[1][1:-1] + "@" + row[2][1:-1] + \
              "@" + row[3][1:-1] + "@" + row[4][1:-1]
        ##uid@fname@lname@uname#.....
        rows.append(data_string)

    conn.commit()
    conn.close()
    return rows
def ReadInfoByUID(uid):
    """recvs a user uid in order to read all info about a specific user,
     if he exists then func will return a string with his information else it will return False"""
    if UIDExists:
        conn = sqlite3.connect(SQLITE_FILE_PATH)
        c = conn.cursor()
        execution_string = "SELECT * FROM UserInfo WHERE UID = %s" % uid

        c.execute(execution_string)
        read_info = c.fetchone()
        data_string = str(read_info[0]) + "#" + read_info[1][1:-1] + "#" + read_info[2][1:-1] + \
            "#" + read_info[3][1:-1] + "#" + read_info[4][1:-1]

        conn.commit()
        conn.close()
        return data_string
    else:
        return False
def UIDExists(uid):
    conn = sqlite3.connect(SQLITE_FILE_PATH)
    c = conn.cursor()
    c.execute("SELECT * FROM UserInfo WHERE UID = %s" %uid)
    read_info = c.fetchone()
    if read_info:
        conn.commit()
        conn.close()
        return True
    else:
        conn.commit()
        conn.close()
        return False
def UnameExists(uname):
    rows = ReadAllRows()
    for row in rows:
        if uname in row:
            return (True, row)
    return (False, )
def GetPassHashByUname(uname):
    """receives a username (that was entered by the user and returns the hash of the compatible password"""
    checked = UnameExists(uname)
    if checked[0]:
        row = checked[1]
        print row
        return row.split("@")[4]
    return "the user name does not exist"
def GetInfoForLock():
    rows_string = ""
    rows = ReadAllRows()
    for row in rows:
        rows_string += row + "#"

    return rows_string[:-1]
def GetLoginInfo(uname):
    checked = UnameExists(uname)
    if checked[0]:
        row = checked[1]
        uid = row.split("@")[1]
        fname = row.split("@")[0]
        lname = row.split("@")[2]
        print uid, fname, lname
        return uid + "#" + fname + "#" + lname
    else:
        return "user not found"

"""file db funcs-------------------------------------------------------------------"""

def AddFileInfo(file_id, file_key):
    conn = sqlite3.connect(SQLITE_FILE_PATH)
    c = conn.cursor()
    # row = "'" + file_id + "'", "'" + str(file_key) + "'"
    row = "insert or ignore into FileInfo values ('%s'," % str(file_id)
    row = row + " '%s')" % str(file_key)
    c.execute(row)
    conn.commit()
    conn.close()
def IDExists(file_id):
    conn = sqlite3.connect(SQLITE_FILE_PATH)
    c = conn.cursor()
    c.execute("SELECT * FROM UserInfo WHERE FID = %s" % file_id)
    read_info = c.fetchone()
    if read_info:
        conn.commit()
        conn.close()
        return True
    else:
        conn.commit()
        conn.close()
        return False
def ReadFileInfoByID(file_id):
    if IDExists:
        conn = sqlite3.connect(SQLITE_FILE_PATH)
        c = conn.cursor()
        execution_string = "SELECT * FROM FileInfo WHERE FID = %s" % file_id
        c.execute(execution_string)
        read_info = c.fetchone()
        # data_string = read_info[0][1:-1] + "#" + read_info[1][1:-1]
        conn.commit()
        conn.close()
        return read_info[1]
    else:
        return False
def GetKeyByID(file_id):
    try:
        # print ReadFileInfoByID(file_id).split("#")[1]
        # return ReadFileInfoByID(file_id).split("#")[1]
        return ReadFileInfoByID(file_id)
    except:
        return "File Not Found"
def DeleteFile(file_id):
    try:
        conn = sqlite3.connect(SQLITE_FILE_PATH)
        c = conn.cursor()
        execution_string = "DELETE FROM FileInfo WHERE FID = %s" % file_id
        c.execute(execution_string)
        conn.commit()
        conn.close()
        return True
    except:
        return False














