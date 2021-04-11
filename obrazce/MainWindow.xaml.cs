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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace obrazce
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
 
        }


        private void trojuhelnik_button(object sender, EventArgs e)
        {
            contentControl.Content = new ctverec("trojuhelnik");
        }

        private void ctverec_button(object sender, EventArgs e)
        {
            contentControl.Content = new ctverec("ctverec");
        }

        private void kruh_button(object sender, EventArgs e)
        {
            contentControl.Content = new ctverec("kruh");
        }

        private void obdelnik_button(object sender, EventArgs e)
        {
            contentControl.Content = new ctverec("obdelnik");
        }
    }
}
