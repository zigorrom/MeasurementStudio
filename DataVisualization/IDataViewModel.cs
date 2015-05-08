using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataVisualization
{
    public interface IDataViewModel:INotifyPropertyChanged
    {

        double LineThickness { get; set; }
    }
}
