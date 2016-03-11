import sqlite3, DbCreator, os.path, Hasher


SQLITE_FILE_PATH = 'mydb.sqlite'    # name of the sqlite database file
USERINFO = 'UserInfo'  # name of the table to be created

INT_TYPE = 'INTEGER'  # column data type
STRING_TYPE = 'TEXT'
"""name of the columns"""
FIRST_NAME = 'FNAME'#1
UID = 'UID'#2
LAST_NAME = 'LNAME'#3
USERNAME = 'UNAME'#4
PASSWORD_HASH = 'PH'#5
COLUMN_LIST = [FIRST_NAME, UID, LAST_NAME, USERNAME, PASSWORD_HASH]



def CheckifExists():
    """checks if the db exists
        if it does not then it will create it"""
    if not os.path.isfile(SQLITE_FILE_PATH):
        DbCreator.CreateTable()

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
        return
    conn = sqlite3.connect(SQLITE_FILE_PATH)
    c = conn.cursor()
    execution_string = 'UPDATE UserInfo SET %s' %COLUMN_LIST[serial_number-1]
    execution_string = execution_string + " = '%s'" %what_to_change
    execution_string = execution_string + " WHERE UID = %d" %uid
    c.execute(execution_string)
    conn.commit()
    conn.close()

def DeleteInfo(uid):
    conn = sqlite3.connect(SQLITE_FILE_PATH)
    c = conn.cursor()
    execution_string = "DELETE FROM UserInfo WHERE UID = %d" %uid
    c.execute(execution_string)
    conn.commit()
    conn.close()

def ReadAllRows():
    conn = sqlite3.connect(SQLITE_FILE_PATH)
    c = conn.cursor()
    c.execute("SELECT * FROM UserInfo")
    rows = c.fetchall()
    temp = rows
    rows = []
    for row in temp:
        data_string = str(row[0]) + "#" + row[1][1:-1] + "#" + row[2][1:-1] + \
              "#" + row[3][1:-1] + "#" + row[4][1:-1]
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
        execution_string = "SELECT * FROM UserInfo WHERE UID = %d" %uid

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
    c.execute("SELECT * FROM UserInfo WHERE UID = %d" %uid)
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
        return row.split("#")[4]
    return "the user name does not exist"


password_hash = Hasher.Hash_Password("hellocoby")
AddInfo("coby", 12347868, "sonnenberg", "coby567", password_hash)
#DeleteInfo(12345)
#x = ReadInfo(1234)
#print x
#x = GetPassHashByUname('hello')
#print x
#UIDExists(123)
#UIDExists(1234)
#GetPassHashByUname('ethan')

