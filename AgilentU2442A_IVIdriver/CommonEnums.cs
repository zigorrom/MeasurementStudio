using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A_IVIdriver
{
    public enum DigitalChannelSizeEnum
    {
        Ch_4_BIT = 4,
        Ch_8_BIT = 8
    }

    public enum ClockSourceEnum
    {
        Internal,
        External
    }

    public enum DigitalDirectionEnum
    {
        Input,
        Output
    }

    public enum SSIMode
    {
        None,
        Master,
        Slave
    }

    public enum OutputStateEnum
    {
        On,
        Off
    }

    public enum TriggerSourceEnum
    {
        None,
        EXTD_AO_TRIG,
        EXTA_TRIG,
        STRG
    }

    public enum TriggerTypeEnum
    {
        Post,
        Delay,
        Pre,
        Mid
    }

    public enum AnalogTriggerSourceEnum
    {
        CH101,
        CH102,
        CH103,
        CH104,
        EXTAP
    }

    public enum TrigerConditionEnum
    {
        AHIG,
        WIND,
        BLOW
    }

    public enum TriggerPolarityEnum
    {
        NEG,
        POS
    }

    //public enum AIRangesEnum
    //{
    //    Range1_25,
    //    Range2_5,
    //    Range5,
    //    Range10
    //}

    public enum PolarityEnum
    {
        Unipolar,
        Bipolar
    }

    public enum ReferenceVoltageEnum
    {
        External,
        Internal
    }

    public enum ChannelEnableEnum
    {
        Enabled,
        Disabled
    }

    public enum VoltageRangeEnum
    {
        V10 ,
        V5 ,
        V2_5 ,
        V1_25,
        AUTO

    }

    public enum CounterFunctionEnum
    {
        FREQuency,
        PERiod,
        PWIDth,
        TOTalize
    }

    public enum GateSourceEnum
    {
        External,
        Internal
    }

    public enum GatePolarity
    {
        AHI,
        ALO
    }

    public enum EnableGateEnum
    {
        Enable,
        Disable
    }

    public enum CountingDirection
    {
        DOWN,
        UP
    }

    public enum WaveformStatus
    {
        EMPTY,
        FRAG,
        DATA,
        OVER
    }

    public enum WaveformComplete
    {
        YES,
        NO
    }


}
