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
            Visualization.HorizontalAxisTitle = "Drain - Source Voltage, V_{DS}(V)";
            Visualization.VerticalAxisTitle = "Drain Current, I_{D}(A)";
            Visualization.Title = "Output I-V Characterization";
            //Visualization.ScaleType = DataVisualization.GraphScaleType.LinLin;
        }


    }
}
