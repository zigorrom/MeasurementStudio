﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.DoubleRange
{
    public class DoubleRange
    {
        DoubleNumericValue m_Start;
        DoubleNumericValue m_End;
        DoubleNumericValue m_Step;
        DoubleNumericValue m_RangeWidth;
        int m_PointsCount;
        private bool m_CrossesZero;
        
        public DoubleRange(DoubleNumericValue Start, DoubleNumericValue End, DoubleNumericValue Step)
        {
            m_Start = Start;
            m_End = End;
            m_Step = Step;
            m_CrossesZero = (End * Start < 0);
            //m_RangeWidth = 
            if (m_Step == 0)
                m_PointsCount = 1;
            else
                m_PointsCount = (int)(m_RangeWidth / m_Step) + 1;
        }
    }
}