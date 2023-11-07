using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Host
{
    public partial class MainWindow : Window
    {
        MainController mainControler;
        public MainWindow()
        {
            InitializeComponent();
            mainControler = new MainController(this);       
        }

        public void UpdateLabel(string text)
        {
            textPanel.Text = text;
            image.Source = new BitmapImage(new Uri("/Images/" + text + ".jpg", UriKind.Relative));
        }

        public void SetName(string text)
        {
            Title = "Host: " + text;
        }
    }
}
