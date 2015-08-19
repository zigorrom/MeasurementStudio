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
            PrepareExportFunction<InfoT>(out _exportInfoFunction);
            PrepareExportFunction<DataT>(out _exportDataFunction);
           
        }

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
        private Func<DataT, string> _exportDataFunction;

        private void PrepareExportFunction<T>(out Func<T,string> exportFunction)
        {
            exportFunction = null;
            var t = typeof(T);
            var properties = t.GetProperties();
            var propNames = properties
                .Where(x => x.GetCustomAttributes(typeof(DataPropertyAttribute), false).Length > 0)
                .Select(x => "t."+x.Name)
                .ToArray();

            const string NameSpacePlaceholder = "_NS_";
            const string ParamTypePlaceholder = "_TYPE_";
            const string StringFormatPlaceholder = "_STRFORM_";
            const string ParamsPlaceholder = "_STRPAR_";
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
                    }";

            var nameSpace = t.Namespace;
            var typeName = t.Name;
            var stringNames = String.Join(", ",propNames);
            var arr = new string[propNames.Length];
            for (int i = 0; i < propNames.Length; i++)
                arr[i] = String.Format("{{{0}}}", i);
            var stringFormat = "\""+String.Join("\t", arr) + "\"";
            var FinalCode = codeFormat
                .Replace(NameSpacePlaceholder, nameSpace)
                .Replace(ParamTypePlaceholder, typeName)
                .Replace(StringFormatPlaceholder, stringFormat)
                .Replace(ParamsPlaceholder, stringNames); //String.Format(codeFormat, nameSpace, typeName, stringFormat, stringNames);

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
            exportFunction = (Func<T, string>)Delegate.CreateDelegate(typeof(Func<T, string>), method);
        }

        private void PrepareHeader<T>(out string Header)
        {
            Header = String.Empty;
            var t = typeof(T);
            var properties = t.GetProperties();

        }
        

        public abstract void Write(MeasurementData<InfoT, DataT> measurement);


        public abstract void WriteDelayed(MeasurementData<InfoT, DataT> measurement);
        
    }
}
