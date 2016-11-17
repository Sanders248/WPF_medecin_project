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
        private List<ServiceUser.User> _listUser = new List<ServiceUser.User>(); // warning be sure that is correct
        public static ServiceUser.ServiceUserClient serviceClient = new ServiceUser.ServiceUserClient();

        /// <summary>
        /// construteur
        /// </summary>
        public Users()
        {
            //init variables
            //  _listUser = new List<Medical_tp.Model.User>();
            
            LoadUsers();
        }

        /// <summary>
        /// charge les users
        /// </summary>
        private async void LoadUsers()
        {

                
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = 65536 * 2;
                ServiceUser.User[] users =  serviceClient.GetListUser();
               
          
        }

        public List<ServiceUser.User> getUsers()
        {
            return _listUser;
        }
    }
}

