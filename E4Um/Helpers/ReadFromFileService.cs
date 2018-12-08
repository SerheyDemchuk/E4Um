﻿using System.Collections.Generic;
using System.Text;
using System.IO;
using E4Um.AppSettings;

namespace E4Um.Helpers
{
    class ReadFromFileService
    {
        static Dictionary<string, double> wordsDictionary = new Dictionary<string, double>();
        //static Dictionary<string, double> dataGridWordsDictionary = new Dictionary<string, double>();

        static List<string> termTranslationList = new List<string>();
        static List<string> termList = new List<string>();
        static List<string> translationList = new List<string>();

        public static Dictionary<string, double> ReturnWordsDictionary()
        {
            wordsDictionary.Clear();
            foreach(string str in termTranslationList)
            {
                wordsDictionary.Add(str, 5);
            }
            return wordsDictionary;
        }

        public static List<string> ReturnTermTranslationList(string path, string callerMethodName)
        {
            termTranslationList.Clear();

            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                string curLine;
                while ((curLine = reader.ReadLine()) != null)
                {
                    if (!termTranslationList.Contains(curLine))
                        termTranslationList.Add(curLine);
                }

            }

            if (callerMethodName != "SelectedItem")
                StringSlicer(StaticConfigProvider.IsTermUpper, StaticConfigProvider.IsTranslationUpper);
            return termTranslationList;
        }

        public static List<string> ReturnTermList()
        {
            return termList;
        }

        public static List<string> ReturnTranslationList()
        {
            return translationList;
        }

        public static void StringSlicer(bool isTermUpper, bool isTranslationUpper)
        {
            termList.Clear();
            translationList.Clear();

            foreach (string str in termTranslationList)
            {
                int index = str.IndexOf(" - ");
                if (index != -1)
                {
                    int translationLength = str.Length - 2 - index;
                    if (isTermUpper)
                    {
                        string temp = str.Substring(0, index + 2);
                        termList.Add(temp.ToUpper());
                    }
                    else
                    {
                        string temp = str.Substring(0, index + 2);
                        termList.Add(temp.ToLower());
                    }
                    if (isTranslationUpper)
                    {
                        string temp = str.Substring(index + 2, translationLength);
                        translationList.Add(temp.ToUpper());
                    }
                    else
                    {
                        string temp = str.Substring(index + 2, translationLength);
                        translationList.Add(temp.ToLower());
                    }
                }
                else
                {
                    int secondIndex = str.IndexOf("-");
                    int translationLength = str.Length - 1 - secondIndex;
                    if (isTermUpper)
                    {
                        string temp = str.Substring(0, secondIndex + 1);
                        termList.Add(temp.ToUpper());
                    }
                    else
                    {
                        string temp = str.Substring(0, secondIndex + 1);
                        termList.Add(temp.ToLower());
                    }
                    if (isTranslationUpper)
                    {
                        string temp = str.Substring(secondIndex + 1, translationLength);
                        translationList.Add(temp.ToUpper());
                    }
                    else
                    {
                        string temp = str.Substring(secondIndex + 1, translationLength);
                        translationList.Add(temp.ToLower());
                    }
                }
            }
        }

        //public static void DataGridStringSlicer()
        //{
        //    termList.Clear();
        //    translationList.Clear();

        //    foreach (string str in termTranslationList)
        //    {
        //        int index = str.IndexOf(" - ");
        //        if (index != -1)
        //        {
        //            int translationLength = str.Length - 2 - index;
        //            termList.Add(str.Substring(0, index + 2));
        //            translationList.Add(str.Substring(index + 2, translationLength));
        //        }
        //        else
        //        {
        //            int secondIndex = str.IndexOf("-");
        //            int translationLength = str.Length - 1 - secondIndex;
        //            termList.Add(str.Substring(0, secondIndex + 1));
        //            translationList.Add(str.Substring(secondIndex + 1, translationLength));
        //        }

        //    }
        //}

        //public static Dictionary<string, double> ReturnDataGridWordsDictionary(string path)
        //{
        //    dataGridWordsDictionary.Clear();
        //    StreamReader reader = new StreamReader(path, Encoding.Default);
        //    string curLine;
        //    while ((curLine = reader.ReadLine()) != null)
        //    {
        //        dataGridWordsDictionary.Add(curLine, 5);
        //    }
        //    StringSlicer(StaticConfigProvider.IsTermUpper, StaticConfigProvider.IsTranslationUpper);
        //    reader.Close();
        //    return dataGridWordsDictionary;
        //}

    }
}
