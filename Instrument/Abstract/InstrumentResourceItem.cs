using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Instruments
{
    public sealed class InstrumentResourceItem : IInstrumentResourceItem
    {
        private string _resource;

        public string Resource
        {
            get { return _resource; }
            private set { _resource = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _alias;

        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        private string  _idn;

        public string  IDN
        {
            get { return _idn; }
            private set { _idn = value; }
        }

        public InstrumentResourceItem(string Resource, string IDN)
        {
            this.Resource = Resource;
            this.IDN = IDN;
            this.Name = String.Empty;
            this.Alias = String.Empty;
        }

        public override int GetHashCode()
        {
            return String.Concat(Resource, IDN).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is IInstrumentResourceItem)
                return Equals((IInstrumentResourceItem)obj);
            return false;

        }

        public bool Equals(IInstrumentResourceItem other)
        {
            if (other.Resource == Resource)
                if (other.IDN == IDN)
                    return true;
            return false;
        }
    }
}
