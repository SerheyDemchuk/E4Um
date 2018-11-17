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
        string windowContentTerm;
        string windowContentTranslation;
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

        public string WindowContentTerm
        {
            get { return windowContentTerm; }
            set
            {
                windowContentTerm = value;
                NotifyPropertyChanged();
            }
        }

        public string WindowContentTranslation
        {
            get { return windowContentTranslation; }
            set
            {
                windowContentTranslation = value;
                NotifyPropertyChanged();
            }
        }
        
        public List<string> TermTranslation { get; set; }
        public List<string> Term { get; set; }
        public List<string> Translation { get; set; }

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
            TermTranslation = new List<string>();
            Term = new List<string>();
            Translation = new List<string>();

            foreach (KeyValuePair<string, double> kvp in Words)
            {
                TermTranslation.Add(kvp.Key);
            }

            this.windowService = windowService;
            this.configProvider = configProvider;
            CurrentRecord = 0;
            StringSlicer();
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

        public void StringSlicer()
        {
            foreach(string str in TermTranslation)
            {
                int index = str.IndexOf(" - ");
                if(index != -1)
                {
                    int translationLength = str.Length - 2 - index;
                    Term.Add(str.Substring(0, index + 2));
                    Translation.Add(str.Substring(index + 2, translationLength));
                }
                else
                {
                    int secondIndex = str.IndexOf("-");
                    int translationLength = str.Length - 1 - secondIndex;
                    Term.Add(str.Substring(0, secondIndex + 1));
                    Translation.Add(str.Substring(secondIndex + 1, translationLength));
                }
                
            }
        }

        public void ChangeWindowContent()
        {
            
           if (CurrentRecord != TermTranslation.Count)
           {
                WindowContentTerm = Term[CurrentRecord];
                WindowContentTranslation = Translation[CurrentRecord];
                CurrentRecord++;
           }
           else
            {
                CurrentRecord = 0;
                WindowContentTerm = Term[CurrentRecord];
                WindowContentTranslation = Translation[CurrentRecord];
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
        static System.Drawing.FontStyle popUpFontStyle;
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
        public static System.Drawing.FontStyle PopUpFontStyle
        {
            get
            {
                return popUpFontStyle;
            }
            set
            {
                popUpFontStyle = value;
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
