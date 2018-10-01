using System;
using System.Windows.Threading;
using E4Um.Views;
using E4Um.Helpers;

namespace E4Um.ViewModels
{
    class PopUpWindowModel : ViewModelBase
    {
        DispatcherTimer timer;

        public PopUpWindowModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            PopUpWindow popup = new PopUpWindow("appear");
            popup.Show();
        }
    }
}
