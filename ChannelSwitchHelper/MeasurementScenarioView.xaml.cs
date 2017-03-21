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
using WPF.JoshSmith.ServiceProviders.UI;

namespace ChannelSwitchHelper
{
    /// <summary>
    /// Interaction logic for MeasurementScenarioView.xaml
    /// </summary>
    public partial class MeasurementScenarioView:UserControl
    {
        private ListViewDragDropManager<MeasurementChannelController> dragMgr;

        public MeasurementScenarioView()
        {
            InitializeComponent();
            this.dragMgr = new ListViewDragDropManager<MeasurementChannelController>(this.MeasurementChannelView);
        }

      
    }
}
