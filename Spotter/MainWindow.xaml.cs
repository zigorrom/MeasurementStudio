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

namespace Spotter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
          
            InitializeComponent();
            var s = new Voltage();
            var e = new Voltage();
            var st = new Voltage();
            var a = new RangeViewModel(s, e, st);
            range.DataContext = a;
            //var duv = new DoubleUnitValue("V");
            //DataContext = duv;
            ////var dnv = new DoubleNumericValue(new Volt());
            //this.a.DataContext = dnv;
            //foreach (var item in COMDevice.GetPortNames())
            //{
            //    MessageBox.Show(item);
            //    try
            //    {
            //        COMDevice dev = new COMDevice(item);
            //    }
            //    catch (Exception e)
            //    {

            //        throw;
            //    }
            //}
            
        }
    }
}
