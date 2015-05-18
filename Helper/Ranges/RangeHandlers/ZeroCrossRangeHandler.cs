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
        public ZeroCrossRangeHandler(DoubleRangeBase range):base(range)
        {
            Initialize(false, true);
            //BackAndForth = false;
            //StartFromZero = true;
        }

        public ZeroCrossRangeHandler():base()
        {
            Initialize(false, true);
        }

        private IEnumerator<double> CurrentEnum()
        {
            for (int i = 1; i < RepeatCounts; i++)
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
                for (val = 0; val <= MaxVal; val += Range.Step)
                {
                    yield return val;
                }
                for (val = 0; val >= MinVal; val -= Range.Step)
                {
                    yield return val;
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
