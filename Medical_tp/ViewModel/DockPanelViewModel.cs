using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Medical_tp.ViewModel
{
    class DockPanelViewModel : BaseViewModel
    {
        #region variables
        private ICommand _openPatientViewCommand;
        private ICommand _openUserViewCommand;
        #endregion

        #region getter / setter
        public ICommand OpenPatientViewCommand
        {
            get { return _openPatientViewCommand; }
            set { _openPatientViewCommand = value; }
        }

        public ICommand OpenUserViewCommand
        {
            get { return _openUserViewCommand; }
            set { _openUserViewCommand = value; }
        }
        #endregion

        public DockPanelViewModel()
        {

            OpenPatientViewCommand = new RelayCommand(param => OpenPatientView());
            OpenUserViewCommand = new RelayCommand(param => OpenUserView());
        }

        protected static void OpenPatientView()
        {
            Model.User actualUser = Data.Session.Instance.ActualUser;

            View.PrincipalWindow window = Medical_tp.Data.Session.Instance.GetPrincipalWindow();

            ViewModel.GestPatientsViewModel vm = new GestPatientsViewModel();
            window.DataContext = vm;


            if (actualUser.Role.Equals("Infirmiere") || actualUser.Role.Equals("Infirmière"))
            {
                window.Content = new Medical_tp.View.GestPatientsInfirmiere();
            }
            else
            {
                window.Content = new Medical_tp.View.GestPatients();
            }


            // CloseSignal = true;
        }

        protected static void OpenUserView()
        {
            Model.User actualUser = Data.Session.Instance.ActualUser;

            View.PrincipalWindow window = Medical_tp.Data.Session.Instance.GetPrincipalWindow();

            ViewModel.GestUsersViewModel vm = new GestUsersViewModel();
            window.DataContext = vm;


            if (actualUser.Role.Equals("Infirmiere") || actualUser.Role.Equals("Infirmière"))
            {
                window.Content = new Medical_tp.View.GestUsersInfirmiere();
            }
            else
            {
                window.Content = new Medical_tp.View.GestUsers();
            }


            // CloseSignal = true;
        }
    }
    
}
