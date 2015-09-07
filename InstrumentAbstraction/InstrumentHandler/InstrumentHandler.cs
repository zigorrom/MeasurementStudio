
using Instruments;
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private const string SerializationFileName = "Devices.ser";
        private const string ResourceFilter = "(GPIB)|(USB)|(COM)?*INSTR";

        //private Dictionary<string,IInstrument> m_InstrumentList;
        //private List<IInstrument> m_Instruments;
        private ObservableCollection<IInstrument> _instruments;
        public ObservableCollection<IInstrument> Instruments
        {
            get { return _instruments; }
        }
        private void InitializeHandler()
        {

            if (_instruments == null)
                _instruments = new ObservableCollection<IInstrument>();

            DiscoverInstruments();
            CheckInstrumentsConnectivity();

            //InitializeViewModel();
        }


        private void CheckInstrumentsConnectivity()
        {
            foreach (var instr in _instruments)
            {
                //if(instr.Is)
            }

        }


        // For serialization default constructor necessary
        private InstrumentHandler()
        {


        }

        ~InstrumentHandler()
        {
            //if(NeedSerialization)
            var dir = Directory.GetCurrentDirectory();
            var binFormatter = new BinaryFormatter();
            using (Stream stream = new FileStream(String.Format("{0}\\{1}", dir, SerializationFileName), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormatter.Serialize(stream, this);
                stream.Close();
            }

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

        private static InstrumentHandler DeserializeOrNew()
        {
            InstrumentHandler handler;
            try
            {
                var dir = Directory.GetCurrentDirectory();
                var binFormatter = new BinaryFormatter();
                using (Stream stream = new FileStream(String.Format("{0}\\{1}", dir, SerializationFileName), FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    handler = (InstrumentHandler)binFormatter.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                handler = new InstrumentHandler();
            }
            if (null == handler)
                handler = new InstrumentHandler();

            handler.InitializeHandler();
            return handler;
        }

        

        private void DiscoverInstruments()
        {
            ///
            /// CREATE ATTRIBUTES THAT ALLOWS TO DETERMINE WHICH CLASS CAN BE SUITABLE TO USE INSTRUMENT
            ///
            try
            {
                var appAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                var InstrumentType = typeof(IInstrument);
                //var types = //new IEnumerable<Type>();//appAssemblies
                

                //var assembly = Assembly.GetAssembly(typeof(IInstrument));
                //var IInstrumentType = typeof(IInstrument);
                //var types = assembly.GetTypes()
                //    .Where(x =>
                //    {
                //        if (x.IsAbstract || x.IsInterface)
                //            return false;
                //        if (IInstrumentType.IsAssignableFrom(x))
                //            return true;
                //        return false;
                //    })
                //    .Select(x =>
                //    {
                //        return new { Key = (InstrumentAttribute)x.GetCustomAttribute(typeof(InstrumentAttribute)), Value = x };//InstrumentInstance };
                //    });


                //var LocalResourceManager = ResourceManager.GetLocalManager();
                //var resources = LocalResourceManager.FindResources(ResourceFilter);
                //if (resources.Length == 0)
                //{
                //    throw new Exception("No instruments found");
                //}
                //foreach (var resource in resources)
                //{
                //    var s = (MessageBasedSession)LocalResourceManager.Open(resource);
                //    s.Write("*IDN?");
                //    var idn = s.ReadString();
                //    s.Dispose();
                //    foreach (var item in types)
                //    {
                //        if (!item.Key.FitsToIDN(idn))
                //            continue;
                //        var instr = (IInstrument)Activator.CreateInstance(item.Value, String.Format("Manufacturer:{0},Model:{1}", item.Key.Manufacturer, item.Key.Model), "", resource);
                //        m_Instruments.Add(instr);
                //    }
                //}
            }
            catch (VisaException)
            {
                //throw;
            }
            catch (Exception ex)
            {
                //throw;
            }

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
