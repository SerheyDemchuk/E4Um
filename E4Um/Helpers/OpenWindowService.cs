using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using E4Um.Views;
using E4Um.ViewModels;

namespace E4Um.Helpers
{
    public interface IWindowService
    {
        void CreatePopUpWindow();
        void CreateMainWindow();
        //void ShowMainWindow();
    }
    class OpenWindowService : IWindowService
    {
        //public OpenWindowService()
        //{

        //}

        //public OpenWindowService(MainWindow mainWindow)
        //{
        //    _mainWidnow = mainWindow;
        //}
        public void CreatePopUpWindow()
        {
            PopUpWindow popUpWindow = new PopUpWindow("appear");
            popUpWindow.ShowActivated = false;
            popUpWindow.Show();
        }

        public void CreateMainWindow()
        {
            MainWindow mainWindow = new MainWindow() { DataContext = new MainWindowModel()};
            mainWindow.ShowActivated = true;
            mainWindow.Show();
        }

        //public void ShowMainWindow()
        //{
        //    _mainWidnow.Show();
        //}
    }
}
