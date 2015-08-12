using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class DataPropertyAttribute : Attribute
    {
        public DataPropertyAttribute(string propertyName, string propertyUnits, string propertyComments)//bool isIndependent, bool isValid, int lastCalculatedRow, string propertyName, string propertyUnits, string propertyComments)
        {
           
            PropertyName = propertyName;
            PropertyUnits = propertyUnits;
            PropertyComments = propertyComments;
        }
        
        public string PropertyName { get; private set; }
        public string PropertyUnits { get; private set; }
        public string PropertyComments { get; private set; }
    }
}
