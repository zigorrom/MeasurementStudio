using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalDataModelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IVDataModel model = new IVDataModel();
            RawDataFormatter fmt = new RawDataFormatter();
            using(Stream s = new FileStream(Directory.GetCurrentDirectory()+"\\text.txt",FileMode.Create,FileAccess.Write,FileShare.None))
            {
                fmt.Serialize(s, model);
            }
        }
    }
}
