using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MeasurementStudio
{
    public interface IPageTransitionView
    {
        void ShowPage(UserControl page); 
    }
}
