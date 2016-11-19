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

        //todo see what happend when we add a enw user then modify it
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

      /*  public void setFirstname(string login, string newFirstName)
        {
            serviceClient.GetUser(login).Firstname = newFirstName;
        }

        public void setLogin(string login, string newLogin)
        {
            serviceClient.GetUser(login).Login = newLogin;
>>>>>>> 7dff60599db1789343a13c06ad270bb20fb32626
        }

        public void setName(string login, string newName)
        {
            serviceClient.GetUser(login).Name = newName;
        }

        public void setPicture(string login, byte[] newPicture)
        {
            serviceClient.GetUser(login).Picture = newPicture;
        }

        public void setPassword(string login, string newPwd)
        {
            serviceClient.GetUser(login).Pwd = newPwd;
        }

        public void setRole(string login, string newRole)
        {
            serviceClient.GetUser(login).Role = newRole;
        }
        */
    }
}
