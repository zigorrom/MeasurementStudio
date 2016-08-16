using Helper.ViewModelInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace MeasurementStudioWebApi
{

    //public class DerivedHost:ServiceHost
    //{
    //    public DerivedHost
    //}
    //public class MeasurementStudioServiceHostFactory:ServiceHostFactory
    //{
    //    private readonly IServiceWindow _wnd;
    //    public MeasurementStudioServiceHostFactory()
    //    {
    //        throw new NotImplementedException();
    //        //_wnd =
    //    }

    //    protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
    //    {
    //        return new MeasurementStudioServiceHost(_wnd, serviceType, baseAddresses);
    //        //return base.CreateServiceHost(serviceType, baseAddresses);
    //    }

    //}

    public class MeasurementStudioServiceHost : ServiceHost
    {
        public MeasurementStudioServiceHost(IServiceWindow wnd, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if(wnd == null)
            {
                throw new ArgumentNullException("wnd");
            }

            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.ContractBehaviors.Add(new ServiceWindowInstanceProvider(wnd));
            }
        }
    }
    class ServiceWindowInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private readonly IServiceWindow wnd;

        public ServiceWindowInstanceProvider(IServiceWindow wnd)
        {
            if(wnd == null)
            {
                throw new ArgumentNullException("wnd");
            }
            // TODO: Complete member initialization
            this.wnd = wnd;
        }

        public object GetInstance(InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            return this.GetInstance(instanceContext);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return new Service(this.wnd);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            var disposable = instance as IDisposable;
            if(disposable != null)
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

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in both code and config file together.
    /// <summary>
    /// http://stackoverflow.com/questions/11267071/how-can-a-self-hosted-winform-wcf-service-interact-with-the-main-form
    /// </summary>
    public class Service : IService
    {
        private IServiceWindow _wnd;

        public Service(IServiceWindow wnd)
        {
            _wnd = wnd;
        }

        public string DisplayMessage(string Message)
        {
            //return "Returned:" + Message;
            _wnd.CurrentSynchronizationContext.Send(_ => _wnd.ShowMessage(Message), null);
            return "Returned:" + Message;
            //var form = MainWindow.CurrentInstance;
            //form.CurrentSynchronizationContext.Send(_ => form.ShowMessage(Message), null);
        }


        public string GetUserName()
        {
            var msg = String.Format("IsAuthenticated: {0}\r\nAuthenticationType: {1}\r\n Name: {2} ",
                ServiceSecurityContext.Current.PrimaryIdentity.IsAuthenticated,
                ServiceSecurityContext.Current.PrimaryIdentity.AuthenticationType,
                ServiceSecurityContext.Current.PrimaryIdentity.Name);
            //_wnd.ShowMessage(msg);
            return String.Format(msg);
        }
    }
}
