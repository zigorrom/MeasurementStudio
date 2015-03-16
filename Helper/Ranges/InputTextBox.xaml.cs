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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Helper.Ranges
{
    /// <summary>
    /// Interaction logic for InputTextBox.xaml
    /// </summary>
    public partial class InputTextBox : UserControl
    {
        public InputTextBox()
        {
            InitializeComponent();
        }
        private string m_ValueName;
        private string m_Value;
        public string ValueName {
            get { return m_ValueName; }
            set { m_ValueName = value; }
        }
        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
    }
}
