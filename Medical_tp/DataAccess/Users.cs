using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.DataAccess
{
    class Users
    {
        /// <summary>
        /// liste des utilisateurs
        /// </summary>
        private List<Model.User> _listUser;
        public static ServiceUser.ServiceUserClient serviceClient = new ServiceUser.ServiceUserClient();

        /// <summary>
        /// construteur
        /// </summary>
        public Users()
        {
            _listUser = new List<Model.User>();
            LoadUsers();
        }

        private bool checkUniqLogin(Model.User testUser)
        {
            if (testUser.RefLogin.Equals(testUser.Login))
                return true;

            foreach (ServiceUser.User u in serviceClient.GetListUser())
                if (u.Login == testUser.Login)
                    return false;

            return true;
        }

        public bool updateUser(Model.User user)
        {
            if (!checkUniqLogin(user))
                return false;

            try
            {
                serviceClient.DeleteUser(user.RefLogin);
                user.RefLogin = user.Login;

                ServiceUser.User servUsr = new ServiceUser.User();

                servUsr.Firstname = user.Firstname;
                servUsr.Login = user.Login;
                servUsr.Name = user.Name;
                servUsr.Picture = user.Picture;
                servUsr.Pwd = user.Pwd;
                servUsr.Role = user.Role;
                serviceClient.AddUser(servUsr);
            }
            catch
            {
                return false;
            }

            return true;
        }
        
        public Model.User addNewUser()
        {
            Model.User u = new Model.User();

            _listUser.Add(u);

            ServiceUser.User servUsr = new ServiceUser.User();
            servUsr.Firstname = u.Firstname;
            servUsr.Login = u.Login;
            servUsr.Name = u.Name;
            servUsr.Picture = u.Picture;
            servUsr.Pwd = u.Pwd;
            servUsr.Role = u.Role;

            serviceClient.AddUser(servUsr);

            return u;
        }

        public void removeUser(Model.User user)
        {
            try
            {
                serviceClient.DeleteUser(user.RefLogin);
                _listUser.Remove(user);
            }
            catch
            { }
        }

        private void LoadUsers()
        {
            try
            {
                foreach (ServiceUser.User u in serviceClient.GetListUser())
                    _listUser.Add(new Model.User(u.Login, u.Pwd, u.Name, u.Firstname, u.Picture, u.Role, u.Connected));
            }
            catch
            {
            }
        }

        public List<Model.User> getUsers()
        {
            return _listUser;
        }
    }
}

