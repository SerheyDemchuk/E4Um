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
    class TestWindowModel: ViewModelBase
    {
        #region Term property
        string term;
        public string Term
        {
            get { return term; }
            set
            {
                if(term != value)
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
                if(translationOneColor != value)
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

        Dictionary<string, double> resultWordsDictionary;
        Dictionary<string, double> ResultWordsDictionary
        {
            get { return resultWordsDictionary; }
            set
            {
                if (resultWordsDictionary != value)
                {
                    resultWordsDictionary = value;
                    NotifyPropertyChanged();
                }
            }
        }

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

        //int questCounter;
        //public int QuestCounter
        //{
        //    get { return questCounter; }
        //    set
        //    {
        //        if (questCounter != value)
        //        {
        //            questCounter = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //int questAmount;
        //public int QuestAmount
        //{
        //    get { return questAmount; }
        //    set
        //    {
        //        if (questAmount != value)
        //        {
        //            questAmount = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        public PopUp Model { get; }
        public int Index { get; set; }
        public bool IsTestOpenFirstly { get; set; }
        readonly IWindowService openWindowService;

        public RelayCommand TranslationClickCommand { get; set; }
        public TestWindowModel(PopUp model, IWindowService openWindowService)
        {
            Model = model;
            Index = 0;
            CorrectAnswersCounter = 0;
            IncorrectAnswersCounter = 0;
            BlockVolume = 10;
            
            if (Model.IsTestOpenFirstly)
            {
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
            //setQuestAmount();
            //QuestCounter = QuestAmount;
            FillTest();
            this.openWindowService = openWindowService;
            TranslationOneColor = Color.FromArgb(255, 0, 0, 0);
            TranslationTwoColor = Color.FromArgb(255, 0, 0, 0);
            TranslationThreeColor = Color.FromArgb(255, 0, 0, 0);
            TranslationFourColor = Color.FromArgb(255, 0, 0, 0);
            TranslationClickCommand = new RelayCommand(TranslationClickCommand_Execute);
        }

        //void setQuestAmount()
        //{
        //    int wordsAmount = CurrentWordsDictionary.Count;
        //    if (wordsAmount >= 10)
        //        QuestAmount = 10;
        //    else QuestAmount = wordsAmount;
        //}

        void FillTest()
        {
            VariantsList.Clear();
            TranslationOneColor = Color.FromArgb(255, 0, 0, 0);
            TranslationTwoColor = Color.FromArgb(255, 0, 0, 0);
            TranslationThreeColor = Color.FromArgb(255, 0, 0, 0);
            TranslationFourColor = Color.FromArgb(255, 0, 0, 0);

            Term = TermList[Index].Remove(TermList[Index].Length - 2);
            VariantsList.Add(new Dictionary<string, bool> { { TranslationList[Index], true } });

            List<int> indexesList = new List<int>();
            indexesList.Add(Index);
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

            Index += 1;
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
            
        }

        void TranslationClickCommand_Execute(object parameter)
        {
            //bool isLearning = false;

            List<object> objUserAnswer = parameter as List<object>;
            List<string> userAnswer = objUserAnswer.Select(s => (string)s).ToList();

            int correctAnswerNumber = 0;
            bool isUserAnswerCorrect = false;

            foreach (Dictionary<string, bool> listItem in VariantsList)
            {
                foreach (KeyValuePair<string, bool> translation in listItem)
                {
                    if (translation.Value == true)
                    {
                        if (" " + userAnswer[0] == translation.Key)
                        {
                            isUserAnswerCorrect = true;
                            AnswerList.Add(new Dictionary<string, bool> { { Term + " " + "-" + translation.Key, true } });
                            CorrectAnswersCounter++;
                        }
                        else
                        {
                            AnswerList.Add(new Dictionary<string, bool> { { Term + " " + "-" + translation.Key, false } });
                            IncorrectAnswersCounter++;
                            if(Model.IsTestOpenFirstly)
                            CurrentWordsDictionary.Add(Term + " " + "-" + translation.Key, 5);
                        } 
                        goto FillColor;
                    }
                    correctAnswerNumber++;
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

            if (IncorrectAnswersCounter < 10)
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
                Model.IsTestOpenFirstly = false;
                ReSortCurrentDictionary(AnswerList);
                openWindowService.HideTestWindow();
            }
            //TranslationOneColor = Color.FromArgb(255, 0, 128, 0);
            //openWindowService.HideTestWindow(isUserAnswerCorrect, isLearning);
        }

        void ReSortCurrentDictionary(List<Dictionary<string, bool>> AnswerList)
        {


            foreach (Dictionary<string, bool> listItem in AnswerList)
            {
                foreach (KeyValuePair<string, bool> answer in listItem)
                {
                    foreach (var key in CurrentWordsDictionary.Keys.ToList())
                    {
                        for (double value = CurrentWordsDictionary[key]; ;)
                        {
                            if (answer.Key == key)
                            {
                                if(answer.Value == true)
                                {
                                    if(value > 1)
                                    {
                                        CurrentWordsDictionary[key] -= 2;
                                        break;
                                    }
                                    break;
                                }
                                else
                                {
                                    if (value < 5)
                                    {
                                        CurrentWordsDictionary[key] += 2;
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
            //StaticNotifyPropertyChanged();
           Model.ReSortWordsDictionary();

        }

        void FillCurrentWordsDictionary()
        {
            CurrentWordsDictionary = (from entry in CurrentWordsDictionary orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);

            foreach (KeyValuePair<string, double> record in CurrentWordsDictionary)
            {
                if(ResultWordsDictionary.Count < 10)
                {
                    if (record.Value == 5)
                        ResultWordsDictionary.Add(record.Key, record.Value);
                    else
                    {
                        
                    }
                }
                
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
    }
}
