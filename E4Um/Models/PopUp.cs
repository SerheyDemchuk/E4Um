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
using Newtonsoft.Json;

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

        static List<string> currentTermList;
        public static List<string> CurrentTermList
        {
            get { return currentTermList; }
            set
            {
                if (currentTermList != value)
                {
                    currentTermList = value;
                    StaticNotifyPropertyChanged();
                }
            }
        }

        static List<string> currentTranslationList;
        public static List<string> CurrentTranslationList
        {
            get { return currentTranslationList; }
            set
            {
                if (currentTranslationList != value)
                {
                    currentTranslationList = value;
                    StaticNotifyPropertyChanged();
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

        public bool isTestOn;
        public bool IsTestOn
        {
            get { return isTestOn; }
            set
            {
                if (isTestOn != value)
                {
                    isTestOn = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int TermCounter { get; set; }
        public int TranslationCounter { get; set; }
        public int TermListCount { get; set; }
        public int TranslationListCount { get; set; }

        readonly IWindowService openWindowService;
        public PopUp(IWindowService openWindowService)
        {
            GetTermTranslationList(StaticConfigProvider.CurrentCategoryPath);
            GetWordsDictionary();
            IsTestOn = StaticConfigProvider.IsTestOn;
            IsTestOpenFirstly = StaticConfigProvider.IsTestOpenFirstly;
            //IsTestOpenFirstly = true;
            TermCounter = 0;
            TranslationCounter = 0;
            TermListCount = TermList.Count;
            TranslationListCount = TranslationList.Count;

            if (IsTestOn && !IsTestOpenFirstly)
            {
                GetCurrentWordsDictionary(10);
                FillCurrentTermTranslationLists(CurrentWordsDictionary);
            }

            else if(IsTestOn && IsTestOpenFirstly)
            {
                CurrentWordsDictionary = new Dictionary<string, double>();
                //GetCurrentWordsDictionary(10);
                FillCurrentTermTranslationLists(CurrentWordsDictionary);
            }

            this.openWindowService = openWindowService;
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
                    {
                        WordsDictionary = ReadFromFileService.ReturnWordsDictionary();
                        IsTestOn = true;
                    }
                    else IsTestOn = false;
                    break;
                case "IsTestOpenFirstly":
                    IsTestOpenFirstly = StaticConfigProvider.IsTestOpenFirstly;
                    break;
            }
        }

        public Dictionary<string, double> GetWordsDictionary()
        {
            WordsDictionary = ReadFromFileService.ReturnWordsDictionary();
            return WordsDictionary;
        }

        // Methods for non-test mode
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
            GetTermList();
            GetTranslationList();

            if (IsTestOn)
            {
                IsTestOpenFirstly = true;
                openWindowService.CreateTestWindow();   
            }
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

        public void FillCurrentTermTranslationLists(Dictionary<string, double> currentDictionary)
        {
            List<string> tempTermTranslationList = new List<string>();
            CurrentTermList = new List<string>();
            CurrentTranslationList = new List<string>();
            foreach (KeyValuePair<string, double> record in currentDictionary)
            {
                tempTermTranslationList.Add(record.Key);
            }

            foreach (string str in tempTermTranslationList)
            {
                int index = str.IndexOf(" - ");
                if (index != -1)
                {
                    int translationLength = str.Length - 2 - index;
                    CurrentTermList.Add(str.Substring(0, index + 2));
                    CurrentTranslationList.Add(str.Substring(index + 2, translationLength));
                }
                else
                {
                    int secondIndex = str.IndexOf("-");
                    int translationLength = str.Length - 1 - secondIndex;
                    CurrentTermList.Add(str.Substring(0, secondIndex + 1));
                    CurrentTranslationList.Add(str.Substring(secondIndex + 1, translationLength));
                }

            }

        }

        public void ReSortWordsDictionary()
        {
            WordsDictionary = (from entry in WordsDictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            ReadFromFileService.GetSortedTermTranslationList(WordsDictionary);
            //GetTermList();
            //GetTranslationList();
            StaticNotifyPropertyChanged();
        }

        public void ReSortCurrentWordsDictionary()
        {
            CurrentWordsDictionary = (from entry in CurrentWordsDictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            ReadFromFileService.GetSortedTermTranslationList(CurrentWordsDictionary);
            SynchronizeWordsDictionaries();
            ReSortWordsDictionary();
            //GetTermList();
            //GetTranslationList();
            StaticNotifyPropertyChanged();
        }

        public void SynchronizeWordsDictionaries()
        {
            foreach (KeyValuePair<string, double> currentWordsDictionaryRecord in CurrentWordsDictionary)
            {
                foreach (var key in WordsDictionary.Keys.ToList())
                {
                    for (double value = WordsDictionary[key]; ;)
                    {
                        if (key == currentWordsDictionaryRecord.Key)
                        {
                            WordsDictionary[key] = currentWordsDictionaryRecord.Value;
                            break;
                        }
                        else break;
                    }
                }
            }
        }

        public static void SerializeWordsDictionary()
        {
            if(WordsDictionary.Count != 0)
                StaticConfigProvider.WordsDictionary = JsonConvert.SerializeObject(WordsDictionary);
        }

        #region Instance and Static NPC
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
