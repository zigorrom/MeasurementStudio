using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Helper
{
	/// <summary>
	/// Interaction logic for DataInputControl.xaml
	/// </summary>
	public partial class DataInputControl : UserControl
	{
		public DataInputControl()
		{
			this.InitializeComponent();
            
		}
        private object m_InputName;
        public object InputName
        {
            get { return m_InputName; }
            set { m_InputName = value; }
        }

        private object m_InputValue;

        public object InputValue
        {
            get { return m_InputValue; }
            set { m_InputValue = value; }
        }
        
	}
}