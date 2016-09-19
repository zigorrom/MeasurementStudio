using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A_IVIdriver
{
    [Flags]
    public enum ChannelEnum
    {
        None=0,
        AI_CH101=1,// = 101,
        AI_CH102=2,
        AI_CH103=4,
        AI_CH104=8,
        AO_CH201=16, //= 201,
        AO_CH202=32,
        DIG_CH501=64,//=501,
        DIG_CH502=128,
        DIG_CH503=256,
        DIG_CH504=512,
    }

}
