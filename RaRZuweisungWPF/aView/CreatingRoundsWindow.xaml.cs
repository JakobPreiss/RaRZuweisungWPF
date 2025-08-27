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
using static System.Net.Mime.MediaTypeNames;

namespace RaRZuweisungWPF.aView
{
    /// <summary>
    /// Interaction logic for CreatingRoundsWindow.xaml
    /// </summary>
    public partial class CreatingRoundsWindow : Window
    {
        public CreatingRoundsWindow()
        { 
            InitializeComponent();
            CreatingInfos.Content = "Creating RaRs";

        }
        public void changeText(string text)
        {
            CreatingInfos.Content = text;
            Closing.Visibility = Visibility.Visible;

        }

        private void Closing_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
