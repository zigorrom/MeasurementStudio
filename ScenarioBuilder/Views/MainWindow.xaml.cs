using ScenarioBuilder.ViewModel;
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

namespace ScenarioBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //ListViewDragDropManager<AvailableExperimentItem> dragMgr;
        //ListViewDragDropManager<IExperimentItem> dragMgr2;
        public MainWindow()
        {
            InitializeComponent();
            //this.dragMgr = new ListViewDragDropManager<AvailableExperimentItem>(this.AvailableExperimentList);
            
        }

        

        private void ScenarioList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
        

       

        

        
        


    }
}
