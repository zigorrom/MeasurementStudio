using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSignalAnalyzer
{
    public class TransformEventArgs:EventArgs
    {
        public int AveragesDone { get; private set; }
    }
}
