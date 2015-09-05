﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.RangeHandlers
{
    public class RangeHandlerViewModel : INotifyPropertyChanged
    {
        public RangeHandlerViewModel()
        {
            RepeatCounts = 1;
            RangeHandler = new NormalDoubleRangeHandler();
            //m_repeatCounts = 0;
            //m_rangeHandler = new NormalDoubleRangeHandler();
        }
        private int m_repeatCounts;
        public int RepeatCounts
        {
            get { return m_repeatCounts; }
            set
            {
                if (value < 1)
                    value = 1;
                if (SetField(ref m_repeatCounts, value, "RepeatCounts"))
                {
                    if (m_rangeHandler != null)
                    {
                        m_rangeHandler.RepeatCounts = m_repeatCounts;
                    }
                }
            }
        }

        private AbstractDoubleRangeHandler m_rangeHandler;
        public AbstractDoubleRangeHandler RangeHandler
        {
            get { return m_rangeHandler; }
            set
            {
                if (SetField(ref m_rangeHandler, value, "RangeHandler"))
                {
                    m_rangeHandler.RepeatCounts = RepeatCounts;
                    //RepeatCounts = m_rangeHandler.RepeatCounts;
                }
            }
        }

        public void InvalidateProperties()
        {
            OnPropertyChanged("RepeatCounts");
            OnPropertyChanged("RangeHandler");
        }


        public event PropertyChangedEventHandler PropertyChanged;


        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}