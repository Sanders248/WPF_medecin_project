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
using System.Windows.Media.Imaging;

namespace Medical_tp.ViewModel
{
    class ObservationViewModel : DockPanelViewModel
    {
        private Patient _current_patient;
        private string _searchPattern;
        private ObservableCollection<Model.Observation> _listObservation = null;
        private ObservableCollection<ImageSource> _observationImage = null;
        private Observation _selectedObservation;
        private Model.Live _liveObs;
        private string _displayCreateBtn;
        private string _displayBtns;

        private ICommand _addCommand;
        private ICommand _createCommand;
        private ICommand _changeImage;

        public ICommand CreateCommand
        {
            get { return _createCommand; }
            set { _createCommand = value; }
        }
        public ICommand ChangeImage
        {
            get { return _changeImage; }
            set { _changeImage = value; }
        }
        public ICommand AddCommand
        {
            get { return _addCommand; }
            set { _addCommand = value; }
        }

        public string DisplayBtns
        {
            get { return _displayBtns; }

            set
            {
                if (_displayBtns != value)
                {
                    _displayBtns = value;
                    OnPropertyChanged("DisplayBtns");
                }
            }
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
        }

        public ObservationViewModel(Patient patient)
        {
            DisplayName = "Display Observations";


            _displayCreateBtn = "Hidden";
            _current_patient = patient;
            _displayBtns = Data.Session.Instance.VisibilityButtons();

            ListObservation = new ObservableCollection<Observation>(_current_patient.Observations);

            ObservationImage = new ObservableCollection<ImageSource>();
            init_imagetab();
            //configuration de la commande
            AddCommand = new RelayCommand(param => AddObservation());
            CreateCommand = new RelayCommand(param => CreateObservation());
            ChangeImage = new RelayCommand(param => Change_image());


            DataAccess.Live.delegateUpdateLive syncLiveDelegate = syncLive;
            DataAccess.Live dataLive = new DataAccess.Live(syncLiveDelegate);
            _liveObs = dataLive.LiveObs;

            System.ServiceModel.InstanceContext instContext = new System.ServiceModel.InstanceContext(dataLive);
            ServiceLive.ServiceLiveClient slc = new ServiceLive.ServiceLiveClient(instContext);
            try
            { 
                slc.Subscribe();
            }catch
            {   }
          
         }
       
        /// <summary>
        /// filtre de recherche
        /// </summary>
        /// 

        public void Change_image()
        {

            if (_selectedObservation == null)
                return;

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(dlg.FileName);
                image.DecodePixelWidth = 250;
                image.EndInit();
                ObservationImage.Add(image);
                OnPropertyChanged("ObservationImage");
            }
        }
    

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
        public ObservableCollection<ImageSource> ObservationImage
        {
            get { return _observationImage; }
            set
            {
                if (_observationImage != value)
                {
                    _observationImage = value;
                    OnPropertyChanged("ObservationImage");

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
                    init_imagetab();
                    OnPropertyChanged("ObservationImage");
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
        private void init_imagetab()
        {
            
            if(SelectedObservation != null)
            {
                ObservationImage.Clear();
                if(SelectedObservation.Pictures != null)
                foreach (Byte[] b in SelectedObservation.Pictures)
                {
                    ObservationImage.Add(Tools.LoadImage(b));
                }
            }

        }

         public Model.Live LiveObs
        {
            get { return _liveObs; }
            set
            {
                if (_liveObs != value)
                {
                    _liveObs = value;
                   
                    OnPropertyChanged("LiveObs");
                }
            }
        }
        
        public void syncLive()
        {
            OnPropertyChanged("LiveObs");
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
                SelectedObservation.Pictures = Tools.ImageArrayToByteArray(ObservationImage);
                DataAccess.Observation.AddObservation(_current_patient, _selectedObservation);
                _selectedObservation.Exist = "True";
            }
            catch { }
        }
    }
    
}
