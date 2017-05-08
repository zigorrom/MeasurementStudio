using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChannelSwitchLibrary
{
    public class ArduinoChannelSwitch : AbstractMessageBasedInstrument
    {
        
        public ArduinoChannelSwitch(string Name, string Alias, string ResourceName):base(Name,Alias,ResourceName)
        {

        }

        private const short MAX_CHANNEL_NUMBER = 32;
        private const char CommandEndChar = ';';
        private const char CommandParamSeparationChar = ',';

        private enum Command
        {
            Watchdog,
            Acknowledge,
            SwitchChannel,
            Error,
            MotorCommand,
        }



        public override bool InitializeDevice()
        {
            return base.InitializeDevice();


        }

        public override bool IsAlive(bool SendIDN)
        {
            throw new NotImplementedException();
        }

        
        /// <summary>
        /// Channel numeration starts from 1.
        /// </summary>
        /// <param name="Channel"> channel number from 1 to 32</param>
        /// <param name="state">state: true/false</param>
        public void SwitchChannel(short Channel, bool state)
        {
            var numState = state?1:0;
            var command = String.Format("{0}{3}{1}{3}{2}{3}{4}", Command.SwitchChannel, Channel, numState, CommandParamSeparationChar, CommandEndChar);
            var response = Query(command);
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
