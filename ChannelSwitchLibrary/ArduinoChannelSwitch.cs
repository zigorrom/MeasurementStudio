using Instruments;
using NationalInstruments.VisaNS;
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

            try
            {
                var serialSession = new SerialSession(ResourceName, AccessModes.ExclusiveLock, 10000, false);
                serialSession.BaudRate = 115200;
                serialSession.Timeout = 10000;
                serialSession.TerminationCharacter = (byte)';';
                serialSession.TerminationCharacterEnabled = true;
                Session = (MessageBasedSession)serialSession;
                //m_session = new MessageBasedSession(m_resourceName, AccessModes.ExclusiveLock, 10000);

            }
            catch (VisaException e)
            {
                OnVisaException(e);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

            
            
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
            var numState = state ? 1 : 0;
            var command = String.Format("{0}{3}{1}{3}{2}{4}", (short)Command.SwitchChannel, Channel, numState, CommandParamSeparationChar, CommandEndChar);
            var response = SendCommand(command); //Query(command);
            var val = GetResponce();

        }

        private void parseResponse()
        {

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
