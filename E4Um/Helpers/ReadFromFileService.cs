using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using E4Um.AppSettings;

namespace E4Um.Helpers
{
    class ReadFromFileService
    {
        static Dictionary<string, double> wordsDictionary = new Dictionary<string, double>();
        static List<string> termList = new List<string>();
        static List<string> translationList = new List<string>();

        public static Dictionary<string, double> ReturnWordsDictionary()
        {
            if (wordsDictionary.Count == 0)
            {
                StreamReader reader = new StreamReader("English\\Словарь 8000" + ".txt", Encoding.Default);
                string curLine;
                while ((curLine = reader.ReadLine()) != null)
                {
                    wordsDictionary.Add(curLine, 5);
                }
                StringSlicer(StaticConfigProvider.IsTermUpper, StaticConfigProvider.IsTranslationUpper);
                reader.Close();
                return wordsDictionary;
            }
            else return wordsDictionary;
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
            List<string> TermTranslationList = new List<string>();

            foreach (KeyValuePair<string, double> kvp in wordsDictionary)
            {
                TermTranslationList.Add(kvp.Key);
            }

            foreach (string str in TermTranslationList)
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

    }
}
