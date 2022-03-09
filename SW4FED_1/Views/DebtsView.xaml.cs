using SW4FED_1.Models;
using SW4FED_1.ViewModels;
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

namespace SW4FED_1.Views
{
    /// <summary>
    /// Interaction logic for DebtsView.xaml
    /// </summary>
    public partial class DebtsView : Window
    {
        public DebtsView()
        {
            InitializeComponent();
        }

        private void AddValueBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as DebtsViewModel;
            if (vm.IsValid)
                DialogResult = true;
            else
                MessageBox.Show("Enter values for debit", "Missing data");
        }
    }
}
