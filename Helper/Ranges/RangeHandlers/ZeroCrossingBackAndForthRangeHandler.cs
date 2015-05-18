using Helper.Ranges.DoubleRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.RangeHandlers
{
    public class ZeroCrossingBackAndForthRangeHandler:BackAndForthRangeHandler
    {
        public ZeroCrossingBackAndForthRangeHandler(DoubleRangeBase range):base(range)
        {
            Initialize(true, true);
            //BackAndForth = true;
            //StartFromZero = true;
        }

        public ZeroCrossingBackAndForthRangeHandler():base()
        {
            Initialize(true, true);
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
                    yield return val;

                for (; val >= 0; val -= Range.Step)
                    yield return val;

                for (val = 0; val >= MinVal; val -= Range.Step)
                    yield return val;

                for (; val <= 0; val += Range.Step)
                    yield return val;


            }
        }

    }
}
