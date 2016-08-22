
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http;
using Hioki3432;
using Instruments;
using System.ServiceModel;
using MeasurementStudioWebApi;
using System.Threading;
using AgilentU2442A_IVIdriver;

namespace test
{



    class Program
    {
        static void Main(string[] args)
        {

            AgilentU2542A a = new AgilentU2542A("agilent", "a", "USB0::0x0957::0x1718::TW52524501::INSTR");
            var ch = a.GetAnalogInputChannel(ChannelEnum.AI_CH101);
            ch.ChannelEnable = true;
           
            Thread.Sleep(1000);
            a.StopAquisition();
            Console.WriteLine(ch.count);
            //var cts = new CancellationTokenSource();

            //var ct = cts.Token;

            //var task = new Task(() =>
            //{
            //    var i = 0;
            //    while (!ct.IsCancellationRequested)
            //        Console.WriteLine(i++);
            //    Console.WriteLine("Finished");
            //}, ct);
            //task.Start();
            //Console.WriteLine("Started");
            
            //Thread.Sleep(5000);
            //Console.WriteLine("CancellationRequested");
            //cts.Cancel();
            
           // Buffer.BlockCopy()
            //for (uint i = uint.MaxValue-2, a=0; a<5; i++,a++)
            //{
            //    Console.WriteLine(i);
            //}

            //var _host = new ServiceHost(typeof(Service));
            //_host.Open();
            //Console.ReadLine();
            //var h = new Hioki3532("","","COM8", 9600, System.IO.Ports.Parity.None, 7, System.IO.Ports.StopBits.One, System.IO.Ports.Handshake.None, Delimiter.CR_LF);


            //var config = new MyConfig("http://localhost:8999"); //new HttpSelfHostConfiguration("http://localhost:8999");
            //var messageHandler = new MySimpleHttpMessageHandler();
            ////config.Routes.MapHttpRoute("default", "api/{controller}/{id}",new { id = RouteParameter.Optional });
            //var server = new HttpSelfHostServer(config, new MySimpleHttpMessageHandler());
            //var task = server.OpenAsync();
            //task.Wait();
            //Console.WriteLine("Server is up and running");
            //Console.ReadLine();

            //Console.WriteLine(Environment.UserName);
            //Console.ReadKey();
            //Console.WriteLine(Convert.ToString(3-1,2));
            //Console.WriteLine(Instruments.ActualInstruments.AgilentU2442A.AgilentU2542A.CounterFunctionEnum.Frequency);
            //Instruments.ActualInstruments.AgilentU2442A.AgilentU2542A a = new Instruments.ActualInstruments.AgilentU2442A.AgilentU2542A("asd", "fsdf", "dsafsd");
            //Console.WriteLine(a.APPLyQuery("201"));
            //Console.WriteLine(a.APPLyQuery("201","202"));
            //var a = InstrumentHandler.Instance;
            //var c = new C();
            //var prop = c.GetType().GetProperties();
            //foreach (var p in prop)
            //{
            //    var cond = typeof(interf).IsAssignableFrom(p.PropertyType);
            //    Console.WriteLine("property type: {0}\n\rnessecary type: {1}\n\rfits{2}", p.PropertyType, typeof(interf), cond);//p.PropertyType.IsAssignableFrom(typeof(interf)));
            //    if (cond)
            //        p.SetValue(c, new A("sdasd"));
            //}
            //var types = Assembly.GetAssembly(typeof(IInstrument)).GetTypes().Where(x => (x.IsAssignableFrom(typeof(IInstrument)))).ToList();

            //var assembly = Assembly.GetAssembly(typeof(IInstrument));
            //var types = assembly.GetTypes().Where(
            //    x=>{
            //        if (x.GetCustomAttribute(typeof(InstrumentAttribute)) != null)
            //            return true;
            //        return false;

            //    });

            //var manager = ResourceManager.GetLocalManager();
            //var resources = manager.FindResources("(GPIB)|(USB)|(COM)?*INSTR");
            //foreach (var type in types)
            //{
            //    foreach (var resource in resources)
            //    {
            //        var session = (MessageBasedSession)manager.Open(resource);
            //        var idn = session.Query("*IDN?");
            //        var attr = (InstrumentAttribute)type.GetCustomAttribute(typeof(InstrumentAttribute));
            //        Console.WriteLine("Resource: {0}, type: {1}, Fit result: {2}",resource,type.Name,attr.FitsToIDN(idn));
            //    }
            //}

            //var b = new B();
            //var c = new C();
            //if(b is B)
            //    Console.WriteLine(true);
            //var p = b.GetType().GetProperties(BindingFlags.GetProperty);
            //foreach (var pr in p)
            //{
                
            //}
            //B b = new B();
            //var attrs = b.GetType().GetCustomAttributes(typeof(A1), false);
            //foreach (A1 attr in attrs)
            //{
            //    Console.WriteLine(attr.FitsToStr("123"));
            //}
            //var handler = InstrumentHandlerNamespace.InstrumentHandler.Instance;
            //handler.DiscoverInstruments();

            //var a = Assembly.GetAssembly(typeof(int));
            //var types = a.GetTypes();
            //foreach (var type in types)
            //{
            //    Console.WriteLine(type.IsAssignableFrom(typeof(InstrumentAbstractionModel.IInstrument)));
            //}

            Console.ReadKey();
        }
    
    }
}
