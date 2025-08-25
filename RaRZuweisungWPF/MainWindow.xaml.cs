using RaRZuweisungWPF.Controller;
using RaRZuweisungWPF.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RaRZuweisungWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TheBigC c = new TheBigC();
            TuiTextBlock.Text=  c.exampleMethod();
        }

        private void Show_Participants_Click(object sender, RoutedEventArgs e)
        {
       
        }

        private void Add_Particpant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Participant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Change_Participant_Window_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Change_Availability_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reset_DataBase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Show_Round_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Create2er_Round_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Create3er_Round_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Change_Round_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}