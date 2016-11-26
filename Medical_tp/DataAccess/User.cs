using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Medical_tp.DataAccess
{
    class User
    {
        public static ServiceUser.ServiceUserClient serviceClient = new ServiceUser.ServiceUserClient();
      
        public User()
        {
        }

        public Model.User getUserFromList(List<Model.User> listUsr, string login)
        {
            foreach (Model.User u in listUsr)
                if (u.Login.Equals(login))
                    return u;

            return null;
        }

        public static Model.User getUser(string login)
        {
            ServiceUser.User servUser = serviceClient.GetUser(login);
            Model.User u = new Model.User(servUser.Login, servUser.Pwd, servUser.Name, servUser.Firstname, servUser.Picture, servUser.Role, servUser.Connected);

            return u;
        }

        public void updateUser(Model.User user)
        {
            ServiceUser.User u = serviceClient.GetUser(user.RefLogin);
            string previousLogin = u.Login;

            if (!u.Firstname.Equals(user.Firstname))
                serviceClient.GetUser(previousLogin).Firstname = user.Firstname;

            u.Login = user.Login;
            u.Name = user.Name;
            u.Picture = user.Picture;
            u.Pwd = user.Pwd;
            u.Role = user.Role;
            u.Connected = user.Connected;
        }

        public bool connexion(string login, string pwd)
        {
            Boolean connected = false;
            try
            {
                connected = serviceClient.Connect(login, pwd);
            }
            catch {
            }
            return connected;

        }

    
    }
}
