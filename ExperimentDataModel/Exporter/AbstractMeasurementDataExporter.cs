using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        protected Func<InfoT, string> _exportInfoFunction;
        private void PrepareInfoHeader()
        {
            var t = typeof(InfoT);
            var properties = t.GetProperties();
            var propNames = properties.Where(x => x.GetCustomAttributes(typeof(DataPropertyAttribute), false).Length > 0).Select(x => "t."+x.Name).ToArray();

            const string codeFormat = @"
                    using System;
                    using _NS_;
                    namespace ExportFunctions
                    {  
                        public class ExportToString
                        {
                            public static string ExportType(_TYPE_ t)
                            {
                                return String.Format(_STRFORM_,_STRPAR_);
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
            var stringFormat = "\""+String.Join("\t", arr) + "\"";
            var FinalCode = codeFormat.Replace("_NS_", nameSpace).Replace("_TYPE_", typeName).Replace("_STRFORM_", stringFormat).Replace("_STRPAR_", stringNames); //String.Format(codeFormat, nameSpace, typeName, stringFormat, stringNames);

            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add(t.Assembly.Location);
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;
            var result = provider.CompileAssemblyFromSource(parameters, FinalCode);
            if (result.Errors.HasErrors)
                throw new Exception("Compiler error: " + String.Join(";", result.Errors));

            var type = result.CompiledAssembly.GetType("ExportFunctions.ExportToString");
            var method = type.GetMethod("ExportType");
            _exportInfoFunction = (Func<InfoT, string>)Delegate.CreateDelegate(typeof(Func<InfoT, string>), method);
        }




        protected virtual void PrepareMeasurementHeader()
        {

        }
        

        public abstract void Write(MeasurementData<InfoT, DataT> measurement);


        public abstract void WriteDelayed(MeasurementData<InfoT, DataT> measurement);
        
    }
}
