using System;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace E4Um.AppSettings
{
    public class StaticConfigProvider
    {
        #region IntervalsFields
        static int secondsToOpen;
        static double delayMilliSeconds;
        #endregion

        #region FontStyleFields
        static FontFamily termFontType;
        static FontFamily translationFontType;
        static double termFontSize;
        static double translationFontSize;
        static System.Drawing.FontStyle termFontStyle;
        static System.Drawing.FontStyle translationFontStyle;
        static bool isTermUpper;
        static bool isTranslationUpper;
        #endregion

        #region CurrentCategoryPathField
        static string currentCategoryPath;
        #endregion

        #region IntervalsProperties
        public static int SecondsToOpen
        {
            get { return secondsToOpen; }
            set
            {
                secondsToOpen = value;
                StaticNotifyPropertyChanged();
            }
        }
        public static double DelayMilliSeconds
        {
            get { return delayMilliSeconds; }
            set
            {
                delayMilliSeconds = value;
                StaticNotifyPropertyChanged();
            }
        }
        #endregion

        #region FontStyleProperties
        public static FontFamily TermFontType
        {
            get { return termFontType; }
            set
            {
                termFontType = value;
                StaticNotifyPropertyChanged();
            }
        }

        public static FontFamily TranslationFontType
        {
            get { return translationFontType; }
            set
            {
                translationFontType = value;
                StaticNotifyPropertyChanged();
            }
        }

        public static double TermFontSize
        {
            get { return termFontSize; }
            set
            {
                termFontSize = value;
                StaticNotifyPropertyChanged();
            }
        }

        public static double TranslationFontSize
        {
            get { return translationFontSize; }
            set
            {
                translationFontSize = value;
                StaticNotifyPropertyChanged();
            }
        }

        public static System.Drawing.FontStyle TermFontStyle
        {
            get { return termFontStyle; }
            set
            {
                termFontStyle = value;
                StaticNotifyPropertyChanged();
            }
        }

        public static System.Drawing.FontStyle TranslationFontStyle
        {
            get { return translationFontStyle; }
            set
            {
                translationFontStyle = value;
                StaticNotifyPropertyChanged();
            }
        }

        public static bool IsTermUpper
        {
            get { return isTermUpper; }
            set
            {
                isTermUpper = value;
                StaticNotifyPropertyChanged();
            }
        }

        public static bool IsTranslationUpper
        {
            get { return isTranslationUpper; }
            set
            {
                isTranslationUpper = value;
                StaticNotifyPropertyChanged();
            }
        }
        #endregion

        #region CurrentCategoryPathProperty
        public static string CurrentCategoryPath
        {
            get { return currentCategoryPath; }
            set
            {
                if(currentCategoryPath != value)
                {
                    currentCategoryPath = value;
                    StaticNotifyPropertyChanged();
                }
                
            }
        }
        #endregion

        static StaticConfigProvider()
        {
            TermFontType = new FontFamily(Properties.Settings.Default.termFontType);
            TranslationFontType = new FontFamily(Properties.Settings.Default.translationFontType);
            TermFontSize = Properties.Settings.Default.termFontSize;
            TranslationFontSize = Properties.Settings.Default.translationFontSize;
            TermFontStyle = Properties.Settings.Default.termFontStyle;
            TranslationFontStyle = Properties.Settings.Default.translationFontStyle;
            IsTermUpper = Properties.Settings.Default.isTermUpper;
            IsTranslationUpper = Properties.Settings.Default.isTranslationUpper;
            SecondsToOpen = Properties.Settings.Default.secondsToOpen;
            DelayMilliSeconds = Properties.Settings.Default.delayMilliSeconds;
            CurrentCategoryPath = Properties.Settings.Default.currentCategoryPath;
        }

        public static void SaveSettings()
        {
            Properties.Settings.Default.termFontType = TermFontType.ToString();
            Properties.Settings.Default.translationFontType = TranslationFontType.ToString();
            Properties.Settings.Default.termFontSize = TermFontSize;
            Properties.Settings.Default.translationFontSize = TranslationFontSize;
            Properties.Settings.Default.termFontStyle = TermFontStyle;
            Properties.Settings.Default.translationFontStyle = TranslationFontStyle;
            Properties.Settings.Default.isTermUpper = IsTermUpper;
            Properties.Settings.Default.isTranslationUpper = IsTranslationUpper;
            Properties.Settings.Default.secondsToOpen = SecondsToOpen;
            Properties.Settings.Default.delayMilliSeconds = DelayMilliSeconds;
            Properties.Settings.Default.currentCategoryPath = CurrentCategoryPath;

            Properties.Settings.Default.Save();
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private static void StaticNotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (StaticPropertyChanged != null)
            {
                StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
