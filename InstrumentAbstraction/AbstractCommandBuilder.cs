using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Instruments
{
    public class AbstractCommandBuilder
    {
        protected CultureInfo m_currentInfo;
        protected const string ResponceNotFitExceptionMessage = "The responce doesn`t fit to any on cases.";
        public AbstractCommandBuilder()
        {
            m_currentInfo = CultureInfo.CreateSpecificCulture("en-US");
            m_currentInfo.NumberFormat = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" };
        }
        protected string StringFormat(string CommandFormat, params object[] Parameters)
        {
            return String.Format(m_currentInfo, CommandFormat, Parameters);
        }
        public int StringToInt(string str)
        {
            int val = 0;
            try
            {
                val = int.Parse(str, m_currentInfo);
            }
            catch (Exception e)
            {
                throw;
            }
            return val;
        }

        public double StringToDouble(string str)
        {
            double val = 0;
            try
            {
                val = double.Parse(str, m_currentInfo);
            }
            catch (Exception e)
            {
                throw;
            }
            return val;
        }
    }
}
