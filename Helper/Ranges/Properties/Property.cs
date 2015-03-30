using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.Properties
{
    public abstract class PropertyBase<T>:Comparer<T>, IEqualityComparer<T>,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void SubscribeOnPropertyChanged(PropertyChangedEventHandler handler)
        {
            PropertyChanged += handler;
        }
        public void UnsubscribeFromPropertyChanged(PropertyChangedEventHandler handler)
        {
            PropertyChanged -= handler;
        }

        public override int Compare(T x, T y)
        {
            return Comparer<T>.Default.Compare(x, y);
        }

        public bool Equals(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        public PropertyBase(string propertyName)
        {
            m_isEditing = false;
            m_PropertyName = propertyName;
            m_PropertyValue = default(T);
           
        }

        protected bool m_isEditing;

        private string m_PropertyName;
        public string PropertyName
        {
            get { return m_PropertyName; }
            private set { m_PropertyName = value; }
        }

        private T m_PropertyValue;
        public T PropertyValue
        {
            get { return m_PropertyValue; }
            set {
                SetValue(value);
            }
        }

        public bool SetValue(T value)
        {
            if (m_isEditing)
                return false;
            if (EqualityComparer<T>.Default.Equals(m_PropertyValue, value))
                return false;
            m_isEditing = true;
            m_PropertyValue = value;
            OnPropertyChanged(PropertyName);
            m_isEditing = false;
            return true;
        }

        public void SetValue(T value, Action action)
        {
            if (m_isEditing)
                return;
            if (EqualityComparer<T>.Default.Equals(m_PropertyValue, value))
                return;
            m_isEditing = true;
            m_PropertyValue = value;
            OnPropertyChanged(PropertyName);
            action();
            m_isEditing = false;
        }





       
    }
}
