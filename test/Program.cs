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
            B b = new B();
            var attrs = b.GetType().GetCustomAttributes(typeof(A1), false);
            foreach (A1 attr in attrs)
            {
                Console.WriteLine(attr.FitsToStr("123"));
            }
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
