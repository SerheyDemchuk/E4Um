using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using E4Um.AppSettings;
using E4Um.Models;
using E4Um.Helpers;

namespace E4Um.ViewModels
{
    class TestWindowModel : ViewModelBase
    {
        #region Term property
        string term;
        public string Term
        {
            get { return term; }
            set
            {
                if (term != value)
                {
                    term = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Translation properties
        Dictionary<string, bool> translationOne;
        public Dictionary<string, bool> TranslationOne
        {
            get { return translationOne; }
            set
            {
                if (translationOne != value)
                {
                    translationOne = value;
                    NotifyPropertyChanged();
                }
            }
        }

        Dictionary<string, bool> translationTwo;
        public Dictionary<string, bool> TranslationTwo
        {
            get { return translationTwo; }
            set
            {
                if (translationTwo != value)
                {
                    translationTwo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        Dictionary<string, bool> translationThree;
        public Dictionary<string, bool> TranslationThree
        {
            get { return translationThree; }
            set
            {
                if (translationThree != value)
                {
                    translationThree = value;
                    NotifyPropertyChanged();
                }
            }
        }

        Dictionary<string, bool> translationFour;
        public Dictionary<string, bool> TranslationFour
        {
            get { return translationFour; }
            set
            {
                if (translationFour != value)
                {
                    translationFour = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Translation Color properties
        Color translationOneColor;
        public Color TranslationOneColor
        {
            get { return translationOneColor; }
            set
            {
                if (translationOneColor != value)
                {
                    translationOneColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        Color translationTwoColor;
        public Color TranslationTwoColor
        {
            get { return translationTwoColor; }
            set
            {
                if (translationTwoColor != value)
                {
                    translationTwoColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        Color translationThreeColor;
        public Color TranslationThreeColor
        {
            get { return translationThreeColor; }
            set
            {
                if (translationThreeColor != value)
                {
                    translationThreeColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        Color translationFourColor;
        public Color TranslationFourColor
        {
            get { return translationFourColor; }
            set
            {
                if (translationFourColor != value)
                {
                    translationFourColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Dictionary
        Dictionary<string, double> currentWordsDictionary;
        Dictionary<string, double> CurrentWordsDictionary
        {
            get { return currentWordsDictionary; }
            set
            {
                if (currentWordsDictionary != value)
                {
                    currentWordsDictionary = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Lists
        List<string> termList;
        public List<string> TermList
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

        List<Dictionary<string, bool>> variantsList;
        public List<Dictionary<string, bool>> VariantsList
        {
            get { return variantsList; }
            set
            {
                if (variantsList != value)
                {
                    variantsList = value;
                    NotifyPropertyChanged();
                }
            }
        }

        List<Dictionary<string, bool>> answerList;
        public List<Dictionary<string, bool>> AnswerList
        {
            get { return answerList; }
            set
            {
                if (answerList != value)
                {
                    answerList = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        #region Counters
        int correctAnswersCounter;
        public int CorrectAnswersCounter
        {
            get { return correctAnswersCounter; }
            set
            {
                if (correctAnswersCounter != value)
                {
                    correctAnswersCounter = value;
                    NotifyPropertyChanged();
                }
            }
        }

        int incorrectAnswersCounter;
        public int IncorrectAnswersCounter
        {
            get { return incorrectAnswersCounter; }
            set
            {
                if (incorrectAnswersCounter != value)
                {
                    incorrectAnswersCounter = value;
                    NotifyPropertyChanged();
                }
            }
        }

        int newWordsCounter;
        public int NewWordsCounter
        {
            get { return newWordsCounter; }
            set
            {
                if (newWordsCounter != value)
                {
                    newWordsCounter = value;
                    NotifyPropertyChanged();
                }
            }
        }

        bool isCountersVisible;
        public bool IsCountersVisible
        {
            get { return isCountersVisible; }
            set
            {
                if (isCountersVisible != value)
                {
                    isCountersVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        int blockVolume;
        public int BlockVolume
        {
            get { return blockVolume; }
            set
            {
                if (blockVolume != value)
                {
                    blockVolume = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PopUp Model { get; }
        public int InitialFillTestIndex { get; set; }
        public int FillTestIndex { get; set; }
        public int TestAppearCounter { get; set; }
        public bool IsDictionaryCompleted { get; set; }
        public bool IsNewWord { get; set; }
        public bool IsNextWord { get; set; }
        public int PriorityReduсtionCounter { get; set; }
        public int DictionaryRefreshCounter { get; set; }
        public int DictionaryRefreshIndex { get; set; }
        public static int NextWordCounter { get; set; }
        public int NewWordsAmount { get; set; }
        public int FillTestNextWordIndex { get; set; }
        bool TranslationClickCanExecute { get; set; }

        readonly IWindowService openWindowService;

        public RelayCommand TranslationClickCommand { get; set; }
        public TestWindowModel(PopUp model, IWindowService openWindowService)
        {
            Model = model;
            BlockVolume = SetBlockVolume();
            NewWordsCounter = BlockVolume;
            InitialFillTestIndex = 0;
            FillTestIndex = 0;
            TestAppearCounter = 0;
            IsDictionaryCompleted = false;
            IsNewWord = true;
            PriorityReduсtionCounter = 0;
            DictionaryRefreshCounter = BlockVolume;
            DictionaryRefreshIndex = BlockVolume;
            NextWordCounter = 0;
            NewWordsAmount = 0;
            FillTestNextWordIndex = BlockVolume;
            TranslationClickCanExecute = true;
            CorrectAnswersCounter = 0;
            IncorrectAnswersCounter = 0;
            
            if (Model.IsTestOpenFirstly)
            {
                IsCountersVisible = true;
                CurrentWordsDictionary = new Dictionary<string, double>();
                TermList = Model.GetTermList();
                TranslationList = Model.GetTranslationList();
            }
            else
            {
                CurrentWordsDictionary = Model.GetCurrentWordsDictionary(BlockVolume);
                TermList = Model.GetCurrentTermList(BlockVolume);
                TranslationList = Model.GetCurrentTranslationList(BlockVolume);
            }
            
            VariantsList = new List<Dictionary<string, bool>>();
            AnswerList = new List<Dictionary<string, bool>>();

            InitialFillTest();
            this.openWindowService = openWindowService;
            TranslationOneColor = Color.FromArgb(255, 0, 0, 0);
            TranslationTwoColor = Color.FromArgb(255, 0, 0, 0);
            TranslationThreeColor = Color.FromArgb(255, 0, 0, 0);
            TranslationFourColor = Color.FromArgb(255, 0, 0, 0);
            TranslationClickCommand = new RelayCommand(TranslationClickCommand_Execute, TranslationClickCommand_CanExecute);
        }

        int SetBlockVolume()
        {
            int wordsAmount = PopUp.WordsDictionary.Count;
            if (wordsAmount >= 10)
                BlockVolume = 10;
            else BlockVolume = wordsAmount;
            return BlockVolume;
        }

        void TranslationClickCommand_Execute(object parameter)
        {

            TranslationClickCanExecute = false;

            List<object> objUserAnswer = parameter as List<object>;
            List<string> userAnswer = objUserAnswer.Select(s => (string)s).ToList();

            int correctAnswerNumber = 0;

            if (Model.IsTestOpenFirstly)
            {
                foreach (Dictionary<string, bool> listItem in VariantsList)
                {
                    foreach (KeyValuePair<string, bool> translation in listItem)
                    {
                        if (translation.Value == true)
                        {
                            if (" " + userAnswer[0] == translation.Key)
                            {
                                AnswerList.Add(new Dictionary<string, bool> { { Term + " " + "-" + translation.Key, true } });
                                CorrectAnswersCounter++;
                            }
                            else
                            {
                                AnswerList.Add(new Dictionary<string, bool> { { Term + " " + "-" + translation.Key, false } });
                                IncorrectAnswersCounter++;
                                if (Model.IsTestOpenFirstly)
                                   PopUp.CurrentWordsDictionary.Add(Term + " " + "-" + translation.Key, 5);
                            }
                            goto FillColor;
                        }
                        correctAnswerNumber++;
                    }
                }
            }
            else
            {
                foreach (Dictionary<string, bool> listItem in VariantsList)
                {
                    foreach (KeyValuePair<string, bool> translation in listItem)
                    {
                        if (translation.Value == true)
                        {
                            if (" " + userAnswer[0] == translation.Key)
                            {
                                AnswerList.Add(new Dictionary<string, bool> { { Term + " " + "-" + translation.Key, true } });
                                CorrectAnswersCounter++;
                            }
                            else
                            {
                                AnswerList.Add(new Dictionary<string, bool> { { Term + " " + "-" + translation.Key, false } });
                                IncorrectAnswersCounter++;
                            }
                            goto FillColor;
                        }
                        correctAnswerNumber++;
                    }
                }
            }
            

        FillColor:
            switch (userAnswer[1])
            {
                case "TranslationOne":
                    TranslationOneColor = Color.FromArgb(255, 128, 0, 0);
                    break;
                case "TranslationTwo":
                    TranslationTwoColor = Color.FromArgb(255, 128, 0, 0);
                    break;
                case "TranslationThree":
                    TranslationThreeColor = Color.FromArgb(255, 128, 0, 0);
                    break;
                case "TranslationFour":
                    TranslationFourColor = Color.FromArgb(255, 128, 0, 0);
                    break;
            }
            switch (correctAnswerNumber)
            {
                case 0:
                    TranslationOneColor = Color.FromArgb(255, 0, 128, 0);
                    break;
                case 1:
                    TranslationTwoColor = Color.FromArgb(255, 0, 128, 0);
                    break;
                case 2:
                    TranslationThreeColor = Color.FromArgb(255, 0, 128, 0);
                    break;
                case 3:
                    TranslationFourColor = Color.FromArgb(255, 0, 128, 0);
                    break;
            }

            if (Model.IsTestOpenFirstly)
            {
                if (IncorrectAnswersCounter < 10)
                {
                    if(InitialFillTestIndex < PopUp.WordsDictionary.Count)
                    {
                        Task.Run(() =>
                        {
                            Thread.Sleep(500);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                InitialFillTest();
                            });
                        });
                    }
                    else
                    {
                        //StaticConfigProvider.IsTestOpenFirstly = false;
                        CompleteWordsDictionary();
                        Model.IsTestOpenFirstly = false;
                        IsCountersVisible = false;
                        StaticConfigProvider.StartTimer = true; 
                        Model.FillCurrentTermTranslationLists(PopUp.CurrentWordsDictionary);

                        openWindowService.HideTestWindow();

                        Task.Run(() =>
                        {
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                FillTest();
                            });
                        });
                    }

                }
                else
                {
                    //StaticConfigProvider.IsTestOpenFirstly = false;
                    Model.IsTestOpenFirstly = false;
                    IsCountersVisible = false;
                    StaticConfigProvider.StartTimer = true;
                    Model.FillCurrentTermTranslationLists(PopUp.CurrentWordsDictionary);
                    WordsDictionaryChangePriority(AnswerList);
                    AnswerList.Clear(); 

                    openWindowService.HideTestWindow();

                    Task.Run(() =>
                    {
                        Thread.Sleep(2000);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            FillTest();
                        });
                    });

                }

            }
            else
            {
                TestAppearCounter++;

                if (TestAppearCounter < BlockVolume)
                {
                    Task.Run(() =>
                    {
                        Thread.Sleep(500);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            FillTest();
                        });
                    });

                }
                else
                {

                    CurrentWordsDictionaryChangePriority(AnswerList);

                    if(NextWordCounter != 0)
                    {
                        IsCountersVisible = true;
                        AnswerList.Clear();
                        Task.Run(() =>
                        {
                            Thread.Sleep(500);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                FillTestNext();
                            });
                        });
                    }
                    else
                    {
                        openWindowService.HideTestWindow();
                        StaticConfigProvider.StartTimer = true;

                        IsCountersVisible = false;
                        IsNextWord = false;

                        AnswerList.Clear();
                        TestAppearCounter = 0;
                        FillTestIndex = 0;

                        // Delay and next appearing filling
                        Task.Run(() =>
                        {
                            Thread.Sleep(2000);
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                FillTest();
                            });
                        });
                        // Delay and next appearing filling
                    }


                }
            }

        }

        bool TranslationClickCommand_CanExecute(object parameter)
        {
            if (TranslationClickCanExecute)
                return true;
            else return false;
        }

        void CompleteWordsDictionary()
        {
            IsDictionaryCompleted = true;

            if (PopUp.CurrentWordsDictionary.Count == 0)
            {
                for (int i = 0; i < BlockVolume; i++)
                {
                    PopUp.CurrentWordsDictionary.Add(PopUp.WordsDictionary.ElementAt(i).Key, PopUp.WordsDictionary.ElementAt(i).Value);
                }
            }
            else if (PopUp.CurrentWordsDictionary.Count != 0)
            {
                int currentWordsDictionaryCount = PopUp.CurrentWordsDictionary.Count();
                Model.ReSortCurrentWordsDictionary();
                for (int i = currentWordsDictionaryCount; i < BlockVolume; i++)
                {
                    if (!PopUp.CurrentWordsDictionary.Keys.Contains(PopUp.WordsDictionary.ElementAt(i).Key))
                    {
                        PopUp.CurrentWordsDictionary.Add(PopUp.WordsDictionary.ElementAt(i).Key, PopUp.WordsDictionary.ElementAt(i).Value);
                    }
                }
            }
            AnswerList.Clear();
        }

        void InitialFillTest()
        {
            VariantsList.Clear();
            TranslationOneColor = Color.FromArgb(255, 0, 0, 0);
            TranslationTwoColor = Color.FromArgb(255, 0, 0, 0);
            TranslationThreeColor = Color.FromArgb(255, 0, 0, 0);
            TranslationFourColor = Color.FromArgb(255, 0, 0, 0);

            Term = TermList[InitialFillTestIndex].Remove(TermList[InitialFillTestIndex].Length - 2);
            VariantsList.Add(new Dictionary<string, bool> { { TranslationList[InitialFillTestIndex], true } });


            List<int> indexesList = new List<int>();
            indexesList.Add(InitialFillTestIndex);
            int translationIndex = 0;
            int tempIndex = 0;
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
            AnotherIndex:
                tempIndex = rnd.Next(0, TermList.Count - 1);
                if (!indexesList.Contains(tempIndex))
                {
                    translationIndex = tempIndex;
                    indexesList.Add(tempIndex);
                }
                else goto AnotherIndex;

                VariantsList.Add(new Dictionary<string, bool> { { TranslationList[translationIndex], false } });
            }

            VariantsList.Shuffle();

            TranslationOne = VariantsList[0];
            TranslationTwo = VariantsList[1];
            TranslationThree = VariantsList[2];
            TranslationFour = VariantsList[3];

            TranslationClickCanExecute = true;
            InitialFillTestIndex += 1;

        }

        void FillTest()
        {
            VariantsList.Clear();
            TranslationOneColor = Color.FromArgb(255, 0, 0, 0);
            TranslationTwoColor = Color.FromArgb(255, 0, 0, 0);
            TranslationThreeColor = Color.FromArgb(255, 0, 0, 0);
            TranslationFourColor = Color.FromArgb(255, 0, 0, 0);

            Term = PopUp.CurrentTermList[FillTestIndex].Remove(PopUp.CurrentTermList[FillTestIndex].Length - 2);
            VariantsList.Add(new Dictionary<string, bool> { { PopUp.CurrentTranslationList[FillTestIndex], true } });

            List<int> indexesList = new List<int>();
            indexesList.Add(FillTestIndex);
            int translationIndex = 0;
            int tempIndex = 0;
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
            AnotherIndex:
                tempIndex = rnd.Next(0, PopUp.CurrentTermList.Count - 1);
                if (!indexesList.Contains(tempIndex))
                {
                    translationIndex = tempIndex;
                    indexesList.Add(tempIndex);
                }
                else goto AnotherIndex;

                VariantsList.Add(new Dictionary<string, bool> { { PopUp.CurrentTranslationList[translationIndex], false } });
            }

            VariantsList.Shuffle();

            TranslationOne = VariantsList[0];
            TranslationTwo = VariantsList[1];
            TranslationThree = VariantsList[2];
            TranslationFour = VariantsList[3];

            TranslationClickCanExecute = true;
            FillTestIndex += 1;

        }

        void FillTestNext()
        {
            VariantsList.Clear();
            TranslationOneColor = Color.FromArgb(255, 0, 0, 0);
            TranslationTwoColor = Color.FromArgb(255, 0, 0, 0);
            TranslationThreeColor = Color.FromArgb(255, 0, 0, 0);
            TranslationFourColor = Color.FromArgb(255, 0, 0, 0);

            Term = PopUp.CurrentTermList[FillTestNextWordIndex].Remove(PopUp.CurrentTermList[FillTestNextWordIndex].Length - 2);
            VariantsList.Add(new Dictionary<string, bool> { { PopUp.CurrentTranslationList[FillTestNextWordIndex], true } });

            List<int> indexesList = new List<int>();
            indexesList.Add(FillTestNextWordIndex);
            int translationIndex = 0;
            int tempIndex = 0;
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
            AnotherIndex:
                tempIndex = rnd.Next(0, PopUp.CurrentTermList.Count - 1);
                if (!indexesList.Contains(tempIndex))
                {
                    translationIndex = tempIndex;
                    indexesList.Add(tempIndex);
                }
                else goto AnotherIndex;

                VariantsList.Add(new Dictionary<string, bool> { { PopUp.CurrentTranslationList[translationIndex], false } });
            }

            VariantsList.Shuffle();

            TranslationOne = VariantsList[0];
            TranslationTwo = VariantsList[1];
            TranslationThree = VariantsList[2];
            TranslationFour = VariantsList[3];

            TranslationClickCanExecute = true;
            FillTestIndex += 1;

        }

        void WordsDictionaryChangePriority(List<Dictionary<string, bool>> AnswerList)
        {
            foreach (Dictionary<string, bool> listItem in AnswerList)
            {
                foreach (KeyValuePair<string, bool> answer in listItem)
                {
                    foreach (var key in PopUp.WordsDictionary.Keys.ToList())
                    {
                        for (double value = PopUp.WordsDictionary[key]; ;)
                        {
                            if (answer.Key == key)
                            {
                                if(!answer.Value)
                                {
                                    if (value < 5)
                                    {
                                        PopUp.WordsDictionary[key] += 2;
                                        break;
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
           Model.ReSortWordsDictionary();

        }

        void CurrentWordsDictionaryChangePriority(List<Dictionary<string, bool>> AnswerList)
        {

            foreach (Dictionary<string, bool> listItem in AnswerList)
            {
                foreach (KeyValuePair<string, bool> answer in listItem)
                {
                    foreach (var key in PopUp.CurrentWordsDictionary.Keys.ToList())
                    {
                        for (double value = PopUp.CurrentWordsDictionary[key]; ;)
                        {
                            if (answer.Key == key)
                            {
                                if (answer.Value == true)
                                {
                                    if (value > 1)
                                    {
                                        if (NextWordCounter == 0)
                                        {
                                            PopUp.CurrentWordsDictionary[key] -= 1;
                                            PriorityReduсtionCounter++;
                                            break;
                                        }
                                        else
                                        {
                                            if(PopUp.WordsDictionary[key] != 2)
                                            {
                                                //PopUp.WordsDictionary[key] -= 1;
                                                PopUp.CurrentWordsDictionary.Remove(key);
                                                Model.FillCurrentTermTranslationLists(PopUp.CurrentWordsDictionary);
                                                if (PopUp.WordsDictionary[key] == 3)
                                                {
                                                    PopUp.WordsDictionary[key] -= 1;
                                                    Model.ReSortWordsDictionary();
                                                }
                                                NextWord();
                                                break;
                                            }
                                            //else if (PopUp.WordsDictionary[key] == 2)
                                            //{
                                            //    DictionaryRefreshCounter = PopUp.WordsDictionary.Count;
                                            //    NextWord();
                                            //    break;
                                            //}   
                                        } 

                                    }
                                    break;
                                }
                                else
                                {
                                    if (value < 5)
                                    {
                                        if (NextWordCounter == 0)
                                        {
                                            PopUp.CurrentWordsDictionary[key] += 1;
                                            break;
                                        }
                                        else
                                        {
                                            DeleteWord();
                                            PopUp.CurrentWordsDictionary[key] += 2;
                                            break;
                                        }

                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }

            Model.ReSortCurrentWordsDictionary();

            if (NextWordCounter == 0 && !IsDictionaryCompleted)
            {
                foreach (var key in PopUp.CurrentWordsDictionary.Keys.ToList())
                {
                    if (PopUp.CurrentWordsDictionary[key] == 3 && PriorityReduсtionCounter != 0)
                    {
                        NextWordCounter++;
                        NextWord();

                        if (!IsNextWord)
                        {
                            IsCountersVisible = true;
                            CorrectAnswersCounter = 0;
                            IncorrectAnswersCounter = 0;
                            NewWordsCount();
                            IsNextWord = true;
                        }
                        break;
                    }
                }

            }
            else if(NextWordCounter == 0 && IsDictionaryCompleted)
            {
                RefreshCurrentDictionary();
            }
            //else if(NextWordCounter == 0 && !IsNewWord)
            //{
            //    RefreshCurrentDictionary();
            //}
            

        }

        public void NextWord()
        {
            //if(DictionaryRefreshCounter < PopUp.WordsDictionary.Count )
            //{
                if(PopUp.WordsDictionary.ElementAt(DictionaryRefreshCounter).Value != 2)
                {
                    PopUp.CurrentWordsDictionary.Add(PopUp.WordsDictionary.ElementAt(DictionaryRefreshCounter).Key, PopUp.WordsDictionary.ElementAt(DictionaryRefreshCounter).Value);
                    Model.FillCurrentTermTranslationLists(PopUp.CurrentWordsDictionary);
                }
                else
                {
                    IsDictionaryCompleted = true;
                    NextWordCounter = 0;
                }
                
                //DictionaryRefreshCounter++;
            //}
            //else
            //{
            //    //IsNewWord = false;
            //    NextWordCounter = 0;
            //    RefreshCurrentDictionary();

            //}
            
        }

        public void DeleteWord()
        {
            foreach (var key in PopUp.CurrentWordsDictionary.Keys.ToList())
            {
                if (PopUp.CurrentWordsDictionary[key] == 3 && PriorityReduсtionCounter != 0)
                {
                    PopUp.CurrentWordsDictionary.Remove(key);
                    PopUp.WordsDictionary[key] -= 1;
                    NextWordCounter--;
                    NewWordsAmount++;
                    DictionaryRefreshIndex += NewWordsAmount;
                    break;
                }
            }
        }

        void RefreshCurrentDictionary()
        {

            foreach (var key in PopUp.CurrentWordsDictionary.Keys.ToList())
            {
                if (PopUp.CurrentWordsDictionary[key] == 1)
                {

                NextRecord:
                    if (DictionaryRefreshIndex < PopUp.WordsDictionary.Count)
                    {
                        if (PopUp.WordsDictionary.Values.Contains(5) || PopUp.WordsDictionary.Values.Contains(4) || PopUp.WordsDictionary.Values.Contains(3) || PopUp.WordsDictionary.Values.Contains(2))
                        {
                            if (!PopUp.CurrentWordsDictionary.Keys.Contains(PopUp.WordsDictionary.ElementAt(DictionaryRefreshIndex).Key) && PopUp.WordsDictionary.ElementAt(DictionaryRefreshIndex).Value != 1)
                            {
                                PopUp.CurrentWordsDictionary.Add(PopUp.WordsDictionary.ElementAt(DictionaryRefreshIndex).Key, PopUp.WordsDictionary.ElementAt(DictionaryRefreshIndex).Value);
                                PopUp.CurrentWordsDictionary.Remove(key);
                                PopUp.WordsDictionary[key] -= 1;
                                DictionaryRefreshIndex++;
                            }
                            else 
                            {
                                if (CheckWordsDictionary())
                                {
                                    DictionaryRefreshIndex++;
                                    goto NextRecord;
                                }
                                else
                                {
                                    //PopUp.CurrentWordsDictionary.Remove(key);
                                    DictionaryRefreshIndex++;
                                }
                                
                            }
                        }
                        else
                        {
                            StaticConfigProvider.IsTestOn = false;
                            break;
                        }

                    }
                    else
                    {
                        if (PopUp.WordsDictionary.Values.Contains(5) || PopUp.WordsDictionary.Values.Contains(4) || PopUp.WordsDictionary.Values.Contains(3) || PopUp.WordsDictionary.Values.Contains(2))
                        {
                            DictionaryRefreshIndex = 0;

                            if (!PopUp.CurrentWordsDictionary.Keys.Contains(PopUp.WordsDictionary.ElementAt(DictionaryRefreshIndex).Key) && PopUp.WordsDictionary.ElementAt(DictionaryRefreshIndex).Value != 1)
                            {
                                PopUp.CurrentWordsDictionary.Add(PopUp.WordsDictionary.ElementAt(DictionaryRefreshIndex).Key, PopUp.WordsDictionary.ElementAt(DictionaryRefreshIndex).Value);
                                PopUp.CurrentWordsDictionary.Remove(key);
                                PopUp.WordsDictionary[key] -= 1;
                                DictionaryRefreshIndex++;
                            }
                            else
                            {
                                DictionaryRefreshIndex++;
                                goto NextRecord;
                            }

                        }
                        else
                        {
                            StaticConfigProvider.IsTestOn = false;
                            break;
                        }
                    }
                    
                }
            }

            Model.ReSortCurrentWordsDictionary();
            Model.FillCurrentTermTranslationLists(PopUp.CurrentWordsDictionary);
        }

        bool CheckWordsDictionary()
        {
            foreach(var key in PopUp.WordsDictionary.Keys.ToList())
            {
                if (!PopUp.CurrentWordsDictionary.Keys.Contains(key) && PopUp.WordsDictionary[key] != 1)
                    return true;
            }
            return false;
        }

        void NewWordsCount()
        {
            NewWordsCounter = 0;
            foreach (var key in PopUp.CurrentWordsDictionary.Keys.ToList())
            {
                if (PopUp.CurrentWordsDictionary[key] == 3 && PriorityReduсtionCounter != 0)
                {
                    NewWordsCounter++;
                }
            }
            NewWordsCounter -= 1;
        }

        //int wordsAmount = WordsDictionary.Count;
        //if (wordsAmount > 10)
        //{
        //    if(wordsAmount % 2 == 0)
        //    Index += 2;
        //    else
        //    {
        //        if (Index != wordsAmount - 1)
        //            Index += 2;
        //    }
        //}
        //else Index += 1;

        //public void CheckNextWord()
        //{

        //    //if(NextWordCounter == 0)
        //    //{
        //        foreach (var key in PopUp.CurrentWordsDictionary.Keys.ToList())
        //        {
        //            for (double value = PopUp.CurrentWordsDictionary[key]; ;)
        //            {
        //                if (PopUp.CurrentWordsDictionary[key] == 3 && PriorityReduсtionCounter != 0)
        //                {
        //                    PopUp.CurrentWordsDictionary.Add(PopUp.WordsDictionary.ElementAt(DictionaryRefreshCounter).Key, PopUp.WordsDictionary.ElementAt(DictionaryRefreshCounter).Value);
        //                    //PopUp.CurrentWordsDictionary.Remove(key);
        //                    Model.FillCurrentTermTranslationLists(PopUp.CurrentWordsDictionary);
        //                    DictionaryRefreshCounter++;
        //                    NextWordCounter++;
        //                }
        //                break;
        //            }
        //            break;
        //        }
        //        //FillTestNextWordIndex = NextWordCounter;
        //    //}
        //}

        //void FillCurrentWordsDictionary()
        //{
        //    CurrentWordsDictionary = (from entry in CurrentWordsDictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

        //    foreach (KeyValuePair<string, double> record in CurrentWordsDictionary)
        //    {
        //        if(ResultWordsDictionary.Count < 10)
        //        {
        //            if (record.Value == 5)
        //                ResultWordsDictionary.Add(record.Key, record.Value);
        //            else
        //            {

        //            }
        //        }

        //    }

        //}

        //public static event PropertyChangedEventHandler StaticPropertyChanged;
        //public static void StaticNotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{
        //    if (StaticPropertyChanged != null)
        //    {
        //        StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}
