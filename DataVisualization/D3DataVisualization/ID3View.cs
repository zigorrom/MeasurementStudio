using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataVisualization.D3DataVisualization
{
    public interface ID3View
    {
        void SetScale(GraphScaleType scaleType);

        void AddSeries(IPointDataSource data, string Description);

        void Clear();
    }
}
