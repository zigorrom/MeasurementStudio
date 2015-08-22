using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVisualization.D3DataVisualization
{
    public interface ID3View
    {
        void SetScale(GraphScaleType scaleType);

        void AddSeries(IEnumerable data);
    }
}
