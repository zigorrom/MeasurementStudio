using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{

    public enum PropertyOrderPriorityEnum:int
    {
        Lowest,
        Low,
        Normal,
        High,
        Highest
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DataPropertyAttribute : Attribute
    {
        public DataPropertyAttribute(string propertyName, string propertyUnits, string propertyComments, PropertyOrderPriorityEnum propertyOrderPriority = PropertyOrderPriorityEnum.Normal)//bool isIndependent, bool isValid, int lastCalculatedRow, string propertyName, string propertyUnits, string propertyComments)
        {
           
            PropertyName = propertyName;
            PropertyUnits = propertyUnits;
            PropertyComments = propertyComments;
            PropertyOrderPriority = propertyOrderPriority;
        }
        

        ///public int PriorityNumber

        public string PropertyName { get; private set; }
        public string PropertyUnits { get; private set; }
        public string PropertyComments { get; private set; }
        public PropertyOrderPriorityEnum PropertyOrderPriority { get; private set; }
    }
}
