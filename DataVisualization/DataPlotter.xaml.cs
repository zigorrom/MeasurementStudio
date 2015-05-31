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

        
        private IVisualizationViewModel m_viewModel;

        public void SetDataContext(IVisualizationViewModel ViewModel)
        {
            m_viewModel = ViewModel;
            DataContext = m_viewModel;
        }

        /// <summary>
        /// </summary>
        /// <param name="Source">ObservableDataSource</param>
        /// <param name="LineColor"></param>
        public void AddLineGraph(IPointDataSource Source, Color LineColor, double thickness,string LineDescription)
        {
            var line = ChartControl.AddLineGraph(Source, LineColor,thickness,LineDescription);
            //line.Description =

            Binding bind = new Binding("LineThickness");
            bind.Source = m_viewModel.LineThickness;
            bind.Mode = BindingMode.TwoWay;
            bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(line, Line.StrokeThicknessProperty, bind);
            //line.SetBinding(Line.StrokeThicknessProperty, bind);
        }

        public void ClearPlots()
        {
            ChartControl.Children.RemoveAll(typeof(LineGraph));
        }
       
    }
}
