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
    class GestPatientsViewModel : DockPanelViewModel
    {
        #region variables
        private Model.Patient _selectedPatient;
        private DataAccess.Patients patients;
        private ObservableCollection<Model.Patient> _listPatient = null;
        private string _searchPattern;
        #endregion

        private ICommand _addCommand;
        private ICommand _modifyCommand;
        private ICommand _deleteCommand;


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

        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }


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
                    System.ComponentModel.ICollectionView myView = CollectionViewSource.GetDefaultView(ListPatient);
                    myView.Filter = (item) =>
                    {
                        if (item as Model.Patient == null)
                            return false;

                        Model.Patient personView = (Model.Patient)item;
                        if (personView.Firstname.ToLower().Contains(value.ToLower()) ||
                            personView.Name.ToLower().Contains(value.ToLower()))
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
            DeleteCommand = new RelayCommand(param => DeletePerson());
        }

        /// <summary>
        /// action permettant d'ajouter une personne à la liste
        /// </summary>
        private void AddPerson()
        {
            try { 
                _listPatient.Add(patients.addNewPatient());
            }
            catch { }
        }

        private void ModifyPerson()
        {
            try
            {
                patients.updatePatient(SelectedPatient);
            }
            catch { }
        }

        public void DeletePerson()
        {
            try
            {
                patients.removePatient(SelectedPatient);

                _listPatient.Remove(SelectedPatient);
            }
            catch { }
        }
    }
}
