using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.ViewModels
{
    public class OutputIVViewModel:IVMainViewModel
    {
        public OutputIVViewModel():base(IVCharacteristicTypeEnum.Output)
        {
            Visualization.HorizontalAxisTitle = "Drain - Source Voltage, Vds";
            Visualization.VerticalAxisTitle = "Drain Current, Id";
        }
    }
}
