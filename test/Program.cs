using InstrumentHandlerNamespace;
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
    
    interface interf
    {
        string Name { get; set; }
    }
    class A:interf
    {
        public A(string Name)
        {
            m_name = Name;
        }
        private string m_name;
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
    }
    class B : interf
    {
        public B(string Name)
        {
            m_name = Name;
        }
        private string m_name;
        public string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
    }

    class C
    {
        private A m_a;
        private B m_b;
        private int m_int;
        public A Aprop
        {
            get { return m_a; }
            set { m_a = value; }
        }
        public B Bprop
        {
            get { return m_b; }
            set { m_b = value; }
        }
        public int MINT
        {
            get { return m_int; }
            set { m_int = value; }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            
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
