using System.Windows;
using System.Windows.Input;

namespace E4Um.Views
{
    /// <summary>
    /// Логика взаимодействия для PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : Window
    {
        Point pt = SystemParameters.WorkArea.TopLeft;
        public PopUpWindow()
        {
            InitializeComponent();

            pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
            pt.Offset(-Width, -Height);
            Left = pt.X - 5;
            Top = pt.Y - 5;

            //Task.Run(() =>
            //{
            //    showWindow(mode);
            //    //closeWindow(mode);
            //});

        }

        //public void showWindow(string mode)
        //{
        //    Dispatcher.Invoke(() =>
        //    {
        //        switch (mode)
        //        {
        //            case "default":
        //                DoubleAnimation animation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
        //                BeginAnimation(OpacityProperty, animation);
        //                break;

        //            case "appear":
        //                DoubleAnimation animation1 = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
        //                BeginAnimation(OpacityProperty, animation1);
        //                break;

        //            case "popup":
        //                for (int i = 0; i < Height + 5; i++)
        //                {
        //                    Dispatcher.Invoke((() =>
        //                    {
        //                        Top = Top - 1;
        //                    }));
        //                }
        //                break;
        //        }
        //    });
        //}

        //public void closeWindow(string mode)
        //{
        //    Thread.Sleep(1000);
        //    Dispatcher.Invoke(() =>
        //    {

        //        DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.8));
        //        BeginAnimation(OpacityProperty, animation);

        //        if (mode == "popup")
        //        {
        //            Top += Height + 45;
        //            BeginAnimation(OpacityProperty, null);
        //        }
        //    });
        //}

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

    }
}

