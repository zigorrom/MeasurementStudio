using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test
{
    class MySimpleHttpMessageHandler:HttpMessageHandler
    {

        private int counter = 0;
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            Console.WriteLine("Received an http message");
            var task = new Task<HttpResponseMessage>(() => {
                var msg = new HttpResponseMessage();
                var un = Thread.CurrentPrincipal.Identity.Name;
                msg.Content = new StringContent("Hello "+un+", conter = "+counter++.ToString());
                Console.WriteLine("http response sent");
                    return msg;
            });
            task.Start();
            return task;
        }
    }
}
