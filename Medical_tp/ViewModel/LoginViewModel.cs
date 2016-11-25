using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Medical_tp.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        #region variables
        private DataAccess.User _dataAccessUser = null;
        private bool _closeSignal;
        private string _login;
        private string _password;
        private string _logErreur;
        #endregion

        #region commandes
        private ICommand _loginCommand;
        #endregion

        #region getter / setter
        /// <summary>
        /// mot de passe de la personne
        /// </summary>
        /// 
        public string LogErreur
        {
            get { return _logErreur; }
            set { _logErreur = value;
                OnPropertyChanged("LogErreur");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        /// <summary>
        /// login de la personne
        /// </summary>
        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged("Login");
                }

            }
        }

        /// <summary>
        /// indique si on doit fermer la fenêtre ou non
        /// </summary>
        public bool CloseSignal
        {
            get { return _closeSignal; }
            set
            {
                if (_closeSignal != value)
                {
                    _closeSignal = value;
                    OnPropertyChanged("CloseSignal");
                }
            }
        }

        /// <summary>
        /// command pour s'authentifier
        /// </summary>
        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set { _loginCommand = value; }
        }

        #endregion

        /// <summary>
        /// constructeur
        /// </summary>
        public LoginViewModel()
        {
            //init variables
            base.DisplayName = "Page de login";
            Login = "";
            Password = "";
            LogErreur = "Hidden";
            _dataAccessUser = new Medical_tp.DataAccess.User();

            //commandes
            _loginCommand = new RelayCommand(param => LoginAccess(), param => true);
        }

        
        /// <summary>
        /// action permettant de s'authentifier
        /// </summary>
        private void LoginAccess()
        {
            if (_dataAccessUser.connexion(Login, Password))
            {
                Model.User actualUser = Data.Session.Instance.ActualUser = DataAccess.User.getUser(Login);

                View.PrincipalWindow window = Medical_tp.Data.Session.Instance.GetPrincipalWindow();

                ViewModel.GestUsersViewModel vm = new GestUsersViewModel();
                window.DataContext = vm;

                if (actualUser.Role.Equals("Infirmiere") || actualUser.Role.Equals("Infirmière"))
                    window.Content = new Medical_tp.View.GestUsersInfirmiere();
                else
                    window.Content = new Medical_tp.View.GestUsers();
               
            }
            else
                LogErreur = "Visible";


        }
    }
}