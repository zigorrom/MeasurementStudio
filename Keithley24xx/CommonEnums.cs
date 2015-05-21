using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley24xx
{
    public enum FunctionEnum
    {
        None,
        CURRent,
        VOLTage,
        RESistance
    }

    public enum SourceCleAutoEnum
    {
        On,
        Off
    }

    public enum SenseFuncConcurrentEnum
    {
        On,
        Off
    }

    public enum TextStatusEnum
    {
        On,
        Off
    }

}
