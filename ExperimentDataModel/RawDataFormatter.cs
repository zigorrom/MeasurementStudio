using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public class RawDataFormatter:AbstractDataFormatter
    {
        public RawDataFormatter()
        {

        }
        public override object Deserialize(System.IO.Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(System.IO.Stream serializationStream, object graph)
        {
            MemberInfo[] infos = FormatterServices.GetSerializableMembers(graph.GetType(), Context);
            var obj = FormatterServices.GetObjectData(graph, infos);
            using (StreamWriter sw = new StreamWriter(serializationStream))
            {
                for (int i = 0; i < infos.Length; i++)
                {
                    sw.Write("{0}={1}",infos[i].Name,obj[i].ToString());
                }

            }
        }
    }
}
