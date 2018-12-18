using System.Windows;
using E4Um.AppSettings;
using E4Um.Helpers;
using E4Um.ViewModels;
using E4Um.Views;

namespace E4Um
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindowModel mainVM = new MainWindowModel(new Models.PopUp(new OpenWindowService()), new ConfigProvider(), new OpenWindowService());
        PopUpWindowModel popUpVM = new PopUpWindowModel(new Models.PopUp(new OpenWindowService()), new OpenWindowService(), new ConfigProvider());

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new MainWindow(new ConfigProvider()) { DataContext = mainVM }.Show();
            new PopUpWindow() { DataContext = popUpVM }.Show();
        }

    }
}
