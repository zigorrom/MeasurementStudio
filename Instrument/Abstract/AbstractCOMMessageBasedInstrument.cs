using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    public abstract class AbstractCOMMessageBasedInstrument:IInstrument,IMessageBasedInstrument,IDisposable
    {
        public AbstractCOMMessageBasedInstrument(string Name, string Alias, string COMResource)
        {
            this.Name = Name;
            this.Alias = Alias;
            this.ResourceName = COMResource;
            this.InstrumentOwner = null;
            this.State = InstrumentState.Idle;
        }

        //private string _name;
        public string Name
        {
            get;
            private set;
        }

        public string Alias
        {
            get;
            private set;
        }

        public string ResourceName
        {
            get;
            private set;
        }

        public IInstrumentOwner InstrumentOwner
        {
            get;
            set;

        }

        public InstrumentState State
        {
            get;
            set;
        }

        public bool IsAlive(bool SendIDN)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public abstract void DetectInstrument(object data);

        public bool Equals(IInstrument other)
        {
            throw new NotImplementedException();
        }

        public bool InitializeDevice()
        {
            throw new NotImplementedException();
        }

        public bool SendCommand(string Command)
        {
            throw new NotImplementedException();
        }

        public string GetResponce()
        {
            throw new NotImplementedException();
        }

        public string Query(string Command)
        {
            throw new NotImplementedException();
        }

        public bool CheckErrors()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
