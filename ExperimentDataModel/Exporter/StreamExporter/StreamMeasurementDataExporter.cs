﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;

namespace ExperimentDataModel
{
    public class StreamMeasurementDataExporter<InfoT, DataT> : IMeasurementDataExporter<InfoT, DataT>
        where InfoT : struct, IMeasurementInfo
        where DataT : struct
    {
        public StreamMeasurementDataExporter(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
            PrepareExportFunction<InfoT>(out _exportInfoFunction);
            PrepareExportFunction<DataT>(out _exportDataFunction);
            PrepareHeader<InfoT>(out _infoHeader);
            PrepareHeader<DataT>(out _dataHeader);
        }
        ~StreamMeasurementDataExporter()
        {
            Dispose();
        }

        //private string _workingDirectory;
        public string WorkingDirectory
        {
            get;
            set;
        }

        public void NewExperiment(string experimentName)
        {
            ExperimentName = experimentName;
            
        }
       
        private Func<InfoT, string> _exportInfoFunction;
        private Func<DataT, string> _exportDataFunction;

        private string _infoHeader;
        private string _dataHeader;

        //private StreamWriter _InfoWriter;
        public string ExperimentName { get; private set; }

        private void PrepareExportFunction<T>(out Func<T, string> exportFunction)
        {
            exportFunction = null;
            var t = typeof(T);
            var properties = t.GetProperties();
            var propNames = properties
                .Where(x => x.GetCustomAttributes(typeof(DataPropertyAttribute), false).Length > 0)
                .OrderByDescending(x => x.GetCustomAttribute<DataPropertyAttribute>(false).PropertyOrderPriority)
                .Select(x => "t." + x.Name)
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
            var stringNames = String.Join(", ", propNames);
            var arr = new string[propNames.Length];
            for (int i = 0; i < propNames.Length; i++)
                arr[i] = String.Format("{{{0}}}", i);
            var stringFormat = "\"" + String.Join("\t", arr) + "\"";
            var FinalCode = codeFormat
                .Replace(NameSpacePlaceholder, nameSpace)
                .Replace(ParamTypePlaceholder, typeName)
                .Replace(StringFormatPlaceholder, stringFormat)
                .Replace(ParamsPlaceholder, stringNames); //String.Format(codeFormat, nameSpace, typeName, stringFormat, stringNames);


            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters();
            
            var interfaces = t.GetInterfaces();
            parameters.ReferencedAssemblies.Add(t.Assembly.Location);
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
            var attrType = typeof(DataPropertyAttribute);
            var attributes = properties
                .Where(x => x.GetCustomAttributes(attrType, false).Length == 1)
                .Select(x => (DataPropertyAttribute)x.GetCustomAttribute(attrType, false))
                .OrderByDescending(x=>x.PropertyOrderPriority)
                ;
            //.ToArray<DataPropertyAttribute>();

            var propertyNameRow = String.Join("\t", attributes.Select(x => x.PropertyName));
            var propertyUnitsRow = String.Join("\t", attributes.Select(x => x.PropertyUnits));
            var propertyCommentsRow = String.Join("\t", attributes.Select(x => x.PropertyComments));
            Header = String.Join("\r\n", propertyNameRow, propertyUnitsRow, propertyCommentsRow);
        }

        public void Write(MeasurementData<InfoT, DataT> measurement)
        {
            var infofn = String.Concat(WorkingDirectory, "\\", ExperimentName, ".txt");
            var datafn = String.Concat(WorkingDirectory, "\\", measurement.Info.Filename, ".txt");
            if (File.Exists(datafn))
                throw new Exception("Such data file already exists");

            var WriteInfoHeader = true;
            if (File.Exists(infofn))
                WriteInfoHeader = false;
            using (StreamWriter InfoSW = new StreamWriter(new FileStream(infofn,FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.Read)))
            {
                if (WriteInfoHeader)
                    InfoSW.WriteLine(_infoHeader);
                
                InfoSW.WriteLine(_exportInfoFunction(measurement.Info));
                
                
                using (StreamWriter DataSW = new StreamWriter(datafn))
                {
                    DataSW.WriteLine(_dataHeader);
                    foreach (var d in measurement)
                    {
                        DataSW.WriteLine(_exportDataFunction(d)); 
                    }
                }
            }
        }

        public async Task  WriteAsync(MeasurementData<InfoT, DataT> measurement)
        {
            await Task.Factory.StartNew(()=>Write(measurement));
        }

        public void Dispose()
        {
            //if (_InfoWriter != null)
            //    _InfoWriter.Dispose();
        }
    }
}
