using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InstrumentHandler
{
    public enum AvailableInstrumentsEmuneration
    {
        DigitalAnalyzer_HP35670A = 1,
        Multimeter_HP34401A = 2,
        Multimeter_Keithley196 = 4,
        SMU_Keithley2400 = 8,
        SMU_Keithley2430 = 16,
        LockInAmplifier_SR830 = 32,
        DAQ_AgilentU2542A = 64
    }
}
