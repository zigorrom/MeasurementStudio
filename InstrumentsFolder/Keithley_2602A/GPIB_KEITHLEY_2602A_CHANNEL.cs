using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

using SMU;

namespace SMU.KEITHLEY_2602A
{
    public class GPIB_KEITHLEY_2602A_CHANNEL : GPIB_KEITHLEY_2602A, I_SMU
    {
        private NumberStyles style;
        private CultureInfo culture;

        private KEITHLEY_2602A_Channels _SelectedChannel;
        public KEITHLEY_2602A_Channels SelectedChannel
        {
            get { return _SelectedChannel; }
            set { _SelectedChannel = value; }
        }

        public GPIB_KEITHLEY_2602A_CHANNEL(byte _PrimaryAddress, byte _SecondaryAddress, byte _BoardNumber, KEITHLEY_2602A_Channels _Channel)
            : base(_PrimaryAddress, _SecondaryAddress, _BoardNumber)
        {
            style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            _SelectedChannel = _Channel;
        }

        public void SwitchON()
        {
            SwitchChannelState(_SelectedChannel, KEITHLEY_2602A_Channel_Status.Channel_ON);
        }

        public void SwitchOFF()
        {
            SwitchChannelState(_SelectedChannel, KEITHLEY_2602A_Channel_Status.Channel_OFF);
        }

        public bool SetVoltageLimit(double Value)
        {
            try
            {
                SetSourceLimit(Value, KEITHLEY_2601A_LimitMode.Voltage, _SelectedChannel);
                return true;
            }
            catch { return false; }
        }

        public bool SetCurrentLimit(double Value)
        {
            try
            {
                SetSourceLimit(Value, KEITHLEY_2601A_LimitMode.Current, _SelectedChannel);
                return true;
            }
            catch { return false; }
        }

        public bool SetSourceVoltage(double Value)
        {
            try
            {
                SetValueToChannel(Value, KEITHLEY_2601A_SourceMode.Voltage, _SelectedChannel);
                return true;
            }
            catch { return false; }
        }

        public bool SetSourceCurrent(double Value)
        {
            try
            {
                SetValueToChannel(Value, KEITHLEY_2601A_SourceMode.Current, _SelectedChannel);
                return true;
            }
            catch { return false; }
        }

        public double MeasureVoltage(int NumberOfAverages, double TimeDelay)
        {
            double MeasuredVoltage;

            var MeasuredVoltageString = MeasureIV_ValueInChannel(_SelectedChannel, KEITHLEY_2601A_MeasureMode.Voltage, NumberOfAverages, TimeDelay).TrimEnd('\n');
            var isSucceed = double.TryParse(MeasuredVoltageString, style, culture, out MeasuredVoltage);

            if (isSucceed)
                return MeasuredVoltage;
            else return double.NaN;
        }

        public double MeasureCurrent(int NumberOfAverages, double TimeDelay)
        {
            double MeasuredCurrent;

            var MeasuredCurrentString = MeasureIV_ValueInChannel(_SelectedChannel, KEITHLEY_2601A_MeasureMode.Current, NumberOfAverages, TimeDelay).TrimEnd('\n');
            var isSucceed = double.TryParse(MeasuredCurrentString, style, culture, out MeasuredCurrent);

            if (isSucceed)
                return MeasuredCurrent;
            else return double.NaN;
        }


        public double MeasureResistance(double valueThroughTheStructure, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            double measuredResistance;
            KEITHLEY_2601A_SourceMode _sourceMode = KEITHLEY_2601A_SourceMode.Voltage;

            switch (sourceMode)
            {
                case SourceMode.Voltage:
                    {
                        _sourceMode = KEITHLEY_2601A_SourceMode.Voltage;
                    } break;
                case SourceMode.Current:
                    { 
                        _sourceMode = KEITHLEY_2601A_SourceMode.Current;
                    } break;
                default:
                    break;
            }
            var measuredRessitanceString = MeasureResistanceOrPowerValueInChannel(_SelectedChannel, _sourceMode, KEITHLEY_2601A_MeasureMode.Resistance, valueThroughTheStructure, NumberOfAverages, TimeDelay).TrimEnd('\n');
            var isSucceed = double.TryParse(measuredRessitanceString, style, culture, out measuredResistance);

            if (isSucceed)
                return measuredResistance;
            else return double.NaN;
        }

        public double MeasurePower(double valueThroughTheStructure, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            double measuredPower;
            KEITHLEY_2601A_SourceMode _sourceMode = KEITHLEY_2601A_SourceMode.Voltage;

            switch (sourceMode)
            {
                case SourceMode.Voltage:
                    {
                        _sourceMode = KEITHLEY_2601A_SourceMode.Voltage;
                    } break;
                case SourceMode.Current:
                    {
                        _sourceMode = KEITHLEY_2601A_SourceMode.Current;
                    } break;
                default:
                    break;
            }
            var measuredPowerString = MeasureResistanceOrPowerValueInChannel(_SelectedChannel, _sourceMode, KEITHLEY_2601A_MeasureMode.Power, valueThroughTheStructure, NumberOfAverages, TimeDelay).TrimEnd('\n');
            var isSucceed = double.TryParse(measuredPowerString, style, culture, out measuredPower);

            if (isSucceed)
                return measuredPower;
            else return double.NaN;
        }
    }
}
