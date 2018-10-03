using System;
using System.Windows.Threading;
using E4Um.Views;
using E4Um.Helpers;
using System.Windows;

namespace E4Um.ViewModels
{
    class PopUpWindowModel : ViewModelBase
    {
        //PopUpWindow popUpWindow;
        DispatcherTimer timer;
        //public RelayCommand OpenWindowCommand { get; set; }

        public PopUpWindowModel()
        {
            //OpenWindowCommand = new RelayCommand(Open);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Open(new OpenWindowService());
        }

        public void Open(IWindowService openWindowService)
        {
            openWindowService.CreatePopUpWindow();
        }
    }
}
