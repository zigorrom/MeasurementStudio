using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Devices
{
    static class CheckValue
    {
        public static void assertTrue(bool Value, string Message)
        {
            if (Value != true) throw new Exception(Message);
        }
    }
}
