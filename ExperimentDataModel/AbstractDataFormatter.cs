using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    
    public abstract class AbstractDataFormatter : IFormatter
    {
        public AbstractDataFormatter()
        {
            m_context = new StreamingContext(StreamingContextStates.All);
            //m_binder =
            //m_surrogateSelector =
        }


        private SerializationBinder m_binder;
        public SerializationBinder Binder
        {
            get
            {
                return m_binder;
            }
            set
            {
                m_binder = value;
            }
        }
        private StreamingContext m_context;
        public StreamingContext Context
        {
            get
            {
                return m_context;
            }
            set
            {
                m_context = value;
            }
        }

        private ISurrogateSelector m_surrogateSelector;
        public ISurrogateSelector SurrogateSelector
        {
            get
            {
                return m_surrogateSelector;
            }
            set
            {
                m_surrogateSelector = value;
            }
        }

        public abstract object Deserialize(System.IO.Stream serializationStream);


        public abstract void Serialize(System.IO.Stream serializationStream, object graph);



    }
}

