using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.RangeHandlers
{
    public class BackAndForthRangeHandler:AbstractDoubleRangeHandler
    {
        public BackAndForthRangeHandler(DoubleRangeBase range):base(range)
        {

        }
        public override IEnumerator<double> GetEnumerator()
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
                for (val = MinVal; val <= MaxVal; val += Range.Step)
                {
                    yield return val;
                }
                for (; val >= MinVal; val -= Range.Step)
                {
                    yield return val;
                }
            }
        }
    }
}
