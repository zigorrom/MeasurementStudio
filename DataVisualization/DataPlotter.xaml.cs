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
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace DataVisualization
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DataPlotter : UserControl
    {
        public DataPlotter()
        {
            InitializeComponent();
            
        }

        private IDataViewModel m_viewModel;

        public void SetDataContext(IDataViewModel ViewModel)
        {
            m_viewModel = ViewModel;
            DataContext = m_viewModel;
        }

        public void AddLineGraph(IPointDataSource Source, Color LineColor)
        {
            throw new NotImplementedException();
            //var line = ChartControl.AddLineGraph();
        }



       

        
        //public object DataContext
        //{
        //    get { return null; }
        //}

    }
}
