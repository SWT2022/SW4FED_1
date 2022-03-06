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
    /// Interaction logic for DebtorsView.xaml
    /// </summary>
    public partial class DebtorsView : Window
    {
        public DebtorsView()
        {
            InitializeComponent();
        }

        private void Ok_BtnCllick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as DebtorsViewModel;
            if (vm.IsValid)
                DialogResult = true;
            else
                MessageBox.Show("Enter values for Name and Initial debt", "Missing data");
        }
    }
}
