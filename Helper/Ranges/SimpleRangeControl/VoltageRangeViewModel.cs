using Helper.Ranges.DoubleRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.SimpleRangeControl
{
    [Serializable]
    public class VoltageRangeViewModel:RangeViewModel
    {
        public VoltageRangeViewModel(string RangeName,DoubleUnitValueDependencyObject start, DoubleUnitValueDependencyObject end, DoubleUnitValueDependencyObject step):base(RangeName, start, end, step)
        {

        }

        public VoltageRangeViewModel(DoubleUnitValueDependencyObject start, DoubleUnitValueDependencyObject end, DoubleUnitValueDependencyObject step):base(start,end,step)
        {

        }

        public VoltageRangeViewModel()
            : base(new Voltage(), new Voltage(), new Voltage())
        {

        }

    }
}
