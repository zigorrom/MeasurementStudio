using Helper.Ranges.DoubleRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.RangeHandlers
{
    public class NormalDoubleRangeHandler:AbstractDoubleRangeHandler
    {
        protected NormalDoubleRangeHandler(string HandlerName, bool ZeroCrossing, DoubleRangeBase range)
            : base(HandlerName, false, ZeroCrossing, range)
        { }

        protected NormalDoubleRangeHandler(string HandlerName, bool ZeroCrossing)
            : base(HandlerName, false, ZeroCrossing)
        { }

        public NormalDoubleRangeHandler(DoubleRangeBase range)
            : base(NormalRangeHandler, false, false, range)
        { }

        public NormalDoubleRangeHandler()
            : base(NormalRangeHandler, false, false)
        { }

        public override IEnumerator<double> GetEnumerator()
        {
            if (AssertRangeNull())
                return null;
            return CurrentEnum();
        }

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

            for (int i = 0; i < RepeatCounts; i++)
            {

                var count = 0;
                for (val = MinVal; (val <= MaxVal) && (count < Range.PointsCount); val += Range.Step, count++, progressCount++)
                {
                    OnProgressChanged(progressCount / maxCount, null);
                    yield return val;
                }
            }
        }
    }
}
