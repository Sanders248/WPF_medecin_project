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
        private ImageSource _DisplayedImage;
       
        #endregion

        private ICommand _addCommand;
        private ICommand _modifyCommand;
        private ICommand _changeImage;


        #region getter / setter

        /// <summary>
        /// command pour ajouter une personne
        /// </summary>


        public ImageSource DisplayedImage
        {
            get { return _DisplayedImage; }
            set { _DisplayedImage = value; }
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

        public ICommand changeImage
        {
            get { return _changeImage; }
            set { _changeImage = value; }

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
                        DisplayedImage = LoadImage(_selectedUser.Picture);
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
            
            _dataAccessPerson = new Medical_tp.DataAccess.Users();
            //chargement des personnes


            users = new DataAccess.Users();

            //transformation en Observable collection pour l'interface
            ListUser = new ObservableCollection<Medical_tp.Model.User>(users.getUsers());
           

            //configuration de la commande
            AddCommand = new RelayCommand(param => AddPerson());
            ModifyCommand = new RelayCommand(param => ModifyPerson());
            changeImage = new RelayCommand(param => change_image());
        }

        /// <summary>
        /// action permettant d'ajouter une personne à la liste
        /// </summary>

        private void change_image()
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.JPG)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dlg.ShowDialog();

            
            if (result == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(dlg.FileName);
                image.EndInit();
                DisplayedImage = image;
                OnPropertyChanged("DisplayedImage");
                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
                _selectedUser.Picture = data;
            }
        }

        private void AddPerson()
        {
            _listUser.Add(users.addNewUser());
        }
    

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            
            using (var mem = new MemoryStream(imageData))
            {
                image.CacheOption = BitmapCacheOption.None;
                mem.Position = 0;
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.None;
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            return image;
        }


        private void ModifyPerson()
        {
            users.updateUser(SelectedUser.Index);
        }

    }
}
