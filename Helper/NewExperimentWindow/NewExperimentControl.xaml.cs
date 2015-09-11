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

namespace Helper.NewExperimentWindow
{
    /// <summary>
    /// Interaction logic for NewExperimentControl.xaml
    /// </summary>
    public partial class NewExperimentControl : Window
    {
        public NewExperimentControl(string CurrentExperimentName)
        {
            InitializeComponent();
            ExpName.Text = CurrentExperimentName;
        }

        public string ExperimentName
        {
            get { return ExpName.Text; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;

        }
    }
}
