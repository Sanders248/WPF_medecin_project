using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Medecin_project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            View.LoginView window = new Medecin_project.View.LoginView();

            ViewModel.LoginViewModel vm = new Medecin_project.ViewModel.LoginViewModel();
            window.DataContext = vm;

            window.Show();
        }
    }
}
