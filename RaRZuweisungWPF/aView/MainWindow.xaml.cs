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

namespace RaRZuweisungWPF.aView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IFController controller;
        public MainWindow()
        {
            InitializeComponent();
            //TheBigC c = new TheBigC();
            //TuiTextBlock.Text=  c.exampleMethod();
            controller = new TheBigC(this);

            controller.resetDatabase();

            var testParticipants = new List<Participant>
            {
                new Participant("Hannah", true, true, true, true, true, true),
                new Participant("Elli", true, true, true, true, true, true),
                new Participant("Paul", true, true, true, true, true, true),
                new Participant("Jake", true, true, true, true, true, true),
                new Participant("Petra", true, true, true, true, true, true),
                new Participant("Laurin", true, true, true, true, true, true),
                new Participant("Gregor", true, true, true, true, true, true),
                new Participant("Sina", true, true, true, true, true, true),
                new Participant("Johanna", true, true, true, true, true, true),
                new Participant("Gloria", true, true, true, true, true, true),
                new Participant("Fabienne", true, true, true, true, true, true),
                new Participant("Eline", true, true, true, true, true, true),

                //new Participant("Konstantin", false, true, true, true, true, true),
                new Participant("Kirsten", false, true, true, true, true, true),
                new Participant("Janis", false, true, true, true, true, true),
                new Participant("Alex", false, true, true, true, true, true),
                new Participant("Pascal", false, true, true, true, true, true),
                new Participant("Phillipe", false, true, true, true, true, true),
                new Participant("Ari", false, true, true, true, true, true),
                new Participant("Paulina", false, true, true, true, true, true),
                new Participant("jana", false, true, true, true, true, true),
                new Participant("Anja", false, true, true, true, true, true),
                new Participant("Klaus", false, true, true, true, true, true),
                //new Participant("Viktor", false, true, true, true, true, true)
            };

            foreach (Participant participant in testParticipants)
            {
                controller.AddParticipant(participant);
            }

            /*RaR3 testRaR3 = new RaR3(testParticipants[0], testParticipants[1], testParticipants[2], 1);
            List<RaR3> test3List = new List<RaR3>();
            test3List.Add(testRaR3);
            DataGridRaR3.ItemsSource = test3List;
            //DataGridRaR3.Visibility = Visibility.Visible;

            RaR2 testRaR2 = new RaR2(testParticipants[1], testParticipants[0], 1);
            List<RaR2> test2List = new List<RaR2>();
            test2List.Add(testRaR2);
            DataGridRaR2.ItemsSource = test2List;
            //DataGridRaR2.Visibility = Visibility.Visible;
            */


            Show_Participants_Click(this, new RoutedEventArgs());
        }

        public void Show_Participants_Click(object sender, RoutedEventArgs e)
        {
            DataGridRaR2.Visibility = Visibility.Hidden;
            DataGridRaR3.Visibility = Visibility.Hidden;
            DataGridParticipants.ItemsSource = controller.GetParticipants();
            DataGridParticipants.Visibility = Visibility.Visible;
        }

        private void Add_Particpant_Click(object sender, RoutedEventArgs e)
        {
            var addParticipant = new AddParticipantWindow(controller, this);
            addParticipant.Show();
        }

        private void Remove_Participant_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridParticipants.SelectedItem is Participant)
            {
                controller.RemoveParticipant((Participant) DataGridParticipants.SelectedItem);
            } else
            {
                displayErrorMessage("Konnte participant nicht casten oder war kein participant");
            }
            Show_Participants_Click(this, new RoutedEventArgs());
        }

        private void Change_Participant_Window_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Change_Availability_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reset_DataBase_Click(object sender, RoutedEventArgs e)
        {
            controller.resetDatabase();
        }

        private void Show_Round_Click(object sender, RoutedEventArgs e)
        {
            int round;
            if (int.TryParse(Round_Indicator.Text, out round))  
            {
                if(controller.checkIfRoundIs2(round))
                {
                    DataGridRaR2.ItemsSource = controller.getRaR2Round(round);
                    DataGridParticipants.Visibility = Visibility.Hidden;
                    DataGridRaR3.Visibility = Visibility.Hidden;
                    //tracking.Visibility = Visibility.Hidden;
                    //scrollV.Visibility = Visibility.Hidden;
                    DataGridRaR2.Visibility = Visibility.Visible;
                }
                else
                {
                    DataGridRaR3.ItemsSource = controller.getRaR3Round(round);
                    DataGridParticipants.Visibility = Visibility.Hidden;
                    DataGridRaR2.Visibility= Visibility.Hidden;
                    //tracking.Visibility = Visibility.Hidden;
                    //scrollV.Visibility = Visibility.Hidden;
                    DataGridRaR3.Visibility = Visibility.Visible;
                }
            } else
            {
                MessageBox.Show("Enter the Round first (upper right corner)");
            }
            
        }

        private void SetRoundPlan(object sender, RoutedEventArgs e)
        {
            RoundPlan roundplan = new RoundPlan(controller);
            roundplan.Show();
        }

        public void displayErrorMessage(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Error: ");
            sb.Append(message);
            MessageBox.Show(sb.ToString());
        }

        private void Change_Round_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Create_Rounds_Click(object sender, RoutedEventArgs e)
        {
            CreatingRoundsWindow crw = new CreatingRoundsWindow();
            crw.Show();
            crw.changeText("Creating Rounds");
            controller.createRounds();
            crw.changeText("Rounds created");
            DataGridParticipants.Visibility = Visibility.Hidden;
        }

        public void printText(string text)
        {
            //tracking.Text += text + "\n";
        }

    }
}