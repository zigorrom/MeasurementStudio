using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoDriver;

namespace ChannelSwitchLibrary
{
    public class ArduinoChannelSwitch
    {
        private ArduinoDriver.ArduinoDriver driver;
        public ArduinoChannelSwitch()
        {
            driver = new ArduinoDriver.ArduinoDriver(ArduinoUploader.Hardware.ArduinoModel.UnoR3, true);

        }


    }
}
