using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>


    public partial class App : Application
    {
        internal UnityContainer container = new UnityContainer();
        public App()
        {
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            container.RegisterType<MainWindow>();
            container.RegisterType<NewBook>();
            container.RegisterType<AppVM>();
            container.RegisterType<BookVM>();
            container.RegisterType<ICommandsMethods, CommandsAppVMMethods>();
            MainWindow mainWindow = container.Resolve<MainWindow>();
            mainWindow.DataContext = container.Resolve<AppVM>();
            mainWindow.Show();
        }

    }
}
