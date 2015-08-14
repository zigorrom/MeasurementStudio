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
using Helper.Ranges.DoubleRange;
using Helper.Ranges.Units;
using Helper.Ranges.SimpleRangeControl;
using IVCharacterization;
using IVCharacterization.ViewModels;
using IVCharacterization.Experiments;

namespace Spotter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OutputCurveMeasurement mea;
        public MainWindow()
        {
          
            InitializeComponent();
            mea = new OutputCurveMeasurement();
            this.Content = mea.Control;
            
            
        }
    }
}
