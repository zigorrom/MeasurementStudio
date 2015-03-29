using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Helper.Ranges
{
    public class DoubleValueValudationRule:ValidationRule
    {
        public DoubleValueValudationRule():base()
        {

        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //throw new NotImplementedException();
            double result = 0;
            try
            {
                string val = value as string;
                result = double.Parse(val, NumberStyles.Float, new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "" });
                return new ValidationResult(true, null);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, e.Message);
            }
        }
    }
}
