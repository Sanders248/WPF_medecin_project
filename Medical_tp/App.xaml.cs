using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Medical_tp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //  View.LoginView window = new Medical_tp.View.LoginView();

            View.PrincipalWindow window = Medical_tp.Data.Session.Instance.GetPrincipalWindow();
            
            ViewModel.LoginViewModel vm = new Medical_tp.ViewModel.LoginViewModel();
            window.DataContext = vm;
            
            window.Content = new Medical_tp.View.LoginView();

            window.Show();
        }
    }
}
