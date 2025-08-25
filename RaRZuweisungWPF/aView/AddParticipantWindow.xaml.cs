using RaRZuweisungWPF.aView;
using RaRZuweisungWPF.Controller;
using RaRZuweisungWPF.Model;
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

namespace RaRZuweisungWPF
{
    /// <summary>
    /// Interaction logic for AddParticipantWindow.xaml
    /// </summary>
    public partial class AddParticipantWindow : Window
    {
        private IFController controller;
        private MainWindow mainWindow;
        public AddParticipantWindow(IFController controller, MainWindow mw)
        {
            InitializeComponent();
            this.controller = controller;
            this.mainWindow = mw;
        }

        private void AddParticipant_Click(object sender, RoutedEventArgs e)
        {
            string? name = NameGiver.Text;
            if(name == null)
            {
                MessageBox.Show("Please Enter a Name");
            }
            bool isOld = OldCheckBox.IsChecked == true;
            Dictionary<int, bool> availabilityDict = new Dictionary<int, bool>();
            int i = 1;
            foreach (CheckBox child in StackPanelCheckBoxes.Children.OfType<CheckBox>())
            {
                availabilityDict.Add(i, child.IsChecked == true);
                i++;
            }

            controller.AddParticipant(new Participant(name, isOld, availabilityDict[1], availabilityDict[2], availabilityDict[3], availabilityDict[4], availabilityDict[5]));
            mainWindow.Show_Participants_Click(this, new RoutedEventArgs());
            this.Close();
        }
    }
}
