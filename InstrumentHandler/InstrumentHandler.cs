using InstrumentAbstractionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{
    public sealed class InstrumentHandler
    {
        #region SingletoneStuff
        private static volatile InstrumentHandler m_Handler;
        private static object syncRoot = new object();
        private InstrumentHandler() { }
        

        public static InstrumentHandler Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (m_Handler == null)
                        m_Handler = new InstrumentHandler();
                }
                return m_Handler;
            }
        }

        #endregion


        private void DiscoverInstruments()
        {

        }

        private bool TryGetDevice(AvailableInstrumentsEmuneration InstrumentName, out IInstrument Instrument)
        {
            throw new NotImplementedException();
        }
                

    }
}
