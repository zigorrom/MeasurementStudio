using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public class DataWriter<DataT>:StreamWriter where DataT:IFormattable
    {
        public  DataWriter(string Path):base(Path)
        {

        }

        public void WriteDataLine(DataT data)
        {

        }

        public void WriteEnumeration(IEnumerable<DataT> enumeration)
        {

        }
    }
}
