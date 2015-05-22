using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class DataPropertyAttribute : Attribute
    {
        public DataPropertyAttribute(bool isIndependent, bool isValid, int lastCalculatedRow, string propertyName, string propertyUnits, string propertyComments)
        {
            IsIndependent = isIndependent;
            IsValid = isValid;
            LastCalculatedRow = lastCalculatedRow;
            PropertyName = propertyName;
            PropertyUnits = propertyUnits;
            PropertyComments = propertyComments;
        }
        public bool IsIndependent { get; private set; }
        public bool IsValid { get; private set; }
        public int LastCalculatedRow { get; private set; }
        public string PropertyName { get; private set; }
        public string PropertyUnits { get; private set; }
        public string PropertyComments { get; private set; }
    }
}
