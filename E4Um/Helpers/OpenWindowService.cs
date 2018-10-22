using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using E4Um.Views;
using E4Um.ViewModels;
using E4Um.Helpers;

namespace E4Um.Helpers
{
    public interface IWindowService
    {
        void CreatePopUpWindow();
        void ShowPopUpWindow(string mode);
        void HidePopUpWindow(string mode);
        //void CreateMainWindow();
        //void ShowMainWindow();
    }
    class OpenWindowService : IWindowService
    {

        //public OpenWindowService(MainWindow mainWindow)
        //{
        //    _mainWidnow = mainWindow;
        //}
        public void CreatePopUpWindow()
        {
            PopUpWindow popUpWindow = new PopUpWindow("appear") { DataContext = new PopUpWindowModel(new OpenWindowService())};    
            popUpWindow.ShowActivated = false;
            popUpWindow.Show();
        }

        public void ShowPopUpWindow(string mode)
        {
            Window popUpWindow = null;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.Title == "PopUp")
                {
                    popUpWindow = window;
                }
            }
           try
           { 
                Task.Run(() =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        switch (mode)
                        {
                            case "appear":
                                DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                                popUpWindow.BeginAnimation(UIElement.OpacityProperty, animation);
                                break;
                            case "popup":
                                popUpWindow.Opacity = 1;
                                for (int i = 0; i < popUpWindow.Height + 45; i++)
                                {
                                    Application.Current.Dispatcher.Invoke((() =>
                                    {
                                        popUpWindow.Top = popUpWindow.Top - 1;
                                    }));
                                }
                                break;
                        }
                    });
                });
               }
           catch { }

        }

        public void HidePopUpWindow(string mode)
        {
            Window popUpWindow = null;

            foreach (Window window in Application.Current.Windows)
            {
                if (window.Title == "PopUp")
                {
                    popUpWindow = window;
                }
            }

            Point pt = SystemParameters.WorkArea.TopLeft;
            pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
            pt.Offset(-popUpWindow.Width, 0);

            try
            {
                Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    Application.Current.Dispatcher.Invoke(() =>
                {
                    DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.8));
                    popUpWindow.BeginAnimation(UIElement.OpacityProperty, animation);

                    if (mode == "popup")
                    {
                        popUpWindow.Left = pt.X - 5;
                        popUpWindow.Top = pt.Y + 40;
                        popUpWindow.BeginAnimation(UIElement.OpacityProperty, null);
                    }
                });
                });
            }
            catch { }
        }

        //public void CreateMainWindow()
        //{
        //    MainWindow mainWindow = new MainWindow() { DataContext = new MainWindowModel()};
        //    mainWindow.ShowActivated = true;
        //    mainWindow.Show();
        //}

        //public void ShowMainWindow()
        //{
        //    _mainWidnow.Show();
        //}
    }
}
