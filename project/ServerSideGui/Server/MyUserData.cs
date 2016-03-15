using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class MyUserData
    {
        private string uid;
        private string firstname;
        private string lastname;

        public MyUserData(string uid, string firstname, string lastname)
        {
            this.uid = uid;
            this.firstname = firstname;
            this.lastname = lastname;
        }
        public string GetFirstName()
        {
            return this.firstname;
        }
        public string GetLasttName()
        {
            return this.lastname;
        }
        public string GetUID()
        {
            return this.uid;
        }
    }
}
