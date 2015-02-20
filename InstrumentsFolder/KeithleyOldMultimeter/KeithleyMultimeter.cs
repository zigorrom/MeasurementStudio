using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devices;
using System.Globalization;
namespace KeithleyOldMultimeter
{
    public class KeithleyMultimeter:GPIB_Device
    {
         public KeithleyMultimeter(byte _PrimaryAddress, byte _SecondaryAddress, byte _BoardNumber) : base(_PrimaryAddress, _SecondaryAddress, _BoardNumber) { }
         public KeithleyMultimeter(string IDN, int DeviceOrder = 0, byte _BoardNumber = 0) : base(IDN, DeviceOrder, _BoardNumber) { }

        public bool ReadVoltage(out double Voltage)
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
                result = result.Substring(4);
                try
                {
                    Voltage = Convert.ToDouble(result, a);
                }

                catch
                {
                    Voltage = 0;
                }
                return isAlive;

            }
        }
    }

}
