using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSignalAnalyzer
{
    public class DynamicSignalAnalyzer
    {
        


        public IDataTransformContract AddFrequencyRange(DiscretizationParameters rangePameters)
        {

            // add to list

            return new TransformProvider(rangePameters);
        }


        private class TransformProvider:IDataTransformContract
        {
            public event EventHandler<TransformEventArgs> NewDataHandled;
            public event EventHandler AveragesNumberReached;
            
            private DiscretizationParameters _params;
            
            public TransformProvider(DiscretizationParameters parameters)
            {
                _params = parameters;
                //var a// = new ConcurrentExclusiveSchedulerPair(TaskScheduler.Default);
                
                
            }



        }

    }
}
