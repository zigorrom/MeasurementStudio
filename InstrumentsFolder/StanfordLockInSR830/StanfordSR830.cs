using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devices;
using System.Globalization;
namespace StanfordLockInSR830
{
    public class StanfordSR830:GPIB_Device
    {
        public StanfordSR830(byte _PrimaryAddress, byte _SecondaryAddress, byte _BoardNumber) : base(_PrimaryAddress, _SecondaryAddress, _BoardNumber) { }
        public StanfordSR830(string IDN, int DeviceOrder = 0, byte _BoardNumber = 0) : base(IDN, DeviceOrder, _BoardNumber) { }
        public bool ReadSignal(out double Signal)
        {
            {

                string result;
                try
                {
                    result = this.RequestQuery("OUTR?1");
                }
                catch
                {
                    result = "0";
                    isAlive = false;
                }
                NumberFormatInfo a = new NumberFormatInfo();
                a.NumberDecimalSeparator = ".";
                a.NumberGroupSeparator = "";
                try
                {
                    Signal = Convert.ToDouble(result, a);
                }

                catch
                {
                    Signal = 0;
                }
                return isAlive;

            }
        }
    }
}
