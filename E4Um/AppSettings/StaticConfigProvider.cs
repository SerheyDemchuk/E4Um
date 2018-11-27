using System;
using System.Configuration;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E4Um.AppSettings
{
    public class StaticConfigProvider
    {
        #region ModeField
        static string popUpMode;
        #endregion

        #region PopUpWidthToContentField
        static string popUpWidthToContent;
        #endregion

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

        #region ModeProperty
        public static string PopUpMode
        {
            get { return popUpMode; }
            set
            {
                popUpMode = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region PopUpWidthToContentProperty
        public static string PopUpWidthToContent
        {
            get
            {
                return popUpWidthToContent;
            }
            set
            {
                popUpWidthToContent = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region IntervalsProperties
        public static int SecondsToOpen
        {
            get { return secondsToOpen; }
            set
            {
                secondsToOpen = value;
                NotifyPropertyChanged();
            }
        }
        public static double DelayMilliSeconds
        {
            get { return delayMilliSeconds; }
            set
            {
                delayMilliSeconds = value;
                NotifyPropertyChanged();
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
                NotifyPropertyChanged();
            }
        }

        public static FontFamily TranslationFontType
        {
            get { return translationFontType; }
            set
            {
                translationFontType = value;
                NotifyPropertyChanged();
            }
        }

        public static double TermFontSize
        {
            get { return termFontSize; }
            set
            {
                termFontSize = value;
                NotifyPropertyChanged();
            }
        }

        public static double TranslationFontSize
        {
            get { return translationFontSize; }
            set
            {
                translationFontSize = value;
                NotifyPropertyChanged();
            }
        }

        public static System.Drawing.FontStyle TermFontStyle
        {
            get { return termFontStyle; }
            set
            {
                termFontStyle = value;
                NotifyPropertyChanged();
            }
        }

        public static System.Drawing.FontStyle TranslationFontStyle
        {
            get { return translationFontStyle; }
            set
            {
                translationFontStyle = value;
                NotifyPropertyChanged();
            }
        }

        public static bool IsTermUpper
        {
            get { return isTermUpper; }
            set
            {
                isTermUpper = value;
                NotifyPropertyChanged();
            }
        }

        public static bool IsTranslationUpper
        {
            get { return isTranslationUpper; }
            set
            {
                isTranslationUpper = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        
        static StaticConfigProvider()
        {
            //PopUpMode = Properties.Settings.Default.popUpMode;
            PopUpMode = "appear";
            PopUpWidthToContent = Properties.Settings.Default.popUpWidthToContent;
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
        }

        public static void SaveSettings()
        {
            Properties.Settings.Default.popUpMode = PopUpMode;
            Properties.Settings.Default.popUpWidthToContent = PopUpWidthToContent;
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

            Properties.Settings.Default.Save();
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
