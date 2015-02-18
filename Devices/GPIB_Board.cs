using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NationalInstruments.NI4882;
namespace Devices
{
   public  class GPIB_Board
    {
        private Board GPIB_Main_Board;
        private int BoardNumber;
        private bool Board_Connected_and_Operatable;
        private Dictionary<string , Address> AliveDevicesWithIDN;
       
        public GPIB_Board(int Board_address=1)
       
        {
            try
            {
                GPIB_Main_Board = new Board(Board_address);
                GPIB_Main_Board.SendInterfaceClear();
                GPIB_Main_Board.SetRemoteEnableLine();
                Board_Connected_and_Operatable = true;
                
            }
            catch(Exception e)
            {
                Board_Connected_and_Operatable = false;
            }
            BoardNumber = Board_address;
            ScanForInstruments();
        }

        private void ScanForInstruments()
        {
            if (!Board_Connected_and_Operatable) return;
            
            AddressCollection AliveDevices = GPIB_Main_Board.FindListeners();
            AliveDevicesWithIDN = new Dictionary<string, Address>();

            string DeviceAnswer;

            foreach(Address AliveDeviceAddress in AliveDevices)
            {
                GPIB_Main_Board.Write(AliveDeviceAddress, "*IDN?");
                DeviceAnswer=GPIB_Main_Board.ReadString(AliveDeviceAddress);
                AliveDevicesWithIDN.Add(DeviceAnswer,AliveDeviceAddress);
            }
            
        }
       public Device Open(string DeviceExpectedIDN_or_PartOfIDN, int DeviceOrder=0)
           // DeviceOrder - in case you have different devices with same IDN
        {
           string[] AliveDeviceIDNs=AliveDevicesWithIDN.Keys.ToArray();
           int FoundDeviceCounter=0;
           foreach( string IDN in AliveDeviceIDNs)
           {
               if (IDN.Contains(DeviceExpectedIDN_or_PartOfIDN))
               {
                   if (FoundDeviceCounter == DeviceOrder)
                       return new Device(BoardNumber, AliveDevicesWithIDN[IDN]);
                   else
                       FoundDeviceCounter++;
               }
           }
           return null;
        }
       public string[] Devices
       {
           get { return AliveDevicesWithIDN.Keys.ToArray(); }
       }
    }
}
