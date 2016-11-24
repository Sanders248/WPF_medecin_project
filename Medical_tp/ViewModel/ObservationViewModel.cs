using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_tp.Model;
using System.Windows.Input;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Medical_tp.ViewModel
{
    class ObservationViewModel : DockPanelViewModel
    {
        private Patient _current_patient;
        private string _searchPattern;
        private ObservableCollection<Model.Observation> _listObservation = null;
        private Observation _selectedObservation;
        private string _displayCreateBtn;

        private ICommand _addCommand;
        private ICommand _createCommand;

       public ICommand CreateCommand
        {
            get { return _createCommand; }
            set { _createCommand = value; }
        }

        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        public Patient Current_patient
        {
            get { return _current_patient; }
            set { _current_patient = value; }
        }

        public string DisplayCreatBtn
        {
            get { return _displayCreateBtn; }

            set
            {
                if (_displayCreateBtn != value)
                {
                    _displayCreateBtn = value;
                    OnPropertyChanged("DisplayCreatBtn");
                }
            }
        }


        private string VisibilityCreatButton()
        {
            if (_selectedObservation.Exist.Equals("True"))
                return "Hidden";
            else
                return "Visible";

        //    OnPropertyChanged("SelectedObservation");
        }

        public ObservationViewModel(Patient patient)
        {
            DisplayName = "Display Observations";

            _displayCreateBtn = "Hidden";
            _current_patient = patient;
            ListObservation = new ObservableCollection<Observation>(_current_patient.Observations);

            //configuration de la commande
            AddCommand = new RelayCommand(param => AddObservation());
            CreateCommand = new RelayCommand(param => CreateObservation());
         //   DeleteCommand = new RelayCommand(param => DeletePerson());
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

                    System.ComponentModel.ICollectionView myView = CollectionViewSource.GetDefaultView(ListObservation);
                    myView.Filter = (item) =>
                    {
                        if (item as Model.Observation == null)
                            return false;

                        Model.Observation obsView = (Model.Observation)item;
                        if (obsView.Date.CompareTo(value) < 1)
                            return true;

                        return false;
                    };

                    OnPropertyChanged("SearchObservation");
                }
            }
        }

        /// <summary>
        /// contient la liste des utilisateurs
        /// </summary>
        public ObservableCollection<Model.Observation> ListObservation
        {
            get { return _listObservation; }
            set
            {
                if (_listObservation != value)
                {
                    _listObservation = value;
                    OnPropertyChanged("ListObservation");

                }
            }
        }

        /// <summary>
        /// personne sélectionnée dans la liste
        /// </summary>
        public Model.Observation SelectedObservation
        {
            get { return _selectedObservation; }
            set
            {
                if (_selectedObservation != value)
                {
                    _selectedObservation = value;
                    DisplayCreatBtn = VisibilityCreatButton();
                    OnPropertyChanged("SelectedObservation");
                    
                    /*   try
                       {
                           DisplayedImage = LoadImage(_selectedUser.Picture);
                           OnPropertyChanged("DisplayedImage");
                       }
                       catch
                       {
                       }*/
                }
            }
        }

        private void AddObservation()
        {
            try
            {
                Model.Observation obs = new Model.Observation();
                _listObservation.Add(obs);
            }
            catch { }
        }

         private void CreateObservation()
        {
            try
            {
                 DataAccess.Observation.AddObservation(_current_patient, _selectedObservation);
                _selectedObservation.Exist = "True";
            }
            catch { }
        }
        
    }
    
}
