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

        ListViewDragDropManager<IAvailableExperimentItem> dragMgr;
        ListViewDragDropManager<IExperimentItem> dragMgr2;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
           
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.dragMgr = new ListViewDragDropManager<IAvailableExperimentItem>(this.AvailableExperimentList);
            //this.dragMgr2 = new ListViewDragDropManager<IExperimentItem>(this.ScenarioList);
            this.dragMgr.ProcessDrop += dragMgr_ProcessDrop;
            this.AvailableExperimentList.Drop += OnListDrop;
            this.AvailableExperimentList.DragEnter += OnListDragEnter;
            //this.ScenarioList.Drop += OnListDrop;
            //this.ScenarioList.DragEnter += OnListDragEnter;
            
            
        }

        private void OnListDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        void dragMgr_ProcessDrop(object sender, ProcessDropEventArgs<IAvailableExperimentItem> e)
        {
            // This shows how to customize the behavior of a drop.
            // Here we perform a swap, instead of just moving the dropped item.

            int higherIdx = Math.Max(e.OldIndex, e.NewIndex);
            int lowerIdx = Math.Min(e.OldIndex, e.NewIndex);

            if (lowerIdx < 0)
            {
                // The item came from the lower ListView
                // so just insert it.
                e.ItemsSource.Insert(higherIdx, e.DataItem);
            }
            else
            {
                // null values will cause an error when calling Move.
                // It looks like a bug in ObservableCollection to me.
                if (e.ItemsSource[lowerIdx] == null ||
                    e.ItemsSource[higherIdx] == null)
                    return;

                // The item came from the ListView into which
                // it was dropped, so swap it with the item
                // at the target index.
                e.ItemsSource.Move(lowerIdx, higherIdx);
                e.ItemsSource.Move(higherIdx - 1, lowerIdx);
            }

            // Set this to 'Move' so that the OnListViewDrop knows to 
            // remove the item from the other ListView.
            e.Effects = DragDropEffects.Move;
        }

        private void OnListDrop(object sender, DragEventArgs e)
        {
            if (e.Effects == DragDropEffects.None)
                return;

            //var experimentItem = e.Data.GetData(typeof(IAvailableExperimentItem)) as IAvailableExperimentItem;
            //if (sender == this.AvailableExperimentList)
            //{
            //    if (this.dragMgr.IsDragInProgress)
            //        return;

            //    // An item was dragged from the bottom ListView into the top ListView
            //    // so remove that item from the bottom ListView.
            //    //(this.listView2.ItemsSource as ObservableCollection<Task>).Remove(task);
            //}
            //else
            //{
            //    if (this.dragMgr2.IsDragInProgress)
            //        return;

            //    // An item was dragged from the top ListView into the bottom ListView
            //    // so remove that item from the top ListView.
            //    (this.listView.ItemsSource as ObservableCollection<Task>).Remove(task);
            //}
        }

        
        


    }
}
