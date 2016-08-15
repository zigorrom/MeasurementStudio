using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MeasurementStudioWebApi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    /// <summary>
    /// http://stackoverflow.com/questions/11267071/how-can-a-self-hosted-winform-wcf-service-interact-with-the-main-form
    /// </summary>
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        void DisplayMessage(string Message);
    }
}
