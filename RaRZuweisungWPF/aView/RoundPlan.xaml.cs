using RaRZuweisungWPF.Controller;
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

namespace RaRZuweisungWPF.aView
{
    /// <summary>
    /// Interaction logic for RoundPlan.xaml
    /// </summary>
    public partial class RoundPlan : Window
    {
        private IFController controller;
        public RoundPlan(IFController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void RowDefinitionsButton_Click(object sender, RoutedEventArgs e)
        {
            bool[] roundplan = new bool[5];
            int i = 0;
            foreach (CheckBox child in RoundStackBox.Children.OfType<CheckBox>())
            {
                roundplan[i] = child.IsChecked == true;
                i++;
            }
            controller.setRoundPlan(roundplan);
            this.Close();
        }
    }
}
