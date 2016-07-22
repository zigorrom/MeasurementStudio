using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilentU2442A_IVIdriver
{
    interface IDigitalChannel
    {
        void DigitalWrite(int value);
        //bool VerifyDigitalWrite();

        void DigitalWriteBit(bool value, int bit);
        //bool Verify

        int DigitalRead();

        bool DigitalReadBit(int bit);

    }
}
