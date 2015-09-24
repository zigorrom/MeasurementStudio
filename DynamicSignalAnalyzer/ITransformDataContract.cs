using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSignalAnalyzer
{

    public interface IDataTransformContract
    {
        event EventHandler<TransformEventArgs>  NewDataHandled;
        event EventHandler AveragesNumberReached;
    }
}
