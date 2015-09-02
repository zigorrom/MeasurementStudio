using Helper.Ranges.DoubleRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.RangeHandlers
{
    public class BackAndForthRangeHandler:AbstractDoubleRangeHandler
    {

        protected BackAndForthRangeHandler(string HandlerName, bool ZeroCrossing, DoubleRangeBase range)
            : base(HandlerName, true, ZeroCrossing,range)
        { }

        protected BackAndForthRangeHandler(string HandlerName, bool ZeroCrossing)
            : base(HandlerName, true, ZeroCrossing)
        { }

        public BackAndForthRangeHandler(DoubleRangeBase range)
            : base(BackAndForthRangeHandler, true, false, range)
        { }
        public BackAndForthRangeHandler()
            : base(BackAndForthRangeHandler, true, false)
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
                for (val = MinVal; (val <= MaxVal)&&(count<Range.PointsCount); val += Range.Step,count++, progressCount++)
                {
                    OnProgressChanged(progressCount / maxCount, null);
                    yield return val;
                }
                count = 0;
                for (val = MaxVal; (val >= MinVal) && (count < Range.PointsCount); val -= Range.Step, count++, progressCount++)
                {
                    OnProgressChanged(progressCount / maxCount, null);
                    yield return val;
                }
                OnCyclePassed(i+1);
            }
        }
    }
}
