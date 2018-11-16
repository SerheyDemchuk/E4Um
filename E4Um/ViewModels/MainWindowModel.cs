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
        //FontFamily fontType;
        
        //public FontFamily FontType
        //{
        //    get
        //    {
        //        return fontType;
        //    }
        //    set
        //    {
        //        fontType = value;
        //        NotifyPropertyChanged("FontType");
        //    }
        //}

        public RelayCommand OpenPopUpWindowCommand { get; set; }
        public RelayCommand OpenFontDialogCommand { get; set; }

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
            OpenFontDialogCommand = new RelayCommand(OpenFontDialogCommand_Execute);
        }

        public void OpenPopUpWindowCommand_Execute(object parameter)
        {
            openWindowService.CreatePopUpWindow(configProvider.PopUpMode, configProvider.DelayMilliSeconds, configProvider.PopUpWidthToContent);
        }

        public void OpenFontDialogCommand_Execute(object parameter)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowColor = true;
            var result = fontDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                //configProvider.PopUpFontFamily 
                //configProvider.PopUpFontType 


                SessionContext.PopUpFontType = new FontFamily(fontDialog.Font.Name);

                //configProvider.PopUpFontType = new FontFamily(fontDialog.Font.Name);
                
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

        //public void Open(object parameter)
        //{
        //    PopUpWindow popup = new PopUpWindow("appear");
        //    popup.Show();
        //    CloseWindow();
        //}

    }

}
