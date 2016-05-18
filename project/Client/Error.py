from Tkinter import *
import tkMessageBox


def server_error_msg():

    root = Tk()
    root.wm_withdraw()
    tkMessageBox.showerror('Error', 'There was an error with the server please make sure it is running.')
    root.mainloop()


def error_msg():
    root = Tk()
    root.wm_withdraw()
    tkMessageBox.showerror('Error', 'Connection error, please try again.')
    root.mainloop()
