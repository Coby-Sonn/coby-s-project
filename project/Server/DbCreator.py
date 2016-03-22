import sqlite3

SQLITE_FILE_PATH = 'mydb.sqlite'    # name of the sqlite database file
USERINFO = 'UserInfo'  # name of the table to be created
FILEINFO = 'FileInfo'
INT_TYPE = 'INTEGER'  # column data type
STRING_TYPE = 'TEXT'
"""name of the columns"""
FIRST_NAME = 'FNAME'
UID = 'UID'
LAST_NAME = 'LNAME'
USERNAME = 'UNAME'
PASSWORD_HASH = 'PH'
FILEKEY = 'KEY'
FILEID = 'FID'


def CreateTable():

    # Connecting to the database file
    conn = sqlite3.connect(SQLITE_FILE_PATH)
    c = conn.cursor()

    # Creating a new SQLite table with 1 column
    c.execute('CREATE TABLE {tn} ({nf} {ft} PRIMARY KEY)'.format(tn=USERINFO, nf=UID, ft=INT_TYPE))
    c.execute("ALTER TABLE {tn} ADD COLUMN '{cn}' {ct}".format(tn=USERINFO, cn=FIRST_NAME, ct=STRING_TYPE))
    c.execute("ALTER TABLE {tn} ADD COLUMN '{cn}' {ct}".format(tn=USERINFO, cn=LAST_NAME, ct=STRING_TYPE))
    c.execute("ALTER TABLE {tn} ADD COLUMN '{cn}' {ct}".format(tn=USERINFO, cn=USERNAME, ct=STRING_TYPE))
    c.execute("ALTER TABLE {tn} ADD COLUMN '{cn}' {ct}".format(tn=USERINFO, cn=PASSWORD_HASH, ct=STRING_TYPE))

    c.execute('CREATE TABLE {tn} ({nf} {ft} PRIMARY KEY)'.format(tn=FILEINFO, nf=FILEID, ft=STRING_TYPE))
    c.execute("ALTER TABLE {tn} ADD COLUMN '{cn}' {ct}".format(tn=FILEINFO, cn=FILEKEY, ct=STRING_TYPE))





    # Committing changes and closing the connection to the database file
    conn.commit()
    conn.close()

#CreateTable()
