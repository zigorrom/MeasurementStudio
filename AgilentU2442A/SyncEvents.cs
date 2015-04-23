using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class SyncEvents
    {
        private EventWaitHandle m_newItemEvent;
        private EventWaitHandle m_exitThreadEvent;
        private WaitHandle[] m_eventArray;

        public SyncEvents()
        {
            m_newItemEvent = new AutoResetEvent(false);
            m_exitThreadEvent = new ManualResetEvent(false);
            m_eventArray = new WaitHandle[2];
            m_eventArray[0] = m_newItemEvent;
            m_eventArray[1] = m_exitThreadEvent;
        }

        public EventWaitHandle NewItemEvent { get { return m_newItemEvent; } }
        public EventWaitHandle ExitThreadEvent { get { return m_exitThreadEvent; } }
        public WaitHandle[] EventArray { get { return m_eventArray; } }
        
        
        
    }
}
