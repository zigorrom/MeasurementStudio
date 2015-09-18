using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSignalAnalyzer
{
    public struct DiscretizationParameters
    {
        public DiscretizationParameters(SampleRatesEnum sampleRate, FrequencyResolutionsEnum resolution, int averageNumber)
        {
            _sampleRate = SampleRates.GetSampleRate(sampleRate);
            _frequencyResolution = FrequencyResolutions.GetFrequencyResolution(resolution);
            _cutoffFrequency = CutoffFrequencies.GetCutoffFrequency(sampleRate);
            _pointsNumber = (int)(_sampleRate / _frequencyResolution);
            _averageNumber = averageNumber;
        }

        public DiscretizationParameters(CutoffFrequenciesEnum cutoff, FrequencyResolutionsEnum resolution, int averageNumber)
        {
            _cutoffFrequency = CutoffFrequencies.GetCutoffFrequency(cutoff);
            _frequencyResolution = FrequencyResolutions.GetFrequencyResolution(resolution);
            _sampleRate = SampleRates.GetSampleRate(cutoff);
            _pointsNumber = (int)(_sampleRate / _frequencyResolution);
            _averageNumber = averageNumber;
        }

        private double _frequencyResolution;
        private int _sampleRate;
        private int _cutoffFrequency;
        private int _pointsNumber;
        private int _averageNumber;


        public double FrequencyResolution { get { return _frequencyResolution; } }
        public int SampleRate { get { return _sampleRate; } }
        public int CutoffFrequency { get { return _cutoffFrequency; } }
        public int PointsNumber { get { return _pointsNumber; } }
        public int AverageNumber { get { return _averageNumber; } set { _averageNumber = value; } }

    }

    public enum FrequencyResolutionsEnum : int
    {
        fs0_125,
        fs0_25,
        fs0_5,
        fs1,
        fs2,
        fs4,
        fs8,
        fs16,
        fs32,
        fs64,
        fs128,
        fs256
    }

    public struct FrequencyResolutions
    {
        private const double FR0_125 = 0.125;
        private const double FR0_25 = 0.25;
        private const double FR0_5 = 0.5;
        private const double FR1 = 1;
        private const double FR2 = 2;
        private const double FR4 = 4;
        private const double FR8 = 8;
        private const double FR16 = 16;
        private const double FR32 = 32;
        private const double FR64 = 64;
        private const double FR128 = 128;
        private const double FR256 = 256;

        public static double GetFrequencyResolution(FrequencyResolutionsEnum resolution)
        {
            switch (resolution)
            {
                case FrequencyResolutionsEnum.fs0_125:
                    return FR0_125;
                case FrequencyResolutionsEnum.fs0_25:
                    return FR0_25;
                case FrequencyResolutionsEnum.fs0_5:
                    return FR0_5;
                case FrequencyResolutionsEnum.fs2:
                    return FR2;
                case FrequencyResolutionsEnum.fs4:
                    return FR4;
                case FrequencyResolutionsEnum.fs8:
                    return FR8;
                case FrequencyResolutionsEnum.fs16:
                    return FR16;
                case FrequencyResolutionsEnum.fs32:
                    return FR32;
                case FrequencyResolutionsEnum.fs64:
                    return FR64;
                case FrequencyResolutionsEnum.fs128:
                    return FR128;
                case FrequencyResolutionsEnum.fs256:
                    return FR256;
                case FrequencyResolutionsEnum.fs1:
                default:
                    return FR1;
            }
        }

    }


    public enum SampleRatesEnum : int
    {
        Fs3K,
        Fs7K,
        Fs15K,
        Fs30K,
        Fs60K,
        Fs90K,
        Fs120K,
        Fs150K,
        Fs180K,
        Fs210K,
        Fs240K,
        Fs270K,
        Fs300K,
        Fs330K,
        Fs360K,
        Fs390K,
        Fs420K,
        Fs450K
    }
    public struct SampleRates
    {
        private const int Fs3K = 3072;
        private const int Fs7K = 7680;
        private const int Fs15K = 15360;
        private const int Fs30K = 30208;
        private const int Fs60K = 60416;
        private const int Fs90K = 90112;
        private const int Fs120K = 120320;
        private const int Fs150K = 150016;
        private const int Fs180K = 180224;
        private const int Fs210K = 210432;
        private const int Fs240K = 240128;
        private const int Fs270K = 270336;
        private const int Fs300K = 300032;
        private const int Fs330K = 330240;
        private const int Fs360K = 360448;
        private const int Fs390K = 390144;
        private const int Fs420K = 420352;
        private const int Fs450K = 450048;


        public static int GetSampleRate(SampleRatesEnum sampleRate)
        {
            switch (sampleRate)
            {
                case SampleRatesEnum.Fs3K:
                    return Fs3K;
                case SampleRatesEnum.Fs7K:
                    return Fs7K;
                case SampleRatesEnum.Fs15K:
                    return Fs15K;
                case SampleRatesEnum.Fs30K:
                    return Fs30K;
                case SampleRatesEnum.Fs60K:
                    return Fs60K;
                case SampleRatesEnum.Fs90K:
                    return Fs90K;
                case SampleRatesEnum.Fs120K:
                    return Fs120K;
                case SampleRatesEnum.Fs150K:
                    return Fs150K;
                case SampleRatesEnum.Fs180K:
                    return Fs180K;
                case SampleRatesEnum.Fs210K:
                    return Fs210K;
                case SampleRatesEnum.Fs240K:
                    return Fs240K;
                case SampleRatesEnum.Fs270K:
                    return Fs270K;
                case SampleRatesEnum.Fs300K:
                    return Fs300K;
                case SampleRatesEnum.Fs330K:
                    return Fs330K;
                case SampleRatesEnum.Fs360K:
                    return Fs360K;
                case SampleRatesEnum.Fs390K:
                    return Fs390K;
                case SampleRatesEnum.Fs420K:
                    return Fs420K;
                case SampleRatesEnum.Fs450K:
                    return Fs450K;
                default:
                    throw new ArgumentException();
            }
        }
        public static int GetSampleRate(CutoffFrequenciesEnum cutoff)
        {
            switch (cutoff)
            {
                case CutoffFrequenciesEnum.f1k:
                    return Fs3K;
                case CutoffFrequenciesEnum.f2_5k:
                    return Fs7K;
                case CutoffFrequenciesEnum.f5k:
                    return Fs15K;
                case CutoffFrequenciesEnum.f10k:
                    return Fs30K;
                case CutoffFrequenciesEnum.f20k:
                    return Fs60K;
                case CutoffFrequenciesEnum.f30k:
                    return Fs90K;
                case CutoffFrequenciesEnum.f40k:
                    return Fs120K;
                case CutoffFrequenciesEnum.f50k:
                    return Fs150K;
                case CutoffFrequenciesEnum.f60k:
                    return Fs180K;
                case CutoffFrequenciesEnum.f70k:
                    return Fs210K;
                case CutoffFrequenciesEnum.f80k:
                    return Fs240K;
                case CutoffFrequenciesEnum.f90k:
                    return Fs270K;
                case CutoffFrequenciesEnum.f100k:
                    return Fs300K;
                case CutoffFrequenciesEnum.f110k:
                    return Fs330K;
                case CutoffFrequenciesEnum.f120k:
                    return Fs360K;
                case CutoffFrequenciesEnum.f130k:
                    return Fs390K;
                case CutoffFrequenciesEnum.f140k:
                    return Fs420K;
                case CutoffFrequenciesEnum.f150k:
                    return Fs450K;
                default:
                    throw new ArgumentException();
            }
        }

    }


    public enum CutoffFrequenciesEnum : int
    {
        f1k,
        f2_5k,
        f5k,
        f10k,
        f20k,
        f30k,
        f40k,
        f50k,
        f60k,
        f70k,
        f80k,
        f90k,
        f100k,
        f110k,
        f120k,
        f130k,
        f140k,
        f150k
    }
    public struct CutoffFrequencies
    {
        private const int F1K = 1000;
        private const int F2_5K = 2500;
        private const int F5K = 5000;
        private const int F10K = 10000;
        private const int F20K = 20000;
        private const int F30K = 30000;
        private const int F40K = 40000;
        private const int F50K = 50000;
        private const int F60K = 60000;
        private const int F70K = 70000;
        private const int F80K = 80000;
        private const int F90K = 90000;
        private const int F100K = 100000;
        private const int F110K = 110000;
        private const int F120K = 120000;
        private const int F130K = 130000;
        private const int F140K = 140000;
        private const int F150K = 150000;


        public static int GetCutoffFrequency(SampleRatesEnum sampleRate)
        {
            switch (sampleRate)
            {
                case SampleRatesEnum.Fs3K:
                    return F1K;
                case SampleRatesEnum.Fs7K:
                    return F2_5K;
                case SampleRatesEnum.Fs15K:
                    return F5K;
                case SampleRatesEnum.Fs30K:
                    return F10K;
                case SampleRatesEnum.Fs60K:
                    return F20K;
                case SampleRatesEnum.Fs90K:
                    return F30K;
                case SampleRatesEnum.Fs120K:
                    return F40K;
                case SampleRatesEnum.Fs150K:
                    return F50K;
                case SampleRatesEnum.Fs180K:
                    return F60K;
                case SampleRatesEnum.Fs210K:
                    return F70K;
                case SampleRatesEnum.Fs240K:
                    return F80K;
                case SampleRatesEnum.Fs270K:
                    return F90K;
                case SampleRatesEnum.Fs300K:
                    return F100K;
                case SampleRatesEnum.Fs330K:
                    return F110K;
                case SampleRatesEnum.Fs360K:
                    return F120K;
                case SampleRatesEnum.Fs390K:
                    return F130K;
                case SampleRatesEnum.Fs420K:
                    return F140K;
                case SampleRatesEnum.Fs450K:
                    return F150K;
                default:
                    throw new ArgumentException();
            }
        }
        public static int GetCutoffFrequency(CutoffFrequenciesEnum cutoff)
        {
            switch (cutoff)
            {
                case CutoffFrequenciesEnum.f1k:
                    return F1K;
                case CutoffFrequenciesEnum.f2_5k:
                    return F2_5K;
                case CutoffFrequenciesEnum.f5k:
                    return F5K;
                case CutoffFrequenciesEnum.f10k:
                    return F10K;
                case CutoffFrequenciesEnum.f20k:
                    return F20K;
                case CutoffFrequenciesEnum.f30k:
                    return F30K;
                case CutoffFrequenciesEnum.f40k:
                    return F40K;
                case CutoffFrequenciesEnum.f50k:
                    return F50K;
                case CutoffFrequenciesEnum.f60k:
                    return F60K;
                case CutoffFrequenciesEnum.f70k:
                    return F70K;
                case CutoffFrequenciesEnum.f80k:
                    return F80K;
                case CutoffFrequenciesEnum.f90k:
                    return F90K;
                case CutoffFrequenciesEnum.f100k:
                    return F100K;
                case CutoffFrequenciesEnum.f110k:
                    return F110K;
                case CutoffFrequenciesEnum.f120k:
                    return F120K;
                case CutoffFrequenciesEnum.f130k:
                    return F130K;
                case CutoffFrequenciesEnum.f140k:
                    return F140K;
                case CutoffFrequenciesEnum.f150k:
                    return F150K;
                default:
                    throw new ArgumentException();
            }
        }


    }
}
