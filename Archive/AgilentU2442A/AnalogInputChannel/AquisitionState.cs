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
            m_ProcessingStopEvent = new ManualResetEvent(false);
            m_AllEventsArray = new WaitHandle[3] { m_NewDataSetAquiredEvent, m_AquisitionStopEvent, m_ProcessingStopEvent };
            m_ProcessingEventArray = new WaitHandle[2] { m_NewDataSetAquiredEvent, m_ProcessingStopEvent };
        }

        public EventWaitHandle AquisitionStopEvent
        {
            get { return m_AquisitionStopEvent; }
        }
        public EventWaitHandle NewDataSetAquiredEvent
        {
            get { return m_NewDataSetAquiredEvent; }
        }

        public EventWaitHandle ProcessingStopEvent
        {
            get { return m_ProcessingStopEvent; }
        }
        
        public WaitHandle[] EventArray
        {
            get { return m_AllEventsArray; }
        }

        public WaitHandle[] ProcessingEventArray
        {
            get { return m_ProcessingEventArray; }
        }

        private EventWaitHandle m_NewDataSetAquiredEvent;
        private EventWaitHandle m_AquisitionStopEvent;
        private EventWaitHandle m_ProcessingStopEvent;
        private WaitHandle[] m_AllEventsArray;
        private WaitHandle[] m_ProcessingEventArray;
    }
}
