using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace E4Um.Helpers
{
    class ReadFromFileService
    {
        static Dictionary<string, double> words = new Dictionary<string, double>();

        public static Dictionary<string, double> ReturnWords()
        {
            if (words.Count == 0)
            {
                StreamReader reader = new StreamReader("English\\Аэропорт. The Airport" + ".txt", Encoding.Default);
                string curLine;
                while ((curLine = reader.ReadLine()) != null)
                {
                    words.Add(curLine, 10);
                }
                reader.Close();
                return words;
            }
            else return words;
            
        }
        
            
    }
}
