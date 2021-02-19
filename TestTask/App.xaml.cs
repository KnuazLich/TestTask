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
            container.RegisterType<DbOperations>();
            CommandsAppVMMethods cm = container.Resolve<CommandsAppVMMethods>();
            DbOperations o = container.Resolve<DbOperations>();
            MainWindow mainWindow = container.Resolve<MainWindow>();
            mainWindow.DataContext = container.Resolve<AppVM>();
            cm.BookBack += o.update_book_db;
            cm.BookRemove += o.remove_from_bd;
            mainWindow.Show();
        }

    }
}
