using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace Medecin_project.DataAccess
{
    public class Users
    {
        /// <summary>
        /// liste des utilisateurs
        /// </summary>
        private List<Model.User> _listUser = null;

        /// <summary>
        /// construteur
        /// </summary>
        public Users()
        {
            //init variables
            _listUser = new List<Medecin_project.Model.User>();
            LoadUser();
        }

        /// <summary>
        /// valide ou non l'authentification d'un utilisateur
        /// </summary>
        /// <param name="name">login du user</param>
        /// <param name="pwd">password du user</param>
        /// <returns>vrai si login/psswd bon sinon faux</returns>
        public bool TestUser(string name, string pwd)
        {
            return _listUser.Where(x => x.Name == name && x.Pwd == pwd).Any();
        }

        /// <summary>
        /// charge les users contenus dans un fichier XML
        /// </summary>
        private void LoadUser()
        {
            try
            {
                XDocument doc = XDocument.Load("Data/users.xml");
                _listUser = (from tmpUser in doc.Element("users").Elements("user")
                             select Model.User.CreateUser(
                             tmpUser.Element("name").Value,
                             tmpUser.Element("pwd").Value)).ToList();
            }
            catch (Exception ex)
            {
                //traitement exception ...
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
