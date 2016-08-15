using Helper.ViewModelInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MeasurementStudioWebApi
{

    //public class DerivedHost:ServiceHost
    //{
    //    public DerivedHost
    //}



    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in both code and config file together.
    /// <summary>
    /// http://stackoverflow.com/questions/11267071/how-can-a-self-hosted-winform-wcf-service-interact-with-the-main-form
    /// </summary>
    public class Service : IService
    {
        private IServiceWindow _wnd;
        
        //public Service(IServiceWindow wnd)
        //{
        //    _wnd = wnd;
        //}

        public void DisplayMessage(string Message)
        {
            _wnd.CurrentSynchronizationContext.Send(_ => _wnd.ShowMessage(Message), null);
            //var form = MainWindow.CurrentInstance;
            //form.CurrentSynchronizationContext.Send(_ => form.ShowMessage(Message), null);
        }
    }
}
