using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{

    public class QueuedMeasurementDataExporter<InfoT, DataT> : MeasurementDataExporter<InfoT, DataT>
        where InfoT : struct, IMeasurementInfo
        where DataT : struct
    {
        public QueuedMeasurementDataExporter(string workingDirectory):base(workingDirectory)
        {

        }

        public override void OpenExperiment(string ExperimentName)
        {
            base.OpenExperiment(ExperimentName);

        }

        public override void OpenMeasurement(string MeasurementName)
        {
            base.OpenMeasurement(MeasurementName);
        }

        public override void OpenMeasurement(InfoT measurementInfo)
        {
            base.OpenMeasurement(measurementInfo);
        }

        public override void Close()
        {
            base.Close();
        }
        public override void CloseExperiment()
        {
            base.CloseExperiment();
        }
        public override void CloseMeasurement()
        {
            base.CloseMeasurement();
        }

        public override void WriteMeasurement(DataT data)
        {
            base.WriteMeasurement(data);
        }

        public override void WriteMeasurement(DataT[] data)
        {
            base.WriteMeasurement(data);
        }

        public override void WriteMeasurement(MeasurementData<InfoT, DataT> data)
        {
            base.WriteMeasurement(data);
        }

        public override async Task WriteMeasurementAsync(DataT data)
        {
            await base.WriteMeasurementAsync(data);
        }

        public override async Task WriteMeasurementAsync(DataT[] data)
        {
            await base.WriteMeasurementAsync(data);
        }

        public override async Task WriteMeasurementAsync(MeasurementData<InfoT, DataT> data)
        {
            await base.WriteMeasurementAsync(data);
        }
    }


    public class MeasurementDataExporter<InfoT, DataT>:IDisposable //: IMeasurementDataExporter<InfoT, DataT>
        where InfoT : struct, IMeasurementInfo
        where DataT : struct
    {

        public MeasurementDataExporter(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
            PrepareExportFunction<InfoT>(out _exportInfoFunction);
            PrepareExportFunction<DataT>(out _exportDataFunction);
            PrepareHeader<InfoT>(out _infoHeader);
            PrepareHeader<DataT>(out _dataHeader);
            IsExperimentOpened = false;
            IsMeasurementOpened = false;
        }

        ~MeasurementDataExporter()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            Close();
        }

        public virtual void Close()
        {
            CloseMeasurement();
            CloseExperiment();
        }

        //private string _workingDirectory;
        public string WorkingDirectory
        {
            get;
            set;
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

        private Func<InfoT, string> _exportInfoFunction;
        private Func<DataT, string> _exportDataFunction;

        private string _infoHeader;
        private string _dataHeader;

        private StreamWriter _infoWriter;
        private StreamWriter _dataWriter;

        private string GenerateFilename(string workingDirectory, string name, string postfix, string extension)
        {
            return Path.Combine(workingDirectory, String.Format("{0}_{1}.{2}", name, postfix, extension));
        }
        
        private string GenerateFilename(string workingDirectory,string name, string postfix, string extension, bool UseNextAvailableName)
        {
            string path = GenerateFilename(workingDirectory, name, postfix, extension);
            var counter = 0;
            if(UseNextAvailableName)
                while (!File.Exists(path))
                {
                    path = GenerateFilename(workingDirectory, name, String.Format("{0}_{1}", postfix, counter++), extension);
                }
            return path;
        }

       

        public string ExperimentName { get; private set; }


        private const string TextFileExtension = "txt";
        private const string DataFileExtension = "dat";

        public bool IsExperimentOpened { get; private set; }
        public bool IsMeasurementOpened { get; private set; }


        public virtual void OpenExperiment(string ExperimentName)
        {
            this.ExperimentName = ExperimentName;
            var infofn = GenerateFilename(WorkingDirectory, ExperimentName, String.Empty, TextFileExtension); //String.Concat(WorkingDirectory, "\\", ExperimentName, ".txt");
            _infoWriter = new StreamWriter(new FileStream(infofn, FileMode.Append, FileAccess.Write, FileShare.Read));
            _infoWriter.WriteLine(_infoHeader);
            IsExperimentOpened = true;
        }

        public virtual void CloseExperiment()
        {
            CloseMeasurement();
            if (IsExperimentOpened)
            {
                _infoWriter.Close();
                IsExperimentOpened = false;
            }
        }


        public virtual void OpenMeasurement(string MeasurementName)
        {
            if(!IsExperimentOpened)
                throw new IOException("Experiment was not initialized");

            if (IsMeasurementOpened)
                CloseMeasurement();

            var datafn = GenerateFilename(WorkingDirectory, MeasurementName, String.Empty, DataFileExtension);
            _dataWriter = new StreamWriter(new FileStream(datafn, FileMode.Append, FileAccess.Write, FileShare.Read));
            _dataWriter.WriteLine(_dataHeader);
            IsMeasurementOpened = true;
        }

        public virtual void OpenMeasurement(InfoT measurementInfo)
        {
            OpenMeasurement(measurementInfo.Filename);
            _infoWriter.WriteLine(_exportInfoFunction(measurementInfo));
        }

        public virtual void CloseMeasurement()
        {
            if (IsMeasurementOpened)
            {
                _dataWriter.Close();
                IsMeasurementOpened = false;
            }
        }


        public virtual void WriteInfo(InfoT info)
        {
            if (IsExperimentOpened)
            {
                _infoWriter.WriteLine(_exportInfoFunction(info));
            }
            else
            {
                throw new Exception("Info writer was not initialized.");
            }
        }

        public virtual void WriteMeasurement(MeasurementData<InfoT, DataT> data)
        {
            if (IsExperimentOpened && IsMeasurementOpened)
            {
                foreach (var p in data)
                {
                    _dataWriter.WriteLine(_exportDataFunction(p));
                }
                _infoWriter.WriteLine(_exportInfoFunction(data.Info));
            }
            else
            {
                throw new Exception("Writers were not initialized. Make sure you are calling NewExperiment and NewMeasurement methods before.");
            }
            //_dataWriter.Flush();
        }

        public virtual void WriteMeasurement(DataT[] data)
        {
            if (IsExperimentOpened && IsMeasurementOpened)
            {
                foreach (var p in data)
                {
                    _dataWriter.WriteLine(_exportDataFunction(p));
                }
            }else
            {
                throw new Exception("Writers were not initialized. Make sure you are calling NewExperiment and NewMeasurement methods before.");
            }
        }

        public virtual void WriteMeasurement(DataT data)
        {
            if (IsExperimentOpened && IsMeasurementOpened)
            {
                _dataWriter.WriteLine(_exportDataFunction(data));
            }
            else
            {
                throw new Exception("Writers were not initialized. Make sure you are calling NewExperiment and NewMeasurement methods before.");
            }
        }



        public virtual async Task WriteMeasurementAsync(MeasurementData<InfoT, DataT> data)
        {
            await Task.Factory.StartNew(() => WriteMeasurement(data));
        }

        public virtual async Task WriteMeasurementAsync(DataT[] data)
        {
            await Task.Factory.StartNew(() => WriteMeasurement(data));
        }

        public virtual async Task WriteMeasurementAsync(DataT data)
        {
            await Task.Factory.StartNew(() => WriteMeasurement(data));
        }




       
    }


    
}
