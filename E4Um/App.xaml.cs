using System.Windows;
using E4Um.ViewModels;
using E4Um.Helpers;

namespace E4Um
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindowModel mainVM = new MainWindowModel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new MainWindow() { DataContext = mainVM }.Show();
        }
    }
}
