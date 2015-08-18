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


        private string _workingDirectory;
        public string WorkingDirectory
        {
            get { return _workingDirectory; }
        }

        public void NewExperiment(string WorkingFolder)
        {
            _workingDirectory = WorkingFolder;
        }

        private Func<InfoT, string> _exportInfoFunction;
        private void GenerateInfoExportFunction(Type t)
        {
            var propNames = t.GetProperties().Where(x => x.GetCustomAttributes(typeof(DataPropertyAttribute), false).Length > 0).Select(x => "t."+x.Name).ToArray();
            const string codeFormat = @"
                    using System;
                    using _NS_;
                    namespace ExportFunctions
                    {  
                        public class ExportToString
                        {
                            public static string ExportType(_TYPE_ t)
                            {
                                return String.Format(_SRTFORM_,_SRTPAR_);
                            }
                        }
                    }
                    ";
            var nameSpace = t.Namespace;
            var typeName = t.Name;
            
            var stringNames = String.Join(", ",propNames);
            
            var arr = new string[propNames.Length];
            for (int i = 0; i < propNames.Length; i++)
                arr[i] = String.Format("{{{0}}}", i);

            var stringFormat = String.Join("\t", arr);// + "\"";

            var FinalCode = codeFormat.Replace("_NS_", nameSpace).Replace("_TYPE_", typeName).Replace("_STRFORM_", stringFormat).Replace("_STRPAR_", stringNames); //String.Format(codeFormat, nameSpace, typeName, stringFormat, stringNames);

        }



        protected virtual void PrepareInfoHeader()
        {
            var it = typeof(InfoT);
            GenerateInfoExportFunction(it);


            //var prop =it.GetProperties(); 
            
            //var m = prop.Where(x => x.GetCustomAttributes(typeof(DataPropertyAttribute), false).Length > 0).Select(
            //    x => x.GetMethod
            //    ).ToArray();

            //var a = Activator.CreateInstance<InfoT>();
            

            

            //for (int i = 0; i < m.Length; i++)
            //{
            //    var r = m[i].Invoke(a,null);
            //    r.ToString();
            //}
            //var properties = it.GetProperties();
            
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
