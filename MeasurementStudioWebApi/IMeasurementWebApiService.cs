using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementStudioWebApi
{
    [ServiceContract]
    public interface IMeasurementWebApiService
    {
        [OperationContract]
        string GetName();

        [OperationContract]
        string ShowMessage(string Message);

        [OperationContract]
        string[] GetAvailablePages();

        [OperationContract]
        bool SwitchToPage(string PageName);
       
    }
}
