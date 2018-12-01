using System;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using System.Linq;
using E4Um.Models;
using E4Um.Helpers;
using E4Um.AppSettings;

namespace E4Um.ViewModels
{
    class PopUpWindowModel : ViewModelBase
    {
        #region WordsDictionary and Term/Translation fields
        Dictionary<string, double> words;
        string windowContentTerm;
        string windowContentTranslation;
        #endregion

        #region WordsDictionary and Term/Translation properties
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
        #endregion

        #region Term/Translation Lists
        public List<string> TermTranslation { get; set; }
        public List<string> Term { get; set; }
        public List<string> Translation { get; set; }
        #endregion

        #region Service/Config readonly fields
        readonly IWindowService windowService;
        readonly IConfigProvider configProvider;
        #endregion

        #region TimerProperties
        DispatcherTimer openWindowTimer;
        int SecondsToOpen { get; set; }
        int DelayMilliSeconds { get; set; }
        #endregion

        #region FontSettingProperties
        FontFamily termFontType;
        FontFamily translationFontType;
        double termFontSize;
        double translationFontSize;
        System.Drawing.FontStyle termFontStyle;
        System.Drawing.FontStyle translationFontStyle;
        public FontFamily TermFontType
        {
            get
            {
                return termFontType;
            }
            set
            {
                termFontType = value;
                NotifyPropertyChanged();
            }
        }
        public FontFamily TranslationFontType
        {
            get
            {
                return translationFontType;
            }
            set
            {
                translationFontType = value;
                NotifyPropertyChanged();
            }
        }
        public double TermFontSize
        {
            get
            {
                return termFontSize;
            }
            set
            {
                termFontSize = value;
                NotifyPropertyChanged();
            }
        }
        public double TranslationFontSize
        {
            get
            {
                return translationFontSize;
            }
            set
            {
                translationFontSize = value;
                NotifyPropertyChanged();
            }
        }
        public System.Drawing.FontStyle TermFontStyle
        {
            get
            {
                return termFontStyle;
            }
            set
            {
                termFontStyle = value;
                NotifyPropertyChanged();
            }
        }
        public System.Drawing.FontStyle TranslationFontStyle
        {
            get
            {
                return translationFontStyle;
            }
            set
            {
                translationFontStyle = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        string PopUpMode { get; set; }
        int CurrentRecord { get; set; }
        bool DefaultModeOffset { get; set; }

        public PopUpWindowModel(IWindowService windowService, IConfigProvider configProvider)
        {
            //this.sessionContext = sessionContext;
            //SessionContext sContext = new SessionContext();
            //this.sessionContext.PropertyChanged += SessionContext_PropertyChanged;
            //StaticConfigProvider.PropertyChanged += SessionContext_PropertyChanged;
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

            PopUpMode = this.configProvider.PopUpMode;
            CurrentRecord = 0;
            DefaultModeOffset = false;
            StringSlicer(StaticConfigProvider.IsTermUpper, StaticConfigProvider.IsTranslationUpper);
            ChangeWindowContent();

            TermFontType = StaticConfigProvider.TermFontType;
            TranslationFontType = StaticConfigProvider.TranslationFontType;
            TermFontSize = StaticConfigProvider.TermFontSize;
            TranslationFontSize = StaticConfigProvider.TranslationFontSize;
            TermFontStyle = StaticConfigProvider.TermFontStyle;
            TranslationFontStyle = StaticConfigProvider.TranslationFontStyle;

            StaticConfigProvider.StaticPropertyChanged += StaticConfigProvider_PropertyChanged;
            ConfigProvider.StaticPropertyChanged += ConfigProvider_PropertyChanged;

            SecondsToOpen = StaticConfigProvider.SecondsToOpen + (int)StaticConfigProvider.DelayMilliSeconds;
            DelayMilliSeconds = (int)StaticConfigProvider.DelayMilliSeconds * 1000;
            openWindowTimer = new DispatcherTimer();

            if (PopUpMode != "default")
                openWindowTimer.Interval = TimeSpan.FromSeconds(SecondsToOpen);
            else
                openWindowTimer.Interval = TimeSpan.FromSeconds(DelayMilliSeconds / 1000);

            openWindowTimer.Tick += OpenWindow_Timer_Tick;
            openWindowTimer.Start();

        }

        private void ConfigProvider_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PopUpMode = ConfigProvider.StaticPopUpMode;
            if(PopUpMode != "default")
                openWindowTimer.Interval = TimeSpan.FromSeconds(StaticConfigProvider.SecondsToOpen + (int)StaticConfigProvider.DelayMilliSeconds);
            else
                openWindowTimer.Interval = TimeSpan.FromSeconds(DelayMilliSeconds / 1000);

        }

        private void StaticConfigProvider_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SecondsToOpen":
                    SecondsToOpen = StaticConfigProvider.SecondsToOpen;
                    openWindowTimer.Interval = TimeSpan.FromSeconds(SecondsToOpen + (int)StaticConfigProvider.DelayMilliSeconds);
                    break;
                case "DelayMilliSeconds":
                    if (PopUpMode != "default")
                    {
                        DelayMilliSeconds = (int)StaticConfigProvider.DelayMilliSeconds * 1000;
                        openWindowTimer.Interval = TimeSpan.FromSeconds(StaticConfigProvider.SecondsToOpen + (int)StaticConfigProvider.DelayMilliSeconds);
                    }
                    else
                    {
                        DelayMilliSeconds = (int)StaticConfigProvider.DelayMilliSeconds * 1000;
                        openWindowTimer.Interval = TimeSpan.FromSeconds(StaticConfigProvider.DelayMilliSeconds);
                    }
                    break;
                case "TermFontType":
                    TermFontType = StaticConfigProvider.TermFontType;
                    break;
                case "TranslationFontType":
                    TranslationFontType = StaticConfigProvider.TranslationFontType;
                    break;
                case "TermFontSize":
                    TermFontSize = StaticConfigProvider.TermFontSize;
                    break;
                case "TranslationFontSize":
                    TranslationFontSize = StaticConfigProvider.TranslationFontSize;
                    break;
                case "TermFontStyle":
                    TermFontStyle = StaticConfigProvider.TermFontStyle;
                    break;
                case "TranslationFontStyle":
                    TranslationFontStyle = StaticConfigProvider.TranslationFontStyle;
                    break;
                case "IsTermUpper":
                    if (StaticConfigProvider.IsTermUpper == true)
                        Term = Term.Select(x => x.ToUpper()).ToList();
                    else
                        Term = Term.Select(x => x.ToLower()).ToList();
                    break;
                case "IsTranslationUpper":
                    if (StaticConfigProvider.IsTranslationUpper == true)
                        Translation = Translation.Select(x => x.ToUpper()).ToList();
                    else
                        Translation = Translation.Select(x => x.ToLower()).ToList();
                    break;
            }
        }

