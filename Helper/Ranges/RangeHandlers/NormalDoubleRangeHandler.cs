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
            for (int count = 1; count < RepeatCounts; count++)
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
