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
            //TheBigC c = new TheBigC();
            //TuiTextBlock.Text=  c.exampleMethod();

            var testParticipants = new List<Participant>
            {
                new Participant("Hannah", true, true, true, true, false, true),
                new Participant("Elli", false, false, true, true, false, true),
                new Participant("Konstantin", true, false, false, true, false, true),
            };

            DataGridParticipants.ItemsSource = testParticipants;
            //DataGridParticipants.Visibility = Visibility.Visible;

            RaR3 testRaR3 = new RaR3(testParticipants[0], testParticipants[1], testParticipants[2], 1);
            List<RaR3> test3List = new List<RaR3>();
            test3List.Add(testRaR3);
            DataGridRaR3.ItemsSource = test3List;
            //DataGridRaR3.Visibility = Visibility.Visible;

            RaR2 testRaR2 = new RaR2(testParticipants[1], testParticipants[0], 1);
            List<RaR2> test2List = new List<RaR2>();
            test2List.Add(testRaR2);
            DataGridRaR2.ItemsSource = test2List;
            DataGridRaR2.Visibility = Visibility.Visible;

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