        private void OpenWindow_Timer_Tick(object sender, EventArgs e)
        {
            //FontType = FontsList[CurrentRecord];
            switch (PopUpMode)
            {
                case ("default"):
                    if (DefaultModeOffset)
                    {
                        Open();
                        ChangeWindowContent();
                        DefaultModeOffset = false;
                    }
                    else
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
                        Thread.Sleep(DelayMilliSeconds);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Close();
                        });
                    });
                    DefaultModeOffset = true;
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
                        Thread.Sleep(DelayMilliSeconds);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Close();
                            ChangeWindowContent();
                        });
                    });
                    DefaultModeOffset = true;
                    break;
            }
        }

        public void Open()
        {
            windowService.ShowPopUpWindow(PopUpMode);
        }

        public void Close()
        {
            windowService.HidePopUpWindow(PopUpMode);   
        }

        public void StringSlicer(bool isTermUpper, bool isTranslationUpper)
        {
            foreach(string str in TermTranslation)
            {
                int index = str.IndexOf(" - ");
                if(index != -1)
                {
                    int translationLength = str.Length - 2 - index;
                    if (isTermUpper)
                    {
                        string temp = str.Substring(0, index + 2);
                        Term.Add(temp.ToUpper());
                    }
                    else
                    {
                        string temp = str.Substring(0, index + 2);
                        Term.Add(temp.ToLower());
                    }
                    if (isTranslationUpper)
                    {
                        string temp = str.Substring(index + 2, translationLength);
                        Translation.Add(temp.ToUpper());
                    }
                    else
                    {
                        string temp = str.Substring(index + 2, translationLength);
                        Translation.Add(temp.ToLower());
                    }
                }
                else
                {
                    int secondIndex = str.IndexOf("-");
                    int translationLength = str.Length - 1 - secondIndex;
                    if (isTermUpper)
                    {
                        string temp = str.Substring(0, secondIndex + 1);
                        Term.Add(temp.ToUpper());
                    }
                    else
                    {
                        string temp = str.Substring(0, secondIndex + 1);
                        Term.Add(temp.ToLower());
                    }
                    if (isTranslationUpper)
                    {
                        string temp = str.Substring(secondIndex + 1, translationLength);
                        Translation.Add(temp.ToUpper());
                    }
                    else
                    {
                        string temp = str.Substring(secondIndex + 1, translationLength);
                        Translation.Add(temp.ToLower());
                    }
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

    //public static class SessionContext
    //{
    //    static FontFamily popUpFontType;
    //    static double popUpFontSize;
    //    static System.Drawing.FontStyle popUpFontStyle;
    //    public static FontFamily PopUpFontType
    //    {
    //        get
    //        {
    //            return popUpFontType;
    //        }
    //        set
    //        {
    //            popUpFontType = value;
    //            NotifyPropertyChanged();
    //        }
    //    }
    //    public static double PopUpFontSize
    //    {
    //        get
    //        {
    //            return popUpFontSize;
    //        }
    //        set
    //        {
    //            popUpFontSize = value;
    //            NotifyPropertyChanged();
    //        }
    //    }
    //    public static System.Drawing.FontStyle PopUpFontStyle
    //    {
    //        get
    //        {
    //            return popUpFontStyle;
    //        }
    //        set
    //        {
    //            popUpFontStyle = value;
    //            NotifyPropertyChanged();
    //        }
    //    }

    //    public static event PropertyChangedEventHandler PropertyChanged;

    //    // This method is called by the Set accessor of each property.
    //    // The CallerMemberName attribute that is applied to the optional propertyName
    //    // parameter causes the property name of the caller to be substituted as an argument.
    //    private static void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(null, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }
    //}
}
