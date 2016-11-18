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

                //need to update service
                //1st way dont work
                if (!u.Firstname.Equals(_listUser[index].Firstname))
                    serviceClient.GetUser(previousLogin).Firstname = _listUser[index].Firstname;

                //2nd way dont work either
                u.Login = _listUser[index].Login;
                u.Name = _listUser[index].Name;
                u.Picture = _listUser[index].Picture;
                u.Pwd = _listUser[index].Pwd;
                u.Role = _listUser[index].Role;
                u.Connected = _listUser[index].Connected;
            }
        }

        public Model.User addNewUser()
        {
            Model.User u = new Model.User(_listUser.Capacity);
            
            _listUser.Add(u);

            return u;
        }
        /// <summary>
        /// charge les users
        /// </summary>

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

