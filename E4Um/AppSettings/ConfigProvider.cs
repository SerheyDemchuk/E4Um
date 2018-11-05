using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E4Um.AppSettings
{
    public interface IConfigProvider
    {
        bool IsTestEnabled { get; set; }
        double PopUpWidth { get; set; }
        double PopUpHeight { get; set; }
        double SecondsToOpen { get; set; }
        int DelayMilliSeconds { get; set; }
        void SaveSettings();   
    }
    class ConfigProvider : ApplicationSettingsBase, IConfigProvider
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
