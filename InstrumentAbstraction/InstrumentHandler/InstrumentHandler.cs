﻿

using Instruments;
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{
    //
    // FOR GOOD SERIALIZATION DEFINE EMPTY CONSTRUCTOR
    //

    [Serializable]
    public sealed class InstrumentHandler//:NotifyPropertyChanged
    {
        private static volatile InstrumentHandler _handler;

        private static object syncRoot = new object();
        
        private const string SerializationFileName = "Devices.seri";
        private const string ResourceFilter = "(GPIB)|(USB)|(COM)?*INSTR";//"?*INSTR";
        private const string AllResourceFilter = "?*";

        [ImportMany]
        private Lazy<IInstrumentFactory, IDictionary<string, object>>[] InstrumentFactoriesPlugins { get; set; }
        // For serialization default constructor necessary
        private InstrumentHandler()
        {


        }

        ~InstrumentHandler()
        {
            //if(NeedSerialization)
            //var dir = Directory.GetCurrentDirectory();
            //var binFormatter = new BinaryFormatter();
            //using (Stream stream = new FileStream(String.Format("{0}\\{1}", dir, SerializationFileName), FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    binFormatter.Serialize(stream, this);
            //    stream.Close();
            //}

        }

        public static InstrumentHandler Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (_handler == null)
                        _handler = DeserializeOrNew();
                }
                return _handler;
            }
        }
        
        
        private void InitializeHandler()
        {
            

            InitializeProperties();

            //InitPlugins();
            
            DiscoverInstruments();
            
            CheckInstrumentsConnectivity();

            
        }

        private void InitializeProperties()
        {
            Instruments = new ObservableCollection<IInstrument>();
            Resources = new ObservableCollection<IInstrumentResourceItem>();
        }

        private static InstrumentHandler DeserializeOrNew()
        {
           var ihandler = new InstrumentHandler();
            ihandler.InitializeHandler();
            return ihandler;
        }

        private void InitPlugins()
        {
            try
            {
                var aggregateCatalog = new AggregateCatalog();
                var directory = string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                                .Split('\\').Reverse().Skip(3).Reverse().Aggregate((a, b) => a + "\\" + b)
                                , "\\", "Instrument\\Components");

                ///
                ///ADD HERE SECURITY KEY CHECK
                /// 
                ///

                var directoryCatalog = new DirectoryCatalog(directory, "*.dll");

                var asmCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                aggregateCatalog.Catalogs.Add(directoryCatalog);
                aggregateCatalog.Catalogs.Add(asmCatalog);

                var container = new CompositionContainer(aggregateCatalog);
                container.ComposeParts(this);

                foreach (var i in InstrumentFactoriesPlugins)
                {
                    var a = i.Value;
                    var instr = a.CreateInstrument("", "", "");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObservableCollection<IInstrument> Instruments { get; private set; }

        public ObservableCollection<IInstrumentResourceItem> Resources { get; private set; }


        private void DiscoverInstruments()
        {
            ///
            /// CREATE ATTRIBUTES THAT ALLOWS TO DETERMINE WHICH CLASS CAN BE SUITABLE TO USE INSTRUMENT
            ///
            try
            {
                var LocalResourceManager = ResourceManager.GetLocalManager();
                var resources = LocalResourceManager.FindResources(AllResourceFilter);
                if (resources.Length == 0)
                {
                    throw new Exception("No instruments found");
                }

                foreach (var resource in resources)
                {
                    var idn = String.Empty;
                    try
                    {
                        var s = (MessageBasedSession)LocalResourceManager.Open(resource);
                        idn = s.Query("*IDN?");
                    }
                    catch (Exception ex1)
                    {
                    }
                    Resources.Add(new InstrumentResourceItem(resource, idn));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CheckInstrumentsConnectivity()
        {
           

        }
        

        public bool TryGetDevice(string InstrumentName, out IInstrument Instrument, IInstrumentOwner Owner)
        {
            throw new NotImplementedException();
        }

        public bool TryGetDevice<T>(string InstrumentName, out T Instrument, IInstrumentOwner Owner) where T : IInstrument
        {
            throw new NotImplementedException();
        }

        public bool TryGetDevices(string InstrumentNames, out IInstrument[] Instruments, IInstrumentOwner Owner)
        {
            throw new NotImplementedException();
        }

    }
}
