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
using System.ComponentModel;

namespace E4Um
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon nIcon = new NotifyIcon();
        ContextMenuStrip cMenuStrip = new ContextMenuStrip();
        public MainWindow()
        {
            InitializeComponent();

            // Initialising tray icon
            nIcon.Icon = new Icon(@"../../Resources/uk.ico");
            nIcon.Visible = true;
            nIcon.Text = "E4U";
            nIcon.DoubleClick += new EventHandler(nIcon_DoubleClick);
            //// /Initialising tray icon

            //// Initialising context menu strip
            cMenuStrip.Items.Add("Открыть окно настроек");
            cMenuStrip.Items.Add("-");
            cMenuStrip.Items.Add("Открыть главное окно");
            cMenuStrip.Items.Add("Выход");
            cMenuStrip.Items[0].Click += new EventHandler(cMenuStripItems0_Click);
            cMenuStrip.Items[3].Click += new EventHandler(cMenuStripItems2_Click);
            nIcon.ContextMenuStrip = cMenuStrip;
            // /Initialising context menu strip
        }

        public void Open(IWindowService openWindowService)
        {
            openWindowService.CreatePopUpWindow("appear");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Open(new OpenWindowService());
            Hide();
        }

        private void nIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void cMenuStripItems0_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void cMenuStripItems2_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            nIcon.Visible = false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            nIcon.Visible = false;
        }
    }
}
