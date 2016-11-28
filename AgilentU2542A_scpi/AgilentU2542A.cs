using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2542A_scpi
{
    public class AgilentU2542A : AbstractMessageBasedInstrument
    {

        public AgilentU2542A(string Name,string Alias, string Resource):base(Name,Alias,Resource)
        {
            Initialize();
        }

        private AgilentU2542ACommandBuilder _commandBuilder;

        private void Initialize()
        {
            
        }

        public string IDN
        {
            get
            {
                throw new NotImplementedException();
            }
            
        }

        public override void DetectInstrument(object data)
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
