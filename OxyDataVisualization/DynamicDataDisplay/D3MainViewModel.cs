using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualization.DynamicDataDisplay
{
    public class D3MainViewModel:AbstractDataVisualizationViewModel
    {
        private GraphScaleType _scale;
        public override GraphScaleType ScaleType
        {
            get
            {
                return _scale;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddSeries(System.Collections.IEnumerable points)
        {
            throw new NotImplementedException();
        }

        public override void InvalidatePlot()
        {
            throw new NotImplementedException();
        }

        public override void ClearChart()
        {
            throw new NotImplementedException();
        }
    }
}
