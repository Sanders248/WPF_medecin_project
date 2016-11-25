using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Media;
using System.Drawing;


namespace Medical_tp.ViewModel
{
    class GestUsersViewModel : DockPanelViewModel
    {
        #region variables
        private Model.User _selectedUser;
        private DataAccess.Users users;
        private ObservableCollection<Model.User> _listUser = null;
        private string _searchPattern;
        private ImageSource _DisplayedImage;
        private bool _closeSignal;

        #endregion

        private ICommand _addCommand;
        private ICommand _modifyCommand;
        private ICommand _changeImage;
        private ICommand _deleteCommand;

        private string _displayBtns;
        private string _readOnlyFields;

        #region getter / setter

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

        public ImageSource DisplayedImage
        {
            get { return _DisplayedImage; }
            set { _DisplayedImage = value; }
        }

        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
            set { _deleteCommand = value; }
        }

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

        public ICommand ChangeImage
        {
            get { return _changeImage; }
            set { _changeImage = value; }
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

        public string ReadOnlyFields
        {
            get { return _readOnlyFields; }

            set
            {
                if (_readOnlyFields != value)
                {
                    _readOnlyFields = value;
                    OnPropertyChanged("ReadOnlyFields");
                }
            }
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
                    try
                    {
                        DisplayedImage = Tools.LoadImage(_selectedUser.Picture);
                        OnPropertyChanged("DisplayedImage");
                    }
                    catch
                    {
                    }
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

            users = new DataAccess.Users();

            //transformation en Observable collection pour l'interface
            ListUser = new ObservableCollection<Medical_tp.Model.User>(users.getUsers());

            _displayBtns = Data.Session.Instance.VisibilityButtons();
            _readOnlyFields = Data.Session.Instance.ReadOnlyFields();

            //configuration de la commande
            AddCommand = new RelayCommand(param => AddPerson());
            ModifyCommand = new RelayCommand(param => ModifyPerson());
            DeleteCommand = new RelayCommand(param => DeletePerson());
            ChangeImage = new RelayCommand(param => Change_image());
        }

        /// <summary>
        /// action permettant d'ajouter une personne à la liste
        /// </summary>

        private void Change_image()
        {
            if (SelectedUser == null)
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
                DisplayedImage = image;
                OnPropertyChanged("DisplayedImage");
                _selectedUser.Picture = Tools.ImageToByte(image);
            }
        }

        private void AddPerson()
        {
            try
            {
                _listUser.Add(users.addNewUser());
            }
            catch { }
        }

        private void DeletePerson()
        {
            try
            {
                users.removeUser(SelectedUser);

                _listUser.Remove(SelectedUser);
            }
            catch { }
        }


        

        private bool ModifyPerson()
        {
            try
            {
                if (users.updateUser(SelectedUser) == false)
                {

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri("pack://application:,,,/View/Ressources/Erreur.jpg");
                    image.DecodePixelWidth = 250;
                    image.EndInit();
                    SelectedUser.Picture = Tools.ImageToByte(image);
                    users.updateUser(SelectedUser);
                }
            }
            catch
            {
                return false;
            }
            return true;
        } 
    }
}
