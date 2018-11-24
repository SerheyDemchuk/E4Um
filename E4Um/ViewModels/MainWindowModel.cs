using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using System.ComponentModel;
//using System.Drawing;
using E4Um.Views;
using E4Um.AppSettings;
using E4Um.Helpers;

namespace E4Um.ViewModels
{
    class MainWindowModel : ViewModelBase
    {
        public RelayCommand OpenPopUpWindowCommand { get; set; }
        public RelayCommand OpenTermFontDialogCommand { get; set; }
        public RelayCommand OpenTranslationFontDialogCommand { get; set; }

        private readonly IConfigProvider configProvider;
        private readonly IWindowService openWindowService;
        //private readonly ISessionContext sessionCon;

        public MainWindowModel(IConfigProvider configProvider, IWindowService openWindowService)
        {
            //this.sessionCon = sessionCon;
            //this.sessionContext.PropertyChanged += SessionContext_PropertyChanged;
            this.configProvider = configProvider;
            this.openWindowService = openWindowService;
            OpenPopUpWindowCommand = new RelayCommand(OpenPopUpWindowCommand_Execute);
            OpenTermFontDialogCommand = new RelayCommand(OpenTermFontDialogCommand_Execute);
            OpenTranslationFontDialogCommand = new RelayCommand(OpenTranslationFontDialogCommand_Execute);
        }

        public void OpenPopUpWindowCommand_Execute(object parameter)
        {
            openWindowService.CreatePopUpWindow(configProvider.PopUpMode, configProvider.DelayMilliSeconds, configProvider.PopUpWidthToContent);
        }
        
        public void OpenTermFontDialogCommand_Execute(object parameter)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowEffects = false;

            string fontFamily = StaticConfigProvider.TermFontType.ToString();
            float fontSize = (float)StaticConfigProvider.TermFontSize;
            System.Drawing.FontStyle fontStyle = StaticConfigProvider.TermFontStyle;

            fontDialog.Font = new System.Drawing.Font(fontFamily, fontSize, fontStyle);

            var result = fontDialog.ShowDialog();
            if( result == DialogResult.OK)
            {
                //configProvider.PopUpFontFamily 
                //configProvider.PopUpFontType 
                System.Drawing.Font f = fontDialog.Font;

                StaticConfigProvider.TermFontType = new FontFamily(fontDialog.Font.Name);
                StaticConfigProvider.TermFontSize = fontDialog.Font.Size;
                StaticConfigProvider.TermFontStyle = fontDialog.Font.Style;
                
                //SessionContext.PopUpFontSize = fontDialog.Font.Size * 96.0 / 72.0;

                //FontFamily ff = new FontFamily(fontDialog.Font.Name);
                //sessionC+on.PropertyChanged += SessionContext_PropertyChanged;
                //sessionCon.WindowFont = FontType;

                //popUpWindowModel.FontType = new FontFamily(fontDialog.Font.Name);

                //configProvider.PopUpFontSizees = fontDialog.Font.Size * 96.0 / 72.0;

                //configProvider.PopUpFontWeight = fontDialog.Font.Bold ? "Bold" : "Normal";
                //configProvider.PopUpFontStyle = fontDialog.Font.Italic ? "Italic" : "Normal";

                //System.Windows.Data.Binding myBinding = new System.Windows.Data.Binding();
                //myBinding.Source = ConfigProvider.Default;
                //myBinding.Path = new PropertyPath("PopUpFontType");
                //myBinding.Mode = BindingMode.TwoWay;
                //BindingOperations.SetBinding(fontDialog.Font, fontDialog.Font.Name, myBinding);
            }
        }

        public void OpenTranslationFontDialogCommand_Execute(object parameter)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowEffects = false;

            string fontFamily = StaticConfigProvider.TranslationFontType.ToString();
            float fontSize = (float)StaticConfigProvider.TranslationFontSize;
            System.Drawing.FontStyle fontStyle = StaticConfigProvider.TranslationFontStyle;

            fontDialog.Font = new System.Drawing.Font(fontFamily, fontSize, fontStyle);

            var result = fontDialog.ShowDialog();
            if (result == DialogResult.OK)
            { 
                System.Drawing.Font f = fontDialog.Font;

                StaticConfigProvider.TranslationFontType = new FontFamily(fontDialog.Font.Name);
                StaticConfigProvider.TranslationFontSize = fontDialog.Font.Size;
                StaticConfigProvider.TranslationFontStyle = fontDialog.Font.Style;

                //SessionContext.PopUpFontSize = fontDialog.Font.Size * 96.0 / 72.0;

                //FontFamily ff = new FontFamily(fontDialog.Font.Name);
                //sessionC+on.PropertyChanged += SessionContext_PropertyChanged;
                //sessionCon.WindowFont = FontType;

                //popUpWindowModel.FontType = new FontFamily(fontDialog.Font.Name);

                //configProvider.PopUpFontSizees = fontDialog.Font.Size * 96.0 / 72.0;

                //configProvider.PopUpFontWeight = fontDialog.Font.Bold ? "Bold" : "Normal";
                //configProvider.PopUpFontStyle = fontDialog.Font.Italic ? "Italic" : "Normal";

                //System.Windows.Data.Binding myBinding = new System.Windows.Data.Binding();
                //myBinding.Source = ConfigProvider.Default;
                //myBinding.Path = new PropertyPath("PopUpFontType");
                //myBinding.Mode = BindingMode.TwoWay;
                //BindingOperations.SetBinding(fontDialog.Font, fontDialog.Font.Name, myBinding);
            }
        }
        //}
        //public void Open(object parameter)
        //{
        //    PopUpWindow popup = new PopUpWindow("appear");
        //    popup.Show();
        //    CloseWindow();
        //}

    }

}
