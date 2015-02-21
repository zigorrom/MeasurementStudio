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

        
        private void InitializeHandler()
        {  
            //m_InstrumentsRegistry = new Dictionary<IInstrumentOwner, List<IInstrument>>();
            //m_ViewSource = new CollectionViewSource();
            //var exp1 = new IVExperiment();

            //var lst1 = new List<IInstrument>();
            //var instr = new SomeInstrument("instr1", "1", "12938");
            //instr.InstrumentOwner = exp1;
            //lst1.Add(instr);
            ////lst1.Add(new SomeInstrument("instr2", "2", "34234"));
            //m_InstrumentsRegistry[exp1] = lst1;
            
            //List<object> lst = new List<object>();
            //CollectionViewSource src = new CollectionViewSource();
            //src.Source = lst;
            //var view = src.View;
            //DiscoverInstruments();
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
        
        public void DiscoverInstruments()
        {
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
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }

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
