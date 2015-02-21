using InstrumentAbstractionModel;
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Helper;
using System.Windows.Data;
using System.Collections.ObjectModel;


namespace InstrumentHandlerNamespace
{
    //
    // FOR GOOD SERIALIZATION DEFINE EMPTY CONSTRUCTOR
    //
    [DataContract]
    public sealed partial class InstrumentHandler//:NotifyPropertyChanged
    {
        private static volatile InstrumentHandler m_Handler;
        private static object syncRoot = new object();
        private const string SerializationFileName = "Devices.xml";
        private const string ResourceFilter = "(GPIB)|(USB)?*INSTR";

        private List<IInstrument> m_InstrumentList;
        public List<IInstrument> InstrumentList
        {
            get { return m_InstrumentList; }
        }

        private void InitializeHandler()
        {
            if (m_InstrumentList != null)
                CheckInstrumentsConnectivity();
            else m_InstrumentList = new List<IInstrument>();
            DiscoverInstruments();
            
            
            //DiscoverInstruments();
        }
        private void CheckInstrumentsConnectivity()
        {
            // Check if all instruments is alive;
        }


        // For serialization default constructor necessary
        private InstrumentHandler()
        {
            
            
        }

        ~InstrumentHandler()
        {
            //if(NeedSerialization)
            var dir = Directory.GetCurrentDirectory();
            DataContractSerializer serializer = new DataContractSerializer(typeof(InstrumentHandler));
            
            using (Stream stream = new FileStream(String.Format("{0}\\{1}", dir, SerializationFileName), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.WriteObject(stream, this);
                stream.Close();
            }
           
        }

        public static InstrumentHandler Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (m_Handler == null)
                        m_Handler = DeserializeOrNew();
                }
                return m_Handler;
            }
        }
                
        private static InstrumentHandler DeserializeOrNew()
        {
            InstrumentHandler handler;
            try
            {
                var dir = Directory.GetCurrentDirectory();
                DataContractSerializer serializer = new DataContractSerializer(typeof(InstrumentHandler));
                
                using (Stream stream = new FileStream(String.Format("{0}\\{1}", dir, SerializationFileName), FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    handler = (InstrumentHandler)serializer.ReadObject(stream);
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
        private void RefreshPermissionTable()
        {

        }
        private void DiscoverInstruments()
        {
            // refresh m_InstrumentPermissionTable with new instruments
            var exp1 = ExperimentsRegistry.Instance.ExperimentsList[0];
            var exp2 = ExperimentsRegistry.Instance.ExperimentsList[1];
            
            var instr1 = new SomeInstrument("1", "a1", "123");
            var instr2 = new SomeInstrument("2", "a2", "asf");

            m_InstrumentList.Add(instr1);
            m_InstrumentList.Add(instr2);

            m_InstrumentPermissionTable = new Dictionary<IInstrumentOwner, Dictionary<IInstrument, InstrumentPermission>>();
            m_InstrumentPermissionTable[exp1] = new Dictionary<IInstrument, InstrumentPermission>();
            var dictPerm = m_InstrumentPermissionTable[exp1];
            dictPerm.Add(instr1, new InstrumentPermission());
            dictPerm.Add(instr2, new InstrumentPermission(true));

            CurrentOwner = exp1;
            
            //m_InstrumentPermissionTable
            /*
            try
            {
                var LocalResourceManager = ResourceManager.GetLocalManager();
                var resources = LocalResourceManager.FindResources(ResourceFilter);
                if(resources.Length==0)
                {
                    throw new Exception("No instruments found");
                }
                foreach (var resource in resources)
                {
                    //NationalInstruments.NI4882.Device dev= new NationalInstruments.NI4882.Device(0)  
                    Console.WriteLine("New Instrument");

                    var s = (MessageBasedSession)LocalResourceManager.Open(resource);
                    Console.WriteLine(resource);
                    s.Write("*IDN?");
                    var idn = s.ReadString();
                    Console.WriteLine(idn);
                    s.Dispose();

                    Console.WriteLine("***************\n\r");
                    //NationalInstruments.NI4882.Device dev = new NationalInstruments.NI4882.Device()
                }
            }
            catch (VisaException)
            {
                //throw;
            }
            catch (Exception ex)
            {
                //throw;
            }
            */
        }

        public bool TryGetDevice(string InstrumentName, out IInstrument Instrument, IInstrumentOwner Owner)
        {
            throw new NotImplementedException();
        }

        public bool TryGetDevice<T>(string InstrumentName,out T Instrument, IInstrumentOwner Owner) where T:IInstrument
        {
            throw new NotImplementedException();
        }

        public bool TryGetDevices(string InstrumentNames, out IInstrument[] Instruments, IInstrumentOwner Owner)
        {
            throw new NotImplementedException();
        }
    }
}
