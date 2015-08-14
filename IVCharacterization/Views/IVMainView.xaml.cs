using Helper.Ranges;
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

namespace IVCharacterization
{
    /// <summary>
    /// Interaction logic for IVMainView.xaml
    /// </summary>
    public partial class IVMainView : UserControl
    {
        public IVMainView()
        {
            //model1 = new IVMainViewModel(IVCharacteristicTypeEnum.Output);
            //model2 = new IVMainViewModel(IVCharacteristicTypeEnum.Transfer);
            InitializeComponent();
            ControlButtons.StartButtonPressed += (o, e) =>
            {
                ProcessProgress.Visibility = System.Windows.Visibility.Visible;
                SetGlobalEnabled(false);
            };
            ControlButtons.StopButtonPressed += (o, e) => 
            { 
                ProcessProgress.Visibility = System.Windows.Visibility.Hidden;
                SetGlobalEnabled(true);
            };
            
        }

        private void SetGlobalEnabled(bool IsEnabled)
        {
            var context = DataContext as IVMainViewModel;
            if (context == null)
                return;
            context.GlobalIsEnabled = IsEnabled;
        }

        //public IVMainViewModel model1 { get; private set; }

        //public IVMainViewModel model2 { get; private set; }
        //public event EventHandler<IVCharacteristicTypeEnum> DataContextChangeDemand;

        //private void OnDataContextChangeDemand(object sender, IVCharacteristicTypeEnum e)
        //{
        //    var handler = DataContextChangeDemand;
        //    if (handler != null)
        //        handler(sender, e);
        //}

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var index = ((ComboBox)sender).SelectedIndex;
        //    switch (index)
        //    {
        //        case 0: OnDataContextChangeDemand(this, IVCharacteristicTypeEnum.Transfer); break;//DataContext = model2; break;
        //        case 1: OnDataContextChangeDemand(this, IVCharacteristicTypeEnum.Output); break;//DataContext = model1; break;
        //    }
        //}

        
    }
}
