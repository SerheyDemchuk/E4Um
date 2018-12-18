using System;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using E4Um.Models;
using E4Um.Views;
using E4Um.AppSettings;
using E4Um.Helpers;

namespace E4Um.ViewModels
{
    class MainWindowModel : ViewModelBase
    {
        string currentCategory;
        public string CurrentCategory
        {
            get
            {
                return currentCategory;
            }
            set
            {
                if(currentCategory != value)
                {
                    currentCategory = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public List<TreeViewItems> TreeViewItemsList { get; set; }

        FileItem selectedItem;
        public FileItem SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    Model.GetDataGridTermTranslationList("English\\" + selectedItem.Name);
                }
            }
        }

        public PopUp Model { get; }

        public ObservableCollection<DataGridItem> DataGridItemList { get; set; }
        public Dictionary<string, double> DataGridWordsDictionary { get; set; }

        List<string> DataGridTermTranslationList { get; set; }
        public List<string> DataGridTermList { get; set; }
        public List<string> DataGridTranslationList { get; set; }

        private readonly IConfigProvider configProvider;
        private readonly IWindowService openWindowService;

        //public RelayCommand OpenPopUpWindowCommand { get; set; }
        public RelayCommand OpenTermFontDialogCommand { get; set; }
        public RelayCommand OpenTranslationFontDialogCommand { get; set; }
        public RelayCommand SelectedItemChangedCommand { get; set; }
        public RelayCommand OpenTestWindowCommand { get; set; }

        public MainWindowModel(PopUp model, IConfigProvider configProvider, IWindowService openWindowService)
        {
               
            Model = model;
            Model.PropertyChanged += Model_PropertyChanged;
            CurrentCategory = StaticConfigProvider.CurrentCategoryPath.Remove(0, 8);
            DataGridItemList = new ObservableCollection<DataGridItem>();
            DataGridWordsDictionary = new Dictionary<string, double>();

            DataGridTermTranslationList = Model.TermTranslationList;
            DataGridTermList = new List<string>();
            DataGridTranslationList = new List<string>();
            StringSlicer();

            for (int i = 0; i < DataGridTermList.Count; i++)
            {
                if (DataGridTermList[i].Length != 0)
                {
                    DataGridItem dgi = new DataGridItem { Term = DataGridTermList[i].Remove(DataGridTermList[i].Length - 1), Translation = DataGridTranslationList[i] };
                    DataGridItemList.Add(dgi);
                }
            }

            this.configProvider = configProvider;
            this.openWindowService = openWindowService;

            TreeViewItemsList = GetItems("English");
            //OpenPopUpWindowCommand = new RelayCommand(OpenPopUpWindowCommand_Execute);
            OpenTermFontDialogCommand = new RelayCommand(OpenTermFontDialogCommand_Execute);
            OpenTranslationFontDialogCommand = new RelayCommand(OpenTranslationFontDialogCommand_Execute);
            SelectedItemChangedCommand = new RelayCommand(SelectedItemChangedCommand_Execute);
            OpenTestWindowCommand = new RelayCommand(OpenTestWindowCommand_Execute);
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "GetDataGridTermTranslationList":
                    DataGridTermTranslationList = Model.TermTranslationList;
                    DataGridItemList.Clear();
                    DataGridTermList.Clear();
                    DataGridTranslationList.Clear();
                    StringSlicer();
                    for (int i = 0; i < DataGridTermList.Count; i++)
                    {
                        if (DataGridTermList[i].Length != 0)
                        {
                            DataGridItem dgi = new DataGridItem { Term = DataGridTermList[i].Remove(DataGridTermList[i].Length - 1), Translation = DataGridTranslationList[i] };
                            DataGridItemList.Add(dgi);
                        }
                    }
                    NotifyPropertyChanged();
                    break;

                case "GetTermTranslationList":
                    DataGridTermTranslationList = Model.TermTranslationList;
                    DataGridItemList.Clear();
                    DataGridTermList.Clear();
                    DataGridTranslationList.Clear();
                    StringSlicer();
                    for (int i = 0; i < DataGridTermList.Count; i++)
                    {
                        if (DataGridTermList[i].Length != 0)
                        {
                            DataGridItem dgi = new DataGridItem { Term = DataGridTermList[i].Remove(DataGridTermList[i].Length - 1), Translation = DataGridTranslationList[i] };
                            DataGridItemList.Add(dgi);
                        }
                    }
                    NotifyPropertyChanged();
                    break;
                case "IsTestOn":
                    if (Model.IsTestOn)
                    {
                        if (StaticConfigProvider.IsTestOpenFirstly)
                            openWindowService.CreateTestWindow();
                    }
                    break;

            }            
        }

        //public void OpenPopUpWindowCommand_Execute(object parameter)
        //{
        //    openWindowService.CreatePopUpWindow(configProvider.PopUpMode, (int)StaticConfigProvider.DelayMilliSeconds * 1000, configProvider.PopUpWidthToContent);
        //}
        
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
                System.Drawing.Font f = fontDialog.Font;

                StaticConfigProvider.TermFontType = new FontFamily(fontDialog.Font.Name);
                StaticConfigProvider.TermFontSize = fontDialog.Font.Size;
                StaticConfigProvider.TermFontStyle = fontDialog.Font.Style;
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

            }
        }

        public void SelectedItemChangedCommand_Execute(object parameter)
        {
            FileItem doubleClickedItem = (FileItem)parameter;
            if(doubleClickedItem != null)
            {
                Model.GetTermTranslationList("English\\" + doubleClickedItem.Name);
                StaticConfigProvider.CurrentCategoryPath = "English\\" + doubleClickedItem.Name;
                CurrentCategory = doubleClickedItem.Name;
            }
            
        }

        public void OpenTestWindowCommand_Execute(object paremeter)
        {
            openWindowService.CreateTestWindow();
        }

        public List<TreeViewItems> GetItems(string path)
        {
            var items = new List<TreeViewItems>();

            
            var dirInfo = new DirectoryInfo(path);

            foreach (var directory in dirInfo.GetDirectories())
            {
                var item = new DirectoryItem
                {
                    Name = directory.Name,
                    Items = GetItems(directory.FullName)
                };

                items.Add(item);
            }

            foreach (var file in dirInfo.GetFiles())
            {
                var item = new FileItem
                {
                    Name = file.Name,
                };

                items.Add(item);
            }

            return items;
        }

        public void StringSlicer()
        {

            foreach (string str in DataGridTermTranslationList)
            {
                int index = str.IndexOf(" - ");
                if (index != -1)
                {
                    int translationLength = str.Length - 2 - index;
                    DataGridTermList.Add(str.Substring(0, index + 2));
                    DataGridTranslationList.Add(str.Substring(index + 2, translationLength));
                }
                else
                {
                    int secondIndex = str.IndexOf("-");
                    int translationLength = str.Length - 1 - secondIndex;
                    DataGridTermList.Add(str.Substring(0, secondIndex + 1));
                    DataGridTranslationList.Add(str.Substring(secondIndex + 1, translationLength));
                }

            }
        }

    }

}
