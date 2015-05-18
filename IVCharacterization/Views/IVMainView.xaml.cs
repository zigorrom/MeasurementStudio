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
            model1 = new IVMainViewModel(IVCharacteristicTypeEnum.Output);
            model2 = new IVMainViewModel(IVCharacteristicTypeEnum.Transfer);
            InitializeComponent();
            //DataContextChanged += IVMainView_DataContextChanged;
            
            //model1.ChangeIVCharacterizationViewModel += ChangeIVCharacterizationViewModel;
            //model2.ChangeIVCharacterizationViewModel += ChangeIVCharacterizationViewModel;
            //DataContext = model1;
            
        }

        IVMainViewModel model1;
        IVMainViewModel model2;


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = ((ComboBox)sender).SelectedIndex;
            switch (index)
            {
                case 0: DataContext = model2; break;
                case 1: DataContext = model1; break;
            }
        }

        
    }
}
