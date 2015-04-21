using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AnalogInputChannels:AnalogInputChannel, IList<AnalogInputChannel>//,IAnalogInputChannel // make IList but AnalogInputChannel
    {
        public AnalogInputChannels(AnalogInputChannel AI1, AnalogInputChannel AI2):base(AI1, AI2)
        {
            //subscribe on events
            m_channelsList = new List<AnalogInputChannel>();
        }
        private List<AnalogInputChannel> m_channelsList;


        public double AnalogRead()
        {
            var channelList = this.Select(x => x.NativeChannelName).ToArray();

            throw new NotImplementedException();
        }

        public void StartAcquisition()
        {
            throw new NotImplementedException();
        }

        public void StopAcquisition()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(AnalogInputChannel item)
        {
            return m_channelsList.IndexOf(item);
        }

        public void Insert(int index, AnalogInputChannel item)
        {
            m_channelsList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public AnalogInputChannel this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(AnalogInputChannel item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(AnalogInputChannel item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(AnalogInputChannel[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(AnalogInputChannel item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<AnalogInputChannel> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
