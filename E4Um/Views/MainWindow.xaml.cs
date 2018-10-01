using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using E4Um.Views;
using E4Um.ViewModels;
using E4Um.Helpers;

namespace E4Um
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon nIcon = new NotifyIcon();
        public MainWindow()
        {
            InitializeComponent();

            // Initialising tray icon
            nIcon.DoubleClick += new EventHandler(nIcon_DoubleClick);
            nIcon.Icon = new Icon(@"../../Resources/uk.ico");
            nIcon.Visible = true;
            nIcon.Text = "E4U";

            // /Initialising tray icon
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PopUpWindow("appear") {DataContext = new PopUpWindowModel()}.Show();
            Hide();
        }

        private void nIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }
    }
}
