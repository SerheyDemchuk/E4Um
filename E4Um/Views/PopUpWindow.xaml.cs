using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Media.Animation;
using E4Um.ViewModels;
using E4Um.Helpers;

namespace E4Um.Views
{
    /// <summary>
    /// Логика взаимодействия для PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : Window
    {
        Point pt = SystemParameters.WorkArea.TopLeft;

        public PopUpWindow(string mode)
        {
            InitializeComponent();
            
            // Setting the default window's position
            switch (mode)
            {
                case "appear":
                    pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
                    pt.Offset(-Width, -Height);
                    Left = pt.X - 5;
                    Top = pt.Y - 5;
                    Opacity = 0;
                    break;
                case "popup":
                    pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
                    pt.Offset(-Width, 0);
                    Left = pt.X - 5;
                    Top = pt.Y;
                    Opacity = 1;
                    break;
            }
            // /Setting the default window's position

            Task.Run(() =>
            {
                showWindow(mode);
                closeWindow(mode);
            });


        }

        public void showWindow(string mode)
        {
            Dispatcher.Invoke(() =>
            {
                switch (mode)
                {
                    case "appear":
                        DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                        BeginAnimation(OpacityProperty, animation);
                        
                        break;
                    case "popup":
                        for (int i = 0; i < Height + 5; i++)
                        {
                            Dispatcher.Invoke((() =>
                            {
                                Top = Top - 1;
                            }));
                        }
                        break;
                }
            });
        }

        public void closeWindow(string mode)
        {
            Thread.Sleep(1000);
            Dispatcher.Invoke(() =>
            {
                DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.8));
                BeginAnimation(OpacityProperty, animation);

                if (mode == "popup")
                {
                    Left = pt.X - 5;
                    Top = pt.Y + 40;
                    BeginAnimation(OpacityProperty, null);
                }
            });

            
        }

    }
}
