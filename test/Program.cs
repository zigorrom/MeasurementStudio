using Instruments;
using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=true,Inherited=true)]
    class A1:Attribute
    {
        public bool FitsToStr(string a)
        {
            if (a == "123")
                return true;
            return false;
        }
    }

    [A1,A1]
    class B
    { }

    class Program
    {
        static void Main(string[] args)
        {
            //var types = Assembly.GetAssembly(typeof(IInstrument)).GetTypes().Where(x => (x.IsAssignableFrom(typeof(IInstrument)))).ToList();

            var assembly = Assembly.GetAssembly(typeof(IInstrument));
            var types = assembly.GetTypes().Where(
                x=>{
                    if (x.GetCustomAttribute(typeof(InstrumentAttribute)) != null)
                        return true;
                    return false;

                });

            var manager = ResourceManager.GetLocalManager();
            var resources = manager.FindResources("(GPIB)|(USB)|(COM)?*");
            foreach (var type in types)
            {
                foreach (var resource in resources)
                {
                    var session = (MessageBasedSession)manager.Open(resource);
                    var idn = session.Query("*IDN?");
                    var attr = (InstrumentAttribute)type.GetCustomAttribute(typeof(InstrumentAttribute));
                    Console.WriteLine("Resource: {0}, type: {1}, Fit result: {2}",resource,type.Name,attr.FitsToIDN(idn));
                }
            }

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
