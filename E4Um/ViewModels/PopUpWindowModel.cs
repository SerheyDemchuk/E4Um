using System;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        ////////////////
        FontFamily fontType;
        public FontFamily FontType
        {
            get
            {
                return fontType;
            }
            set
            {
                fontType = value;
                NotifyPropertyChanged();
            }
        }
        ////////////////
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
        //private readonly ISessionContext sessionContext;

        DispatcherTimer openWindowTimer;

        public PopUpWindowModel(IWindowService windowService, IConfigProvider configProvider)
        {
            //this.sessionContext = sessionContext;
            //SessionContext sContext = new SessionContext();
            //this.sessionContext.PropertyChanged += SessionContext_PropertyChanged;
            SessionContext.PropertyChanged += SessionContext_PropertyChanged;
            //this.sessionContext.WindowFont = new FontFamily("Impact");
            //this.sessionContext.PropertyChanged += SessionContext_PropertyChanged;
            //FontType = new FontFamily("Impact");
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

        private void SessionContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PopUpFontType")
            {   
                FontType = SessionContext.PopUpFontType;
            }
        }

        private void OpenWindow_Timer_Tick(object sender, EventArgs e)
        {
            //FontType = FontsList[CurrentRecord];
            switch (configProvider.PopUpMode)
            {
                case ("default"):
                    ChangeWindowContent();
                    break;

                case ("appear"):
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
                    break;

                case ("popup"):
                    Task.Run(() =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Open();
                        });
                    });
                    Task.Run(() =>
                    {
                        Thread.Sleep(configProvider.DelayMilliSeconds);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Close();
                            ChangeWindowContent();
                        });
                    });
                    break;
            }
        }

        public void Open()
        {
            windowService.ShowPopUpWindow(configProvider.PopUpMode);
        }

        public void Close()
        {
            windowService.HidePopUpWindow(configProvider.PopUpMode);   
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
    //public interface ISessionContext
    //{
    //    FontFamily WindowFont { get; set; }
    //}

    public static class SessionContext
    {
        static FontFamily popUpFontType;
        static double popUpFontSize;
        public static FontFamily PopUpFontType
        {
            get
            {
                return popUpFontType;
            }
            set
            {
                popUpFontType = value;
                NotifyPropertyChanged();
            }
        }
        public static double PopUpFontSize
        {
            get
            {
                return popUpFontSize;
            }
            set
            {
                popUpFontSize = value;
                NotifyPropertyChanged();
            }
        }

        public static event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private static void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(null, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
