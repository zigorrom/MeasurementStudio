using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    interface a
    {
        void a_meth();
    }

    class b:a
    {
        public void a_meth()
        {
            Console.WriteLine("a_meth from b class");
        }
        public void b_meth()
        {
            Console.WriteLine("b_meth from b class");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new b();
            Method<b>(obj);
            Console.ReadKey();
        }
        static void Method<T>(a obj)
        {
            var bobj = (T)Convert.ChangeType(obj, typeof(T));
           
            //if (obj is b)
            //{
            //    var bobj = (b)obj;
            //    bobj.a_meth();
            //    bobj.b_meth();
            //}
            //else
            //    obj.a_meth();
        }
    }
}
