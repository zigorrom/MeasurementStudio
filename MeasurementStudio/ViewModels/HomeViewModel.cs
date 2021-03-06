﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementStudio
{
    public class HomeViewModel:INotifyPropertyChanged
    {
        #region PropertyEvents

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetField(ref _userName, value, "UserName"); }
        }
        public HomeViewModel()
        {
            try
            {
                UserName = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;//System.dire //Environment.UserName;
            }
            catch 
            {
                UserName = "User";
            }
            
        }
    }
}
