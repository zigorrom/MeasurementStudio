using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel.Exporter.StreamExporter
{
    class ContinuousStreamMeasurementDataExporter<InfoT, DataT>: IObserver<DataT[]>
        where InfoT : struct, IMeasurementInfo
        where DataT : struct
    {
        private StreamWriter _infoWriter;
        private StreamWriter _dataWriter;

        private string _workingDirectory;

        public string WorkingDirectory
        {
            get { return _workingDirectory; }
            set { _workingDirectory = value; }
        }

        private Func<InfoT, string> _exportInfoFunction;
        private Func<DataT, string> _exportDataFunction;

        private string _infoHeader;
        private string _dataHeader;



        public ContinuousStreamMeasurementDataExporter(string WorkingDirectory)
        {
            Initialize(WorkingDirectory);
        }

        public void Initialize(string workingDirectory)
        {
            this.WorkingDirectory = WorkingDirectory;
            PrepareExportFunction<InfoT>(out _exportInfoFunction);
            PrepareExportFunction<DataT>(out _exportDataFunction);
            PrepareHeader<InfoT>(out _infoHeader);
            PrepareHeader<DataT>(out _dataHeader);
        }


        private void PrepareExportFunction<T>(out Func<T, string> exportFunction)
        {
            exportFunction = null;
            var t = typeof(T);
            var properties = t.GetProperties();
            if (properties.Length < 1)
                throw new ArgumentException("Seems none of type properties were marked with DataPropertyAttribute");

            var propNames = properties
                .Where(x => x.GetCustomAttributes(typeof(DataPropertyAttribute), false).Length > 0)
                .OrderByDescending(x => x.GetCustomAttribute<DataPropertyAttribute>(false).PropertyOrderPriority)
                .Select(x => "t." + x.Name)
                .ToArray();

            const string NameSpacePlaceholder = "_NS_";
            const string ParamTypePlaceholder = "_TYPE_";
            const string StringFormatPlaceholder = "_STRFORM_";
            const string ParamsPlaceholder = "_STRPAR_";
            const string CulturePlaceholder = "_CULTURE_";
            const string codeFormat = @"
                    using System;
                    using System.Globalization;
                    using _NS_;
                    namespace ExportFunctions
                    {  
                        public class ExportToString
                        {
                            public static string ExportType(_TYPE_ t)
                            {
                                return String.Format(new CultureInfo(_CULTURE_),_STRFORM_,_STRPAR_);
                            }
                        }
                    }";
            var culture = "\"en-US\"";
            var nameSpace = t.Namespace;
            var typeName = t.Name;
            var stringNames = String.Join(", ", propNames);
            var arr = new string[propNames.Length];
            for (int i = 0; i < propNames.Length; i++)
                arr[i] = String.Format("{{{0}}}", i);
            var stringFormat = "\"" + String.Join("\t", arr) + "\"";
            var FinalCode = codeFormat
                .Replace(CulturePlaceholder, culture)
                .Replace(NameSpacePlaceholder, nameSpace)
                .Replace(ParamTypePlaceholder, typeName)
                .Replace(StringFormatPlaceholder, stringFormat)
                .Replace(ParamsPlaceholder, stringNames); //String.Format(codeFormat, nameSpace, typeName, stringFormat, stringNames);


            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters();

            var interfaces = t.GetInterfaces();
            parameters.ReferencedAssemblies.Add(t.Assembly.Location);
            parameters.ReferencedAssemblies.Add("System.Globalization.dll");
            foreach (var i in interfaces)
            {
                parameters.ReferencedAssemblies.Add(i.Assembly.Location);
            }
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
            if (properties.Length < 1)
                throw new ArgumentException("Seems none of type properties were marked with DataPropertyAttribute");
            var attrType = typeof(DataPropertyAttribute);
            var attributes = properties
                .Where(x => x.GetCustomAttributes(attrType, false).Length == 1)
                .Select(x => (DataPropertyAttribute)x.GetCustomAttribute(attrType, false))
                .OrderByDescending(x => x.PropertyOrderPriority)
                ;
            //.ToArray<DataPropertyAttribute>();

            var propertyNameRow = String.Join("\t", attributes.Select(x => x.PropertyName));
            var propertyUnitsRow = String.Join("\t", attributes.Select(x => x.PropertyUnits));
            var propertyCommentsRow = String.Join("\t", attributes.Select(x => x.PropertyComments));
            Header = String.Join("\r\n", propertyNameRow, propertyUnitsRow, propertyCommentsRow);
        }

        public void StartDataListening(MeasurementData<InfoT,DataT> measurementData)
        {
            throw new NotImplementedException();
        }

        public void  StopDataListening()
        {

        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(DataT[] value)
        {
            throw new NotImplementedException();
        }
    }
}
