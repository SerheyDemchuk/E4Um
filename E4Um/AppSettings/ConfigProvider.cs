using System;
using System.Configuration;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace E4Um.AppSettings
{
    public interface IConfigProvider
    {
        bool IsTestOn { get; set; }
        string CurrentCategory { get; set; }
        string PopUpMode { get; set; }
        double PopUpWidth { get; set; }
        double PopUpHeight { get; set; }
        string PopUpWidthToContent { get; set; }
        Color PopUpBackground { get; set; }
        void SaveSettings();
    }
    public class ConfigProvider : ApplicationSettingsBase, IConfigProvider, INotifyPropertyChanged
    {
        #region Configuration Properties and Save Method

        static ConfigProvider defaultInstance = ((ConfigProvider)(Synchronized(new ConfigProvider())));
        public static ConfigProvider Default
        {
            get { return defaultInstance; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool IsTestOn
        {
            get { return (bool)this["IsTestOn"]; }
            set { this["IsTestOn"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Семья. Family")]
        public string CurrentCategory
        {
            get { return (string)this["CurrentCategory"]; }
            set { this["CurrentCategory"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("appear")]
        public string PopUpMode
        {
            get { return (string)this["PopUpMode"]; }
            set
            {
                if(value == "appear")
                {
                    PopUpWidthToContent = "Width";
                    this["PopUpMode"] = value;
                    StaticPopUpMode = (string)this["PopUpMode"];
                }
                else if (value == "popup")
                {
                    PopUpWidthToContent = "Width";
                    this["PopUpMode"] = value;
                    StaticPopUpMode = (string)this["PopUpMode"];
                }
                else if (value == "default")
                {
                    PopUpWidthToContent = "Manual";
                    this["PopUpMode"] = value;
                    StaticPopUpMode = (string)this["PopUpMode"];
                }

            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("300")]
        public double PopUpWidth
        {
            get { return (double)this["PopUpWidth"]; }
            set
            {
                if(value != 400)
                this["PopUpWidth"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("80")]
        public double PopUpHeight
        {
            get { return (double)this["PopUpHeight"]; }
            set { this["PopUpHeight"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Width")]
        public string PopUpWidthToContent
        {
            get { return (string)this["PopUpWidthToContent"]; }
            set { this["PopUpWidthToContent"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("#FFFFF2A3")]
        public Color PopUpBackground
        {
            get { return (Color)this["PopUpBackground"]; }
            set { this["PopUpBackground"] = value; }
        }

        public void SaveSettings()
        {
            Default.Save();
        }

        #endregion

        #region Static Configuration Properties and Static NPC

        static string staticPopUpMode;
        public static string StaticPopUpMode
        {
            get { return staticPopUpMode; }
            set
            {
                staticPopUpMode = value;
                StaticNotifyPropertyChanged();
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        public static void StaticNotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (StaticPropertyChanged != null)
            {
                StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
