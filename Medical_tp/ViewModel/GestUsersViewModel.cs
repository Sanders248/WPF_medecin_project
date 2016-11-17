﻿using System;
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
        private ServiceUser.User _selectedUser;
        private ObservableCollection<ServiceUser.User> _listUser = null;
        private DataAccess.Users _dataAccessPerson;
        private string _searchPattern;
        #endregion

        private ICommand _addCommand;


        #region getter / setter

        /// <summary>
        /// command pour ajouter une personne
        /// </summary>
        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
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
        public ObservableCollection<ServiceUser.User> ListUser
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
        public ServiceUser.User SelectedUser
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
            List<ServiceUser.User> tmpList = _dataAccessPerson.getUsers();

            //transformation en Observable collection pour l'interface
            ListUser = new ObservableCollection<Medical_tp.ServiceUser.User>(tmpList);

            //configuration de la commande
            AddCommand = new RelayCommand(param => AddPerson());
        }

        /// <summary>
        /// action permettant d'ajouter une personne à la liste
        /// </summary>
        private void AddPerson()
        {
            _listUser.Add(new Medical_tp.ServiceUser.User() { Name = "New", Firstname = "New", Login = "" });
        }

    }
}
