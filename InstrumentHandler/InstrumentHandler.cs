using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{
    public sealed class InstrumentHandler
    {
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


    }
}
