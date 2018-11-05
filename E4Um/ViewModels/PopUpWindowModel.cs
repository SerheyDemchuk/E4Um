using System;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using E4Um.Models;
using E4Um.Views;
using E4Um.Helpers;
using E4Um.AppSettings;

namespace E4Um.ViewModels
{
    class PopUpWindowModel : ViewModelBase
    {
        Dictionary<string, double> words;
        string windowContent;

        public Dictionary<string, double> Words
        {
            get
            {
                words = new Dictionary<string, double>(PopUp.GetWords());
                return words;
            }
        }

        public string WindowContent
        {
            get { return windowContent; }
            set
            {
                windowContent = value;
                NotifyPropertyChanged("WindowContent");
            }
        }

        public List<string> WordPairs { get; set; }

        public int CurrentRecord { get; set; }

        private readonly IWindowService windowService;
        private readonly IConfigProvider configProvider;

        DispatcherTimer openWindowTimer;

        public PopUpWindowModel(IWindowService windowService, IConfigProvider configProvider)
        {
            WordPairs = new List<string>();
            foreach (KeyValuePair<string, double> kvp in Words)
            {
                WordPairs.Add(kvp.Key);
            }

            this.windowService = windowService;
            this.configProvider = configProvider;
            CurrentRecord = 0;
            ChangeWindowContent();
            openWindowTimer = new DispatcherTimer();
            openWindowTimer.Interval = TimeSpan.FromSeconds(this.configProvider.SecondsToOpen);
            openWindowTimer.Tick += OpenWindow_Timer_Tick;
            openWindowTimer.Start();
        }

        private void OpenWindow_Timer_Tick(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ChangeWindowContent();
                    Open();
                });
            });
            Task.Run(() =>
            {
                Thread.Sleep(configProvider.DelayMilliSeconds);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Close();
                });
            });

        }

        public void Open()
        {
            windowService.ShowPopUpWindow("appear");
        }

        public void Close()
        {
            windowService.HidePopUpWindow("appear");   
        }

        public void ChangeWindowContent()
        {
            
           if (CurrentRecord != WordPairs.Count)
           {
                WindowContent = WordPairs[CurrentRecord];
                CurrentRecord++;
           }
           else
            {
                CurrentRecord = 0;
                WindowContent = WordPairs[CurrentRecord];
                CurrentRecord++;
            }

        }
    }
}
