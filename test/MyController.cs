using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace test
{
    public class MyController : ApiController
    {
        public string Get()
        {
            return "Hello from m controller in self-hosting app";
        }
    }
}
