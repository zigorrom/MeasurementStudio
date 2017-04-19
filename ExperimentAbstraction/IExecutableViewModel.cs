using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExperimentAbstraction
{
    public interface IExecutableViewModel
    {
        IExecutable Executable { get; }
    }
}
