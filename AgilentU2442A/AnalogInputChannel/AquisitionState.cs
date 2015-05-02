using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AgilentU2442A
{
    class AquisitionState
    {
        public AquisitionState()
        {
            m_NewDataSetAquiredEvent = new AutoResetEvent(false);
            m_AquisitionStopEvent = new ManualResetEvent(false);
            m_AllEventsArray = new WaitHandle[2] { m_NewDataSetAquiredEvent, m_AquisitionStopEvent };
        }

        public EventWaitHandle AquisitionStopEvent
        {
            get { return m_AquisitionStopEvent; }
        }
        public EventWaitHandle NewDataSetAquiredEvent
        {
            get { return m_NewDataSetAquiredEvent; }
        }

        public WaitHandle[] EventArray
        {
            get { return m_AllEventsArray; }
        }

        private EventWaitHandle m_NewDataSetAquiredEvent;
        private EventWaitHandle m_AquisitionStopEvent;
        private WaitHandle[] m_AllEventsArray;
    }
}
