using Helper.Ranges.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Helper.Ranges.DoubleRange
{
    
    public abstract class DoubleUnitValueDependencyObject : DependencyObject//, INotifyPropertyChanged
    {
        private string m_UnitName;
        private const string UnitNamePropertyName = "UnitName";
        private const string MagnitudePropertyName = "Magnitude";
        private const string PrefixPropertyName = "Prefix";
        private const string NumericValuePropertyName = "NumericValue";
        private double m_PrefixValue;

        
        public DoubleUnitValueDependencyObject(string UnitName, double Magnitude, UnitPrefixesEnum Prefix)
        {
            Initialize(UnitName, Magnitude, Prefix);
        }

        public DoubleUnitValueDependencyObject(string UnitName, double Magnitude)
        {
            Initialize(UnitName, Magnitude, UnitPrefixesEnum.DEFAULT);
        }

        public DoubleUnitValueDependencyObject(string UnitName)
        {
            Initialize(UnitName, 0, UnitPrefixesEnum.DEFAULT);
        }
        private void Initialize(string UnitName, double Magnitude, UnitPrefixesEnum Prefix)
        {
            this.PrefixValue = 1;
            this.m_UnitName = UnitName;
            this.Prefix = Prefix;
            this.Magnitude = Magnitude;
        }

        public string UnitName
        {
            get { return m_UnitName; }
        }

        private double PrefixValue
        {
            get { return m_PrefixValue; }
            set { m_PrefixValue = value; }
        }

        public static readonly DependencyProperty NumericValueProperty = DependencyProperty.Register(
            NumericValuePropertyName,
            typeof(double),
            typeof(DoubleUnitValueDependencyObject),
            new PropertyMetadata(default(double),
                new PropertyChangedCallback(NumericValueChanged))
            );



        private static void NumericValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DoubleUnitValueDependencyObject val = (DoubleUnitValueDependencyObject)d;
            val.Magnitude = (double)e.NewValue / val.PrefixValue;
        }

        public double NumericValue
        {
            get
            {
                return (double)GetValue(NumericValueProperty);
            }
            set
            {
                SetValue(NumericValueProperty, value);
            }
        }


        public static readonly DependencyProperty MagnitudeProperty = DependencyProperty.Register(
            MagnitudePropertyName,
            typeof(double),
            typeof(DoubleUnitValueDependencyObject),
            new PropertyMetadata(default(double),
                new PropertyChangedCallback(MagnitudeChanged))
            );

        private static void MagnitudeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var val = (DoubleUnitValueDependencyObject)d;
            val.NumericValue = (double)e.NewValue * val.PrefixValue;
        }

        public double Magnitude
        {
            get {
                return (double)GetValue(MagnitudeProperty);
            }
            set
            {
                SetValue(MagnitudeProperty, value);
            }
        }
        public static readonly DependencyProperty PrefixProperty = DependencyProperty.Register(
            PrefixPropertyName,
            typeof(UnitPrefixesEnum),
            typeof(DoubleUnitValueDependencyObject),
            new PropertyMetadata(default(UnitPrefixesEnum),
                new PropertyChangedCallback(PrefixChanged))
            );

        private static void PrefixChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var val = (DoubleUnitValueDependencyObject)d;
            val.PrefixValue = UnitPrefixesValues.ConvertFromPrefixToDouble((UnitPrefixesEnum)e.NewValue);
            val.NumericValue = val.Magnitude * val.PrefixValue;
        }

        public UnitPrefixesEnum Prefix
        {
            get {
                return (UnitPrefixesEnum)GetValue(PrefixProperty);
            }
            set
            {
                SetValue(PrefixProperty, value);
            }
        }

        public void CastToPrefix(UnitPrefixesEnum prefix)
        {
            Prefix = prefix;
            Magnitude = NumericValue / PrefixValue; //oldNumVal / m_PrefixValue;
        }
    }
}
