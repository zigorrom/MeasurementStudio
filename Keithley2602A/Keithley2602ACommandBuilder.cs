using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley2602A
{
    public class Keithley2602ACommandBuilder:AbstractCommandBuilder
    {
        public string BeeperEnable()
        {
            return "beeper.enable = 1 ";
        }
    }
}
