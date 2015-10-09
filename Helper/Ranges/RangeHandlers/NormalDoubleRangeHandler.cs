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
            double val, step;//, MinVal, MaxVal;

            var maxCount = RepeatCounts * Range.PointsCount;
            var progressCount = 0;
            Func<double, double, bool> comparator;
            
            if (Range.End > Range.Start)
            {
                //MinVal = Range.Start;
                //MaxVal = Range.End;
                //step = Range.Step;
                step = Range.Step;
                comparator = new Func<double, double, bool>((a, b) => a <= b);
            }
            else
            {
                //MinVal = Range.End;
                //MaxVal = Range.Start;
                step = -Range.Step;
                comparator = new Func<double, double, bool>((a, b) => a <= b);
            }
            for (int i = 0; i < RepeatCounts; i++)
            {

                var count = 0;
                for (val = Range.Start; comparator(val,Range.End) && (count < Range.PointsCount); val += step, count++, progressCount++)
                {
                    OnProgressChanged(progressCount / maxCount, null);
                    yield return val;
                }
            }
            

            
            
        }

        public override int TotalPoints
        {
            get { return RepeatCounts * Range.PointsCount; }
        }
    }
}
