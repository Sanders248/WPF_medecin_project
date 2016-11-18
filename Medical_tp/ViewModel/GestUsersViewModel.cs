using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Medical_tp.ViewModel
{
    class GestUsersViewModel : BaseViewModel
    {
        #region variables
        private Model.User _selectedUser;
        private DataAccess.Users users;
        private ObservableCollection<Model.User> _listUser = null;
        private DataAccess.Users _dataAccessPerson;
        private string _searchPattern;
        #endregion

        private ICommand _addCommand;
        private ICommand _modifyCommand;


        #region getter / setter

        /// <summary>
        /// command pour ajouter une personne
        /// </summary>
        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        public ICommand ModifyCommand
        {
            get { return _modifyCommand; }
            set { _modifyCommand = value; }
        }

        /// <summary>
        /// filtre de recherche
        /// </summary>
        public string SearchPattern
        {
            get { return _searchPattern; }
            set
            {
                if (_searchPattern != value)
                {
                    _searchPattern = value;
                    //utilisation des propriétés de vue d'une collection 
                    //pour faire des filtres dessus
                    System.ComponentModel.ICollectionView myView = CollectionViewSource.GetDefaultView(ListUser);
                    myView.Filter = (item) =>
                    {
                        if (item as ServiceUser.User == null)
                            return false;

                        ServiceUser.User personView = (ServiceUser.User)item;
                        if (personView.Firstname.Contains(value) ||
                            personView.Name.Contains(value))
                            return true;

                        return false;
                    };
                    //indique à l'interface de se rafraichir
                    OnPropertyChanged("SearchPattern");
                }
            }
        }

        /// <summary>
        /// contient la liste des utilisateurs
        /// </summary>
        public ObservableCollection<Model.User> ListUser
        {
            get { return _listUser; }
            set
            {
                if (_listUser != value)
                {
                    _listUser = value;
                    OnPropertyChanged("ListUser");
                }
            }
        }

        /// <summary>
        /// personne sélectionnée dans la liste
        /// </summary>
        public Model.User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged("SelectedUser");
                }
            }
        }
        #endregion

        /// <summary>
        /// constructeur
        /// </summary>
        public GestUsersViewModel()
        {
            DisplayName = "Display User";

            _dataAccessPerson = new Medical_tp.DataAccess.Users();
            //chargement des personnes

            users = new DataAccess.Users();

            //transformation en Observable collection pour l'interface
            ListUser = new ObservableCollection<Medical_tp.Model.User>(users.getUsers());

            //configuration de la commande
            AddCommand = new RelayCommand(param => AddPerson());
            ModifyCommand = new RelayCommand(param => ModifyPerson());
        }

        /// <summary>
        /// action permettant d'ajouter une personne à la liste
        /// </summary>
        private void AddPerson()
        {
            _listUser.Add(users.addNewUser());

            //allow to verify change from user on service // Delete this after verification
          //  ServiceUser.ServiceUserClient serviceClient = new ServiceUser.ServiceUserClient();
          //  ServiceUser.User[] us = serviceClient.GetListUser();
        }
       
        private void ModifyPerson()
        {
            users.updateUser(SelectedUser.Index);
        }

    }
}
