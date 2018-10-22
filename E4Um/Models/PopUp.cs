using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using E4Um.Helpers;

namespace E4Um.Models
{
    static class PopUp
    {
       static Dictionary<string, double> Words { get; set; }

        public static Dictionary<string, double> GetWords()
        {
            Words = ReadFromFileService.ReturnWords();
            return Words;
        }
    }
}
