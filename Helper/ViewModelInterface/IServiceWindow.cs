using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helper.ViewModelInterface
{
    public interface IServiceWindow
    {
        void ShowMessage(string message);
        SynchronizationContext CurrentSynchronizationContext { get; }
        IServiceWindow CurrentInstance { get; }
    }
}
