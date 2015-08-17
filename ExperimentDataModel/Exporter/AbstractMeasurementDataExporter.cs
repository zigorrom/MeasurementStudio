using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public abstract class AbstractMeasurementDataExporter<InfoT,DataT>:IMeasurementDataExporter<InfoT,DataT>
        where InfoT:struct
        where DataT:struct
    {
        public AbstractMeasurementDataExporter()
        {
            _infoHeader = String.Empty;
            _measurementHeader = String.Empty;
            PrepareInfoHeader();
            PrepareMeasurementHeader();
        }

        protected string _infoHeader;
        protected string _measurementHeader;

        protected Delegate _f;

        private string _workingDirectory;
        public string WorkingDirectory
        {
            get { return _workingDirectory; }
        }

        public void NewExperiment(string WorkingFolder)
        {
            _workingDirectory = WorkingFolder;
        }

        protected virtual void PrepareInfoHeader()
        {
            var it = typeof(InfoT);

            var m = it.GetProperties().Where(x => x.GetCustomAttributes(typeof(DataPropertyAttribute), false).Length > 0).Select(
                x =>
                
                    
                     Delegate.CreateDelegate(typeof(Func<object>), x.GetMethod)
                    
                ).ToArray();
            
            //var properties = it.GetProperties();
            _f = Delegate.Combine(m);
            //for(int i = 0; i < properties.Length; i++)
            //{
            //    _f = Delegate.Combine(   properties[i].GetMethod;
            //}
        }


        protected virtual void PrepareMeasurementHeader()
        {

        }
        

        public abstract void Write(MeasurementData<InfoT, DataT> measurement);


        public abstract void WriteDelayed(MeasurementData<InfoT, DataT> measurement);
        
    }
}
