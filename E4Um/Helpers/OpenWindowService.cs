using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Reflection;
using E4Um.Views;
using E4Um.ViewModels;
using E4Um.Helpers;
using E4Um.AppSettings;

namespace E4Um.Helpers
{
    public interface IWindowService
    {
        void CreatePopUpWindow(string mode, int delayMilliSeconds, string popUpSizeToContent);
        void ShowPopUpWindow(string mode);
        void HidePopUpWindow(string mode);
        //void CreateMainWindow();
        //void ShowMainWindow();
    }
    class OpenWindowService : IWindowService
    {
        SizeChangedEventHandler handler;
        //public OpenWindowService(MainWindow mainWindow)
        //{
        //    _mainWidnow = mainWindow;
        //}
        public void CreatePopUpWindow(string mode, int delayMilliSeconds, string popUpSizeToContent)
        {
            Point pt = SystemParameters.WorkArea.TopLeft;
            PopUpWindow popUpWindow = new PopUpWindow(mode) { DataContext = new PopUpWindowModel(new OpenWindowService(), new ConfigProvider())};

            switch (mode)
            {
                case "default":
                    popUpWindow.Loaded += (object sender, RoutedEventArgs e) =>
                    {
                        pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
                        pt.Offset(-popUpWindow.Width, -popUpWindow.Height);
                        popUpWindow.Left = pt.X - 5;
                        popUpWindow.Top = pt.Y - 5;
                        popUpWindow.Opacity = 1;
                    };
                    popUpWindow.ShowActivated = false;
                    popUpWindow.Show();
                    break;
                case "appear":
                    DoubleAnimation fadeIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                    popUpWindow.BeginAnimation(UIElement.OpacityProperty, fadeIn);

                    Task.Run(() =>
                    {
                        Thread.Sleep(delayMilliSeconds);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            DoubleAnimation fadeOut = new DoubleAnimation(0, TimeSpan.FromSeconds(0.8));
                            popUpWindow.BeginAnimation(UIElement.OpacityProperty, fadeOut);
                        });
                    });
                    popUpWindow.Loaded += (object sender, RoutedEventArgs e) =>
                    {
                        pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
                        pt.Offset(-popUpWindow.Width, -popUpWindow.Height);
                        popUpWindow.Left = pt.X - 5;
                        popUpWindow.Top = pt.Y - 5;
                        popUpWindow.Opacity = 0;
                    };
                    popUpWindow.ShowActivated = false;
                    popUpWindow.Show();
                    break;
                case "popup":
                    popUpWindow.Loaded += (object sender, RoutedEventArgs e) =>
                    {
                        pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
                        pt.Offset(-popUpWindow.Width, 2*popUpWindow.Height);
                        popUpWindow.Left = pt.X;
                        popUpWindow.Top = pt.Y;
                        popUpWindow.Opacity = 1;
                    };
                    popUpWindow.ShowActivated = false;
                    popUpWindow.Show();
                    break;
            }
            
            
        }

        public void ShowPopUpWindow(string mode)
        {
            Point pt = SystemParameters.WorkArea.TopLeft;
            Window popUpWindow = null;
            DoubleAnimation fadeIn = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));

            foreach (Window window in Application.Current.Windows)
            {
                if (window.Title == "PopUp")
                {
                    popUpWindow = window;
                }
            }

            switch (mode)
            {
                case "appear":
                    if (popUpWindow.SizeToContent.ToString() == "Width")
                    {
                        popUpWindow.BeginAnimation(UIElement.OpacityProperty, fadeIn);

                        handler += (object sender, SizeChangedEventArgs e) =>
                        {
                            popUpWindow.SizeChanged -= handler;
                            pt.X = 0;
                            pt.Y = 0;
                            pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
                            pt.Offset(-popUpWindow.ActualWidth, -popUpWindow.ActualHeight);
                            popUpWindow.Left = pt.X - 5;
                            popUpWindow.Top = pt.Y - 5;
                        };
                        popUpWindow.SizeChanged += handler;
                    }
                    else if(popUpWindow.SizeToContent.ToString() == "Manual")
                    {
                        popUpWindow.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                    }

                    break;
                case "popup":
                    popUpWindow.Opacity = 1;
                    pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
                    pt.Offset(-popUpWindow.ActualWidth, 45);
                    popUpWindow.Left = pt.X - 5;
                    popUpWindow.Top = pt.Y - 5;

                    for (int i = 0; i < popUpWindow.Height + 45; i++)
                    {
                        Application.Current.Dispatcher.Invoke((() =>
                        {
                            popUpWindow.Top = popUpWindow.Top - 1;
                        }));
                    }

                    break;
                case "default":
                    popUpWindow.BeginAnimation(UIElement.OpacityProperty, fadeIn);
                    pt.X = 0;
                    pt.Y = 0;
                    pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
                    pt.Offset(-popUpWindow.ActualWidth, -popUpWindow.ActualHeight);
                    popUpWindow.Left = pt.X - 5;
                    popUpWindow.Top = pt.Y - 5;
                    break;
            }

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
            if(mode == "appear")
            {
                DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.8));
                popUpWindow.BeginAnimation(UIElement.OpacityProperty, animation);
            } 
            else if(mode == "popup")
            {
                popUpWindow.Top += popUpWindow.Height + 45;
                popUpWindow.BeginAnimation(UIElement.OpacityProperty, null);
            }
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
