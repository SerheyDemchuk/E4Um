using System;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using E4Um.Models;
using E4Um.Views;
using E4Um.Helpers;
using System.Windows;

namespace E4Um.ViewModels
{
    class PopUpWindowModel : ViewModelBase
    {
        Dictionary<string, double> words;

        public Dictionary<string, double> Words
        {
            get
            {
                words = new Dictionary<string, double>(PopUp.GetWords());
                return words;
            }
        }
        public int SecondsToOpen { get; set; }

        private readonly IWindowService windowService;
        
        DispatcherTimer openTimer;


        public PopUpWindowModel(IWindowService windowService)
        {
            this.windowService = windowService;
            SecondsToOpen = 5;
            openTimer = new DispatcherTimer();
            openTimer.Interval = TimeSpan.FromSeconds(SecondsToOpen);
            openTimer.Tick += Open_Timer_Tick;
            openTimer.Tick += Close_Timer_Tick;
            openTimer.Start();
        }

        private void Open_Timer_Tick(object sender, EventArgs e)
        {
            Open();
        }

        private void Close_Timer_Tick(object sender, EventArgs e)
        {
            Close();
        }

        public void Open()
        {
            windowService.ShowPopUpWindow("appear");
        }

        public void Close()
        {
            windowService.HidePopUpWindow("appear");
        }
    }
}
