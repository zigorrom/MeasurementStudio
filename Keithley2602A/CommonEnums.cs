using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley2602A
{
    public enum Keithley2602AChannelsEnum { ChannelA, ChannelB }
    public enum Keithley2602AChannelStatuEnum { Channel_ON, Channel_OFF }
    public enum Keithley2602ASenseEnum { SENSE_LOCAL, SENSE_REMOTE }
    public enum Keithley2601ASourceModeEnum { Voltage, Current }
    public enum Keithley2601AMeasureModeEnum { Voltage, Current, Resistance, Power }
    public enum Keithley2601ALimitModeEnum { Voltage, Current }
}
