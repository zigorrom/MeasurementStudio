using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP35670ANamespace
{
    public class HP35670A:AbstractMessageBasedInstrument
    {

        private HP35670ACommandBuilder CommandBuilder { get; set; }

        public HP35670A(string Name, string Alias, string ResourceName):base(Name, Alias, ResourceName)
        {
            SetTimeout(-1);
        }

        public override bool InitializeDevice()
        {
            return base.InitializeDevice();


        }
        
        public bool Wait()
        {
            if (SendCommand(CommandBuilder.WAItQuery()))
                return true;
            return false;
        }

        public bool Init()
        {

            if (SendCommand(CommandBuilder.INIT()))
                return true;
            return false;
        }

        public bool Abort()
        {
            if (SendCommand(CommandBuilder.ABORT()))
                return true;
            return false;
        }

        public bool OutputEnable()
        {
            if (SendCommand(CommandBuilder.OUTPutSTATus(SwitchState.On)))
                return true;
            return false;
        }

        public bool OutputDisable()
        {
            if (SendCommand(CommandBuilder.OUTPutSTATus(SwitchState.Off)))
                return true;
            return false;
        }

        public bool ClearCaptureBuffer()
        {
            if (SendCommand(CommandBuilder.TCAPtureDELete()))
                return true;
            return false;
        }

        public bool SelectInstrumentMode(InstrumentModes mode)
        {
            if (SendCommand(CommandBuilder.INSTrumentSELect(mode)))
                return true;
            return false;
        }

        public bool Input2Disable()
        {
            if (SendCommand(CommandBuilder.INPut(2, SwitchState.Off)))
                return true;
            return false;
        }

        public bool Channel1Select()
        {
            if (SendCommand(CommandBuilder.CALCulateACTive(1, ActiveTracesEnum.A)))
                return true;
            return false;
        }

        public bool DataFormat()
        {
            if (SendCommand(CommandBuilder.FORMatDATA(FormatEnum.REAL, 64)))
                return true;
            return false;
        }

        public bool Feed()
        {
            if (SendCommand(CommandBuilder.CALCulateFEED(1, CMDSTR.XFR_POW)))
                return true;
            return false;
        }

        public bool SelectUnits()
        {
            if (SendCommand(CommandBuilder.CALCulateUNITVOLTage(1, VoltageUnits.V2divHZ)))
                return true;
            return false;
        }

        public bool DisableAutoCalibration()
        {
            if (SendCommand(CommandBuilder.CALibrationAUTO(CalibrationEnum.OFF)))
                return true;
            return false;
        }

        public bool SetFrequencyResolution()
        {
            if (SendCommand(CommandBuilder.FREquencyRESolution(FrequencyResolutionEnum.r1600)))
                return true;
            return false;
        }

        public bool SetVoltageOffset()
        {
            if (SendCommand(CommandBuilder.SOURceVOLTageOFFSet(5)))
                return true;
            return false;
        }

        public bool ClearStatus()
        {
            if (SendCommand(CommandBuilder.ClearStatus()))
                return true;
            return false;
        }

        public bool Calibrate()
        {
            var a = CommandBuilder.CalibrateQueryParse(Query(CommandBuilder.CalibrateQuery()));
            return a;
        }


        public bool SetAverages(int Averages)
        {
            if (SendCommand(CommandBuilder.AVERage(SwitchState.On)))
                return true;
            return false;
        }

        public bool SetUpdateNumber(int NUpdates)
        {
            if (SendCommand(CommandBuilder.AVERageIRESultRATE(NUpdates)))
                return true;
            return false;
        }

        public bool SetAverageCount(int Naver)
        {
            if (SendCommand(CommandBuilder.AVERageCOUNT(Naver)))
                return true;
            return false;
        }

        public bool GetPointsNumber(out int Number)
        {
            Number = 0;
            var query = Query(CommandBuilder.CALCulateDATAHEADerPOINtsQuery());
            if (!String.IsNullOrEmpty(query))
                return false;
            var a = CommandBuilder.StringToInt(query);
            Number = a;
            return true;
        }

        public bool SetStartFrequency(float freq)
        {
            if (SendCommand(CommandBuilder.FREQuencySTARt(freq )))
                return true;
            return false;
        }

        public bool SetStopFrequency(float freq)
        {
            if (SendCommand(CommandBuilder.FREQuencySTOP(freq)))
                return true;
            return false;
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
