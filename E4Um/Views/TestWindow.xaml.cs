using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace E4Um.Views
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : MetroWindow
    {
        public TestWindow()
        {
            Point pt = SystemParameters.WorkArea.TopLeft;
            InitializeComponent();
            pt.Offset(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
            pt.Offset(-Width, -Height);
            Left = pt.X - 5;
            Top = pt.Y - 5;
            ChangeAppStyle();
        }

        public void ChangeAppStyle()
        {
            // set the Red accent and dark theme only to the current window
            ThemeManager.ChangeAppStyle(this,
                                        ThemeManager.GetAccent("Green"),
                                        ThemeManager.GetAppTheme("BaseLight"));
        }
    }
}
