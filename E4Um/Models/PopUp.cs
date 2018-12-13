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

        static Dictionary<string, double> wordsDictionary;
        public static Dictionary<string, double> WordsDictionary
        {
            get { return wordsDictionary; }
            set
            {
                if(wordsDictionary != value)
                {
                    wordsDictionary = value;
                    StaticNotifyPropertyChanged();
                }
            }
        }

        static Dictionary<string, double> currentWordsDictionary;
        public static Dictionary<string, double> CurrentWordsDictionary
        {
            get { return currentWordsDictionary; }
            set
            {
                if (currentWordsDictionary != value)
                {
                    currentWordsDictionary = value;
                    StaticNotifyPropertyChanged();
                }
            }
        }

        List<string> termList;
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

        List<string> translationList;
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

        List<string> currentTermList;
        public List<string> CurrentTermList
        {
            get { return currentTermList; }
            set
            {
                if (currentTermList != value)
                {
                    currentTermList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        List<string> currentTranslationList;
        public List<string> CurrentTranslationList
        {
            get { return currentTranslationList; }
            set
            {
                if (currentTranslationList != value)
                {
                    currentTranslationList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        List<string> termTranslationList;
        public List<string> TermTranslationList
        {
            get { return termTranslationList; }
            set
            {
                if (termTranslationList != value)
                {
                    termTranslationList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        bool isTestOpenFirstly;
        public bool IsTestOpenFirstly
        {
            get { return isTestOpenFirstly; }
            set
            {
                if (isTestOpenFirstly != value)
                {
                    isTestOpenFirstly = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsTestOn { get; set; }
        public int TermCounter { get; set; }
        public int TranslationCounter { get; set; }
        public int TermListCount { get; set; }
        public int TranslationListCount { get; set; }

        public PopUp()
        {
            GetTermTranslationList(StaticConfigProvider.CurrentCategoryPath);
            IsTestOn = StaticConfigProvider.IsTestOn;
            IsTestOpenFirstly = true;
            TermCounter = 0;
            TranslationCounter = 0;
            TermListCount = TermList.Count;
            TranslationListCount = TranslationList.Count;
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
                case "IsTestOn":
                    if (StaticConfigProvider.IsTestOn == true)
                        IsTestOn = true;
                    else IsTestOn = false;
                    break;
            }
        }

        // Methods for non-test mode
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
        // /Methods for non-test mode

        // Methods for Category Tab
        public void GetTermTranslationList(string path, [CallerMemberName]string caller = "")
        {
            string callerMethodName = caller;
            TermTranslationList = ReadFromFileService.ReturnTermTranslationList(path, callerMethodName);

            if (IsTestOn)
            {
                GetWordsDictionary();
                if (!WordsDictionary.Values.Contains(3) && !WordsDictionary.Values.Contains(1))
                    IsTestOpenFirstly = true;
            }

            GetTermList();
            GetTranslationList();
            NotifyPropertyChanged();
        }

        public void GetDataGridTermTranslationList(string path, [CallerMemberName]string caller = "")
        {
            string callerMethodName = caller;
            TermTranslationList = ReadFromFileService.ReturnTermTranslationList(path, callerMethodName);
            NotifyPropertyChanged();
        }
        // /Methods for Category Tab

        public Dictionary<string, double> GetCurrentWordsDictionary(int blockVolume)
        {
            CurrentWordsDictionary = ReadFromFileService.ReturnCurrentWordsDictionary(blockVolume);
            return CurrentWordsDictionary;
        }

        public List<string> GetCurrentTermList(int blockVolume)
        {
            CurrentTermList = new List<string>();
            for (int i = 0; i < blockVolume; i++)
                CurrentTermList.Add(TermList[i]);
            //if (TermListCount - TermCounter < blockVolume)
            //{
            //    blockVolume = TermListCount;
            //    for (int i = 0; i < blockVolume; i++)
            //    {
            //        CurrentTermList.Add(TermList[i]);
            //    }
            //}
            //else
            //{
            //    for (int i = TermCounter; i < blockVolume + TermCounter; i++)
            //    {
            //        CurrentTermList.Add(TermList[i]);
            //        TermCounter++;
            //    }
            //}
            
            return CurrentTermList;
        }
        public List<string> GetCurrentTranslationList(int blockVolume)
        {
            CurrentTranslationList = new List<string>();

            for (int i = 0; i < blockVolume; i++)
                CurrentTranslationList.Add(TranslationList[i]);
            //if (TranslationListCount - TranslationCounter < blockVolume)
            //{
            //    blockVolume = TranslationListCount;
            //    for (int i = 0; i < blockVolume; i++)
            //    {
            //        CurrentTranslationList.Add(TranslationList[i]);
            //    }
            //}
            //else
            //{
            //    for (int i = TranslationCounter; i < blockVolume + TranslationCounter; i++)
            //    {
            //        CurrentTermList.Add(TranslationList[i]);
            //        TranslationCounter++;
            //    }
            //}

            return CurrentTranslationList;
        }

        public void ReSortWordsDictionary()
        {
            WordsDictionary = (from entry in WordsDictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            ReadFromFileService.SortTermTranslationList(WordsDictionary);
            //GetTermList();
            //GetTranslationList();
            StaticNotifyPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Static NPC
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
