using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.DataAccess
{
    class Users
    {
        /// <summary>
        /// liste des utilisateurs
        /// </summary>
        private List<ServiceUser.User> _listUser = null; // warning be sure that is correct
        public static ServiceUser.ServiceUserClient serviceClient = new ServiceUser.ServiceUserClient();

        /// <summary>
        /// construteur
        /// </summary>
        public Users()
        {
            //init variables
            //  _listUser = new List<Medical_tp.Model.User>();
            _listUser = new List<ServiceUser.User>();
            LoadUsers();
        }

        /// <summary>
        /// charge les users
        /// </summary>
        private void LoadUsers()
        {   
                foreach (ServiceUser.User u in serviceClient.GetListUser())
                    _listUser.Add(u);
        }

        public List<ServiceUser.User> getUsers()
        {
            return _listUser;
        }
    }
}

