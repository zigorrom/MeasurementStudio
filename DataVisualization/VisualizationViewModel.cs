using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataVisualization
{
    public class VisualizationViewModel:IVisualizationViewModel, INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }

        private string m_HorizontalAxisLabel;
        public string HorizontalAxisLabel
        {
            get
            {
                return m_HorizontalAxisLabel;
            }
            set
            {
                if (m_HorizontalAxisLabel == value)
                    return;
                m_HorizontalAxisLabel = value;
                OnPropertyChanged("HorizontalAxisLabel");
            }
        }


        private string m_VerticalAxisLabel;
        public string VertivalAxisLabel
        {
            get
            {
                return m_VerticalAxisLabel;
            }
            set
            {
                if (m_VerticalAxisLabel == value)
                    return;
                m_VerticalAxisLabel = value;
                OnPropertyChanged("VerticalAxisLabel");
            }
        }


        private double m_LineThickness;
        public double LineThickness
        {
            get
            {
                return m_LineThickness;
            }
            set
            {
                if (m_LineThickness == value)
                    return;
                m_LineThickness = value;
                OnPropertyChanged("LineThickness");
            }
        }


        private Visibility m_LegendVisibility;
        public Visibility LegendVisibility
        {
            get
            {
                return m_LegendVisibility;
            }
            set
            {
                if (m_LegendVisibility == value)
                    return;
                m_LegendVisibility = value;
                OnPropertyChanged("LegendVisibility");
            }
        }

       
    }
}
