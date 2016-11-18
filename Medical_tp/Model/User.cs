using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.Model
{
    class User
    {
        #region variables
        private string _login;
        private string _pwd;
        private string _name;
        private string _firstname;
        private Byte[] _picture;
        private string _role;
        private bool _connected;
        private int _id;
        #endregion

        public User(string login, string pwd, string name, string firstname, Byte[] picture, string role, bool connected, int id)
        {
            _login = login;
            _pwd = pwd;
            _name = name;
            _firstname = firstname;
            _picture = picture;
            _role = role;
            _connected = connected;
            _id = id;
        }

        public User(int id)
        {
            _login = "";
            _pwd = "";
            _name = "";
            _firstname = "";
            _picture = null;
            _role = "";
            _connected = false;
            _id = id;
        }

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        public Byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }

        public string Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public bool Connected
        {
            get { return _connected; }
            set { _connected = value; }
        }

        public int Index
        {
            get { return _id; }
        }

    }
}
