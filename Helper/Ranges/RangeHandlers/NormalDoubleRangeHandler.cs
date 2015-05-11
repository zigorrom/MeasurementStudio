using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.RangeHandlers
{
    public class NormalDoubleRangeHandler:AbstractDoubleRangeHandler
    {
        public NormalDoubleRangeHandler(DoubleRangeBase range):base(range)
        {
            BackAndForth = false;
            StartFromZero = false;
        }

        public override IEnumerator<double> GetEnumerator()
        {
            for (int count = 1;count < RepeatCounts;count++ )
            { 
                //throw new NotImplementedException();
                for (double i = Range.Start; i <= Range.End; i += Range.Step)
                {
                    //OnProgressChanged((int)((i-Range.Start) / Range.RangeWidth) * 100, i);
                    yield return i;
                }
            }
        }
    }
}
