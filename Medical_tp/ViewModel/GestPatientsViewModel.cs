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
    class GestPatientsViewModel : BaseViewModel
    {
        #region variables
        private Model.Patient _selectedPatient;
        private DataAccess.Patients patients;
        private ObservableCollection<Model.Patient> _listPatient = null;
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

        // todo 
       /* public string SearchPattern
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
                        if (item as Model.User == null)
                            return false;

                        Model.User personView = (Model.User)item;
                        if (personView.Firstname.ToLower().Contains(value.ToLower()) ||
                            personView.Name.ToLower().Contains(value.ToLower()))
                            return true;

                        return false;
                    };
                    //indique à l'interface de se rafraichir
                    OnPropertyChanged("SearchPattern");
                }
            }
        }*/

        /// <summary>
        /// contient la liste des utilisateurs
        /// </summary>
        public ObservableCollection<Model.Patient> ListPatient
        {
            get { return _listPatient; }
            set
            {
                if (_listPatient != value)
                {
                    _listPatient = value;
                    OnPropertyChanged("ListPatient");
                }
            }
        }

        /// <summary>
        /// personne sélectionnée dans la liste
        /// </summary>
        public Model.Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                if (_selectedPatient != value)
                {
                    _selectedPatient = value;
                    OnPropertyChanged("SelectedPatient");
                }
            }
        }
        #endregion

        /// <summary>
        /// constructeur
        /// </summary>
        public GestPatientsViewModel()
        {
            DisplayName = "Display Patients";
            
            patients = new DataAccess.Patients();

            //transformation en Observable collection pour l'interface
            ListPatient = new ObservableCollection<Medical_tp.Model.Patient>(patients.getPatients());

            //configuration de la commande
            AddCommand = new RelayCommand(param => AddPerson());
            ModifyCommand = new RelayCommand(param => ModifyPerson());
        }

        /// <summary>
        /// action permettant d'ajouter une personne à la liste
        /// </summary>
        private void AddPerson()
        {
            //todo
           // _listPatient.Add(patients.addNewUser());
        }
       
        private void ModifyPerson()
        {
            //todo
          //  patients.updateUser(SelectedUser.Index);
        }
        
    }
}
