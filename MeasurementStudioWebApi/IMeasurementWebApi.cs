using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeasurementStudioWebApi
{
    public interface IMeasurementWebApi
    {
        void ShowMessage(string message);
        SynchronizationContext CurrentSynchronizationContext { get; }
        //IMeasurementWebApi CurrentInstance { get; }
        


        string[] GetAvailablePages();

        bool SwitchToPage(string PageName);
    }
}
