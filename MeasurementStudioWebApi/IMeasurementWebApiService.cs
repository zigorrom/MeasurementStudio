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
        Task<string> GetUserNameAsync();
        //[OperationContract(AsyncPattern=true)]
        //IAsyncResult GetNameAsync();

        [OperationContract]
        string ShowMessage(string Message);

        [OperationContract]
        string[] GetAvailablePages();

        [OperationContract]
        bool SwitchToPage(string PageName);
       

    }
}
