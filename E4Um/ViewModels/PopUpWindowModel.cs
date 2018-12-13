using System;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media;
using System.ComponentModel;
using E4Um.Models;
using E4Um.Helpers;
using E4Um.AppSettings;

namespace E4Um.ViewModels
{
    class PopUpWindowModel : ViewModelBase
    {

        #region WordsDictionary property
        Dictionary<string, double> wordsDictionary;
        public Dictionary<string, double> WordsDictionary
        {
            get { return wordsDictionary; }
            set
            {
                wordsDictionary = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Term/Translation properties
        string windowContentTerm;
        string windowContentTranslation;
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

        public PopUp Model { get; }
        string PopUpMode { get; set; }
        int CurrentRecord { get; set; }
        bool DefaultModeOffset { get; set; }

        // Test-related data
        List<double> PriorityList { get; set; }
        int HighPriorityCounter { get; set; }
        int MiddlePriorityCounter { get; set; }
        int LowPriorityCounter { get; set; }
        // /Test-related data

        public PopUpWindowModel(PopUp model, IWindowService windowService, IConfigProvider configProvider)
        {

            Model = model;
            WordsDictionary = Model.GetWordsDictionary();
            this.windowService = windowService;
            this.configProvider = configProvider;

            PopUpMode = this.configProvider.PopUpMode;
            CurrentRecord = 0;
            DefaultModeOffset = false;
            PriorityList = new List<double>();
            HighPriorityCounter = 0;
            MiddlePriorityCounter = 0;
            LowPriorityCounter = 0;

            ChangeWindowContent();

            TermFontType = StaticConfigProvider.TermFontType;
            TranslationFontType = StaticConfigProvider.TranslationFontType;
            TermFontSize = StaticConfigProvider.TermFontSize;
            TranslationFontSize = StaticConfigProvider.TranslationFontSize;
            TermFontStyle = StaticConfigProvider.TermFontStyle;
            TranslationFontStyle = StaticConfigProvider.TranslationFontStyle;

            PopUp.StaticPropertyChanged += Model_PropertyChanged;
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

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "WordsDictionary")
            {
                WordsDictionary = PopUp.WordsDictionary;
                foreach(KeyValuePair<string, double> record in WordsDictionary)
                {
                    PriorityList.Add(record.Value);
                }
            }
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
                case "IsTestOn":
                    if (StaticConfigProvider.IsTestOn == true)
                        openWindowTimer.Stop();
                    else openWindowTimer.Start();
                    break;

            }
        }

        private void OpenWindow_Timer_Tick(object sender, EventArgs e)
        {
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

        public void ChangeWindowContent()
        {

           if (CurrentRecord < Model.TermList.Count)
           {
                if(PriorityList.Count != 0)
                {
                    if (PriorityList[CurrentRecord] == 5)
                    {
                        WindowContentTerm = Model.TermList[CurrentRecord];
                        WindowContentTranslation = Model.TranslationList[CurrentRecord];
                        CurrentRecord++;
                    }
                    else if (PriorityList[CurrentRecord] == 3 && HighPriorityCounter != 8)
                    {
                        HighPriorityCounter++;
                        CurrentRecord = 0;
                        WindowContentTerm = Model.TermList[CurrentRecord];
                        WindowContentTranslation = Model.TranslationList[CurrentRecord];
                        CurrentRecord++;
                    }
                    else if (PriorityList[CurrentRecord] == 3 && HighPriorityCounter == 8)
                    {
                        WindowContentTerm = Model.TermList[CurrentRecord];
                        WindowContentTranslation = Model.TranslationList[CurrentRecord];
                        CurrentRecord++;
                    }
                }
                else
                {
                    WindowContentTerm = Model.TermList[CurrentRecord];
                    WindowContentTranslation = Model.TranslationList[CurrentRecord];
                    CurrentRecord++;
                }
           }
           else
            {
                CurrentRecord = 0;
                WindowContentTerm = Model.TermList[CurrentRecord];
                WindowContentTranslation = Model.TranslationList[CurrentRecord];
                CurrentRecord++;
            }

        }

    }

}
