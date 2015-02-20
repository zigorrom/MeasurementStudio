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


namespace InstrumentHandlerNamespace
{
    //
    // FOR GOOD SERIALIZATION DEFINE EMPTY CONSTRUCTOR
    //
    [DataContract]
    public sealed class InstrumentHandler
    {

        private static volatile InstrumentHandler m_Handler;
        private static object syncRoot = new object();
        [DataMember]
        private string Name = "Vasya";

        [DataMember]
        private Dictionary<IInstrumentOwner, List<IInstrument>> list;

        private InstrumentHandler()
        {
            list = new Dictionary<IInstrumentOwner, List<IInstrument>>();
            
            //InitializeHandler();
        }

        ~InstrumentHandler()
        {
            //if(NeedSerialization)
            var dir = Directory.GetCurrentDirectory();
            DataContractSerializer serializer = new DataContractSerializer(typeof(InstrumentHandler));
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(InstrumentHandler));
            using (Stream stream = new FileStream(String.Format("{0}\\{1}", dir, SerializationFileName), FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.WriteObject(stream, this);
                stream.Close();
            }
            //Serialization here.
        }


        public static InstrumentHandler Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (m_Handler == null)
                        //deserialize or create new object
                        m_Handler = DeserializeOrNew();//new InstrumentHandler();
                }
                return m_Handler;
            }
        }


        private const string SerializationFileName = "Devices.xml";
        private static InstrumentHandler DeserializeOrNew()
        {
            InstrumentHandler handler;
            try
            {
                var dir = Directory.GetCurrentDirectory();
                DataContractSerializer serializer = new DataContractSerializer(typeof(InstrumentHandler));
                
                using (Stream stream = new FileStream(String.Format("{0}\\{1}", dir, SerializationFileName), FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    handler = (InstrumentHandler)serializer.ReadObject(stream);//xmlSerializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                handler = new InstrumentHandler();
            }
            if (null == handler)
                handler = new InstrumentHandler();
            return handler;
        }
        //private Dictionary<IInstrumentOwner, List<IInstrument>> m_InstrumentDictionary;


        //private void AddInstrument(IInstrument instrument)
        //{

        //}

        private void InitializeHandler()
        {
            DiscoverInstruments();
        }
        //
        //Write here device addresses statically.
        //

        private const string ResourceFilter = "?*";//"GPIB|USB?*INSTR";
        private void DiscoverInstruments()
        {
            try
            {
                var LocalResourceManager = ResourceManager.GetLocalManager();
                var resources = LocalResourceManager.FindResources(ResourceFilter);
                foreach (var resource in resources)
                {
                    //NationalInstruments.NI4882.Device dev= new NationalInstruments.NI4882.Device(0)   
                    var s = (MessageBasedSession)LocalResourceManager.Open(resource);
                    s.Write("*IDN?");
                    var idn = s.ReadString();
                    //NationalInstruments.NI4882.Device dev = new NationalInstruments.NI4882.Device()
                }
            }
            catch (VisaException)
            {

                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool TryGetDevice(IInstrumentOwner Owner, AvailableInstrumentsEmuneration InstrumentName, out IInstrument Instrument)
        {
            throw new NotImplementedException();
        }

        public bool TryGetDevices(IInstrumentOwner Owner, AvailableInstrumentsEmuneration InstrumentNames, out IInstrument[] Instruments)
        {
            throw new NotImplementedException();
        }
    }
}
