using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using E4Um.AppSettings;
using E4Um.Helpers;

namespace E4Um.Models
{
    class PopUp: INotifyPropertyChanged
    {
        Dictionary<string, double> wordsDictionary;
        List<string> termList;
        List<string> translationList;

        public Dictionary<string, double> WordsDictionary
        {
            get { return wordsDictionary; }
            set
            {
                if(wordsDictionary != value)
                {
                    wordsDictionary = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public List <string> TermList
        {
            get { return termList; }
            set
            {
                if (termList != value)
                {
                    termList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public List<string> TranslationList
        {
            get { return translationList; }
            set
            {
                if (translationList != value)
                {
                    translationList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PopUp()
        {
            GetWordsDictionary();
            GetTermList();
            GetTranslationList();
            StaticConfigProvider.StaticPropertyChanged += StaticConfigProvider_PropertyChanged;
        }
        private void StaticConfigProvider_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsTermUpper":
                    if (StaticConfigProvider.IsTermUpper == true)
                        TermList = TermList.Select(x => x.ToUpper()).ToList();
                    else
                        TermList = TermList.Select(x => x.ToLower()).ToList();
                    break;
                case "IsTranslationUpper":
                    if (StaticConfigProvider.IsTranslationUpper == true)
                        TranslationList = TranslationList.Select(x => x.ToUpper()).ToList();
                    else
                        TranslationList = TranslationList.Select(x => x.ToLower()).ToList();
                    break;
            }
        }

        public Dictionary<string, double> GetWordsDictionary()
        {
            WordsDictionary = ReadFromFileService.ReturnWordsDictionary();
            return WordsDictionary;
        }
        public List<string> GetTermList()
        {
            TermList = ReadFromFileService.ReturnTermList();
            return TermList;
        }
        public List<string> GetTranslationList()
        {
            TranslationList = ReadFromFileService.ReturnTranslationList();
            return TranslationList;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
