using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Devices;
using System.Windows.Forms;
namespace HP34401A
{
    public class HP34401AMultimeter:GPIB_Device
    {
             public HP34401AMultimeter(byte _PrimaryAddress, byte _SecondaryAddress, byte _BoardNumber) : base(_PrimaryAddress, _SecondaryAddress, _BoardNumber) { }
             public HP34401AMultimeter(string IDN, int DeviceOrder = 0, byte _BoardNumber = 0) : base(IDN, DeviceOrder, _BoardNumber) { }
             public override bool InitDevice()
             {
                 try
                 {
                     this.SendCommandRequest("*CLS");
                     this.SendCommandRequest("*RST");
                     //Set 1 measurement for read
                    this.SendCommandRequest("*ESE 1");
                     //enable auto-output off
                    this.SendCommandRequest("*SRE 32");
                     //enable source as Voltage
                     this.SendCommandRequest("*OPC");
                     
                     //enable sensing as Voltage

                 }
                 catch
                 {
                     this.isAlive = false;
                 }
                 return this.isAlive;
             }

        public bool ReadVoltage(out double Voltage)
             {

                 string result;
                 try
                 {
                     this.SendCommandRequest("*SRE 32");
                     result = this.RequestQuery("MEAS:VOLT:DC?");
                 }
                 catch(Exception e)
                 {
                     result = "0";
                     isAlive = false;
                     MessageBox.Show(e.Message);
                 }
                 NumberFormatInfo a = new NumberFormatInfo();
                 a.NumberDecimalSeparator = ".";
                 a.NumberGroupSeparator = "";
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
