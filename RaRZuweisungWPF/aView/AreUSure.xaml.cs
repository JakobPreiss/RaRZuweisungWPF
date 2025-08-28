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
    /// Interaction logic for AreUSure.xaml
    /// </summary>
    public partial class AreUSure : Window
    {
        private IFController controller;
        public AreUSure(IFController c)
        {
            InitializeComponent();
            controller = c;
        }

        private void Affirming_Click(object sender, RoutedEventArgs e)
        {
            controller.resetDatabase();
            Close();
        }

        private void Canceling_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
