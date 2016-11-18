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

        //todo think to update the index when delete add, move elmts
        public void updateUser(int index)
        {
            ServiceUser.User u = null;
            try
            {
                u = serviceClient.GetListUser()[index];
            }
            catch
            { }
            if (u != null)
            {
                string previousLogin = u.Login;

                serviceClient.DeleteUser(previousLogin);

                ServiceUser.User servUsr = new ServiceUser.User();
                servUsr.Firstname = _listUser[index].Firstname;
                servUsr.Login = _listUser[index].Login;
                servUsr.Name = _listUser[index].Name;
                servUsr.Picture = _listUser[index].Picture;
                servUsr.Pwd = _listUser[index].Pwd;
                servUsr.Role = _listUser[index].Role;

                serviceClient.AddUser(servUsr);
            }
        }

        public Model.User addNewUser()
        {
            Model.User u = new Model.User(_listUser.Capacity);
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
            serviceClient.DeleteUser(user.Login);
            _listUser.Remove(user);
        }

        private void LoadUsers()
        {
            int i = 0;
            try
            {
                foreach (ServiceUser.User u in serviceClient.GetListUser())
                {
                    _listUser.Add(new Model.User(u.Login, u.Pwd, u.Name, u.Firstname, u.Picture, u.Role, u.Connected, i));
                    ++i;
                }
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

