using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace MeasurementStudioWebApi
{
    public class MeasurementServiceHost : ServiceHost
    {
        public MeasurementServiceHost(IMeasurementWebApi api, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (api == null)
            {
                throw new ArgumentNullException("api");
            }

            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.ContractBehaviors.Add(new MeasurementServiceInstanceProvider(api));
            }
        }
    }
    class MeasurementServiceInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private readonly IMeasurementWebApi api;

        public MeasurementServiceInstanceProvider(IMeasurementWebApi api)
        {
            if (api == null)
            {
                throw new ArgumentNullException("api");
            }
            // TODO: Complete member initialization
            this.api = api;
        }

        public object GetInstance(InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            return this.GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return new MeasurementWebApiService(this.api);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            var disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            //throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            //throw new NotImplementedException();
        }
    }


    public class MeasurementWebApiService:IMeasurementWebApiService
    {
        private IMeasurementWebApi _instance;

        public MeasurementWebApiService(IMeasurementWebApi instance)
        {
            _instance = instance;
        }

        public string GetName()
        {
            var msg = String.Format("IsAuthenticated: {0}\r\nAuthenticationType: {1}\r\n Name: {2} ",
                  ServiceSecurityContext.Current.PrimaryIdentity.IsAuthenticated,
                  ServiceSecurityContext.Current.PrimaryIdentity.AuthenticationType,
                  ServiceSecurityContext.Current.PrimaryIdentity.Name);
            //_wnd.ShowMessage(msg);
            return String.Format(msg);
        }

        public string ShowMessage(string Message)
        {
            _instance.CurrentSynchronizationContext.Send(_ => _instance.ShowMessage(Message), null);
            return "Returned:" + Message;
        }




        public string[] GetAvailablePages()
        {
            return _instance.GetAvailablePages();
        }

        public bool SwitchToPage(string PageName)
        {
            return _instance.SwitchToPage(PageName);
        }
        

        public async Task<string> GetUserNameAsync()
        {
            try
            {
                var result = await Task.Run(() => GetName());
                return result;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }
    }
}
