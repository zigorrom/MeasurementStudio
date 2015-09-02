using Helper.Ranges.DoubleRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.RangeHandlers
{
    public class ZeroCrossRangeHandler:NormalDoubleRangeHandler
    {
        public ZeroCrossRangeHandler(DoubleRangeBase range)
            : base(ZeroCrossingRangeHandler, true, range)
        { }

        public ZeroCrossRangeHandler()
            : base(ZeroCrossingRangeHandler, true)
        { }

        private IEnumerator<double> CurrentEnum()
        {
            double val, MinVal, MaxVal;
            if (Range.End > Range.Start)
            {
                MinVal = Range.Start;
                MaxVal = Range.End;
            }
            else
            {
                MinVal = Range.End;
                MaxVal = Range.Start;
            }

            var maxCount = RepeatCounts * Range.PointsCount;
            var progressCount = 0;


            for (int i = 0; i <= RepeatCounts; i++)
            {
                

                if (Range.Step == 0)
                {
                    yield return 0;
                    break;
                }
                else
                {
                    for (val = 0; val <= MaxVal; val += Range.Step, progressCount++)
                    {
                        OnProgressChanged(progressCount / maxCount, null);
                        yield return val;
                    }
                    for (val = 0; val >= MinVal; val -= Range.Step, progressCount++)
                    {
                        OnProgressChanged(progressCount / maxCount, null);
                        yield return val;
                    }
                }

            }

        }

        public override IEnumerator<double> GetEnumerator()
        {
            if (AssertRangeNull())
                return null;
            if (!Range.CrossesZero)
                return base.GetEnumerator();
            else
                return CurrentEnum();
        }
    }
}
