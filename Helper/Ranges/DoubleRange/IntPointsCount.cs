using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Helper.Ranges.DoubleRange
{
    public class IntPointsCount : DependencyObject// INotifyPropertyChanged
    {
        public IntPointsCount()
        {
            PointsCount = 0;
        }

        public IntPointsCount(int count)
        {
            PointsCount = count;
        }


        public static readonly DependencyProperty PointsCountProperty = DependencyProperty.Register(
            "PointsCount",
            typeof(int),
            typeof(IntPointsCount),
            new PropertyMetadata(default(int))
            );

        public int PointsCount
        {
            get
            {
                return (int)GetValue(PointsCountProperty);
            }
            set
            {
                SetValue(PointsCountProperty, value);

            }
        }


    }
}
