using System;
using System.Configuration;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E4Um.ViewModels;

namespace E4Um.AppSettings
{
    public interface IConfigProvider
    {
        bool IsTestEnabled { get; set; }
        string PopUpMode { get; set; }
        double PopUpWidth { get; set; }
        double PopUpHeight { get; set; }
        Color PopUpBackground { get; set; }
        FontFamily PopUpFontType { get; set; }
        string PopUpWidthToContent { get; set; }
        double SecondsToOpen { get; set; }
        int DelayMilliSeconds { get; set; }
        void SaveSettings();
    }
    public class ConfigProvider : ApplicationSettingsBase, IConfigProvider
    {
        private static ConfigProvider defaultInstance = ((ConfigProvider)(Synchronized(new ConfigProvider())));
        public static ConfigProvider Default
        {
            get { return defaultInstance; }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool IsTestEnabled
        {
            get { return (bool)this["IsTestEnabled"]; }
            set { this["IsTestEnabled"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("appear")]
        public string PopUpMode
        {
            get { return (string)this["PopUpMode"]; }
            set
            {
                this["PopUpMode"] = value;
                if (value == "default")
                    PopUpWidthToContent = "Manual";
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("300")]
        public double PopUpWidth
        {
            get { return (double)this["PopUpWidth"]; }
            set { this["PopUpWidth"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("80")]
        public double PopUpHeight
        {
            get { return (double)this["PopUpHeight"]; }
            set { this["PopUpHeight"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("#FFFFF2A3")]
        public Color PopUpBackground
        {
            get { return (Color)this["PopUpBackground"]; }
            set { this["PopUpBackground"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Segoe UI")]
        public FontFamily PopUpFontType
        {
            get { return (FontFamily)this["PopUpFontType"]; }
            set { this["PopUpFontType"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Width")]
        public string PopUpWidthToContent
        {
            get { return (string)this["PopUpWidthToContent"]; }
            set { this["PopUpWidthToContent"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("2")]
        public double SecondsToOpen
        {
            get { return (double)this["SecondsToOpen"]; }
            set { this["SecondsToOpen"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("1000")]
        public int DelayMilliSeconds
        {
            get { return (int)this["DelayMilliSeconds"]; }
            set { this["DelayMilliSeconds"] = value; }
        }

        public void SaveSettings()
        {
            Default.Save();
        }

    }
}
