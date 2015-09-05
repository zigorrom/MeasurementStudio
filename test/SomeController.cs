using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace test
{
    public class SomeController:ApiController
    {
        public string Get(string id)
        {
            return String.Concat("Some controller meet id: ", id);
        }
        public string Get()
        {
            return "Hello from some controller in self-hosting app";
        }
    }
}
