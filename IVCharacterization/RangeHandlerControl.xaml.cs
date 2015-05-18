using System;
using System.Collections.Generic;
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

namespace IVCharacterization
{
    /// <summary>
    /// Interaction logic for RangeHandlerControl.xaml
    /// </summary>
    public partial class RangeHandlerControl : UserControl
    {
        public RangeHandlerControl()
        {
            this.InitializeComponent();
        }

        public TextBox RepeatCountTextBox
        {
            get { return CountsRepeat; }
        }

        public ComboBox RangeHandlerComboBox
        {

            get
            {
                return HandlerComboBox;
            }

        }
    }
}