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

        public VisualizationViewModel(DataPlotter plotter)
        {
            m_plotter = plotter;
            //m_plotter.AddLineGraph()
        }

        private DataPlotter m_plotter;


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
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
                SetField(ref m_HorizontalAxisLabel, value, "HorizontalAxisLabel");
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
                SetField(ref m_VerticalAxisLabel, value,"VerticalAxisLabel");
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
                SetField(ref m_LineThickness,value,"LineThickness");
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
                SetField(ref m_LegendVisibility, value,"LegendVisibility");
            }
        }



        private string m_HeaderLabel;
        public string HeaderLabel
        {
            get
            {
                return m_HeaderLabel;
            }
            set
            {
                SetField(ref m_HeaderLabel, value, "HeaderLabel");
            }
        }
    }
}
