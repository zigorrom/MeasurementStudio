using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class MySimpleHttpMessageHandler:HttpMessageHandler
    {
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            Console.WriteLine("Received an http message");
            var task = new Task<HttpResponseMessage>(() => {
                var msg = new HttpResponseMessage();
                msg.Content = new StringContent("Hello self hosting");
                Console.WriteLine("http response sent");
                    return msg;
            });
            task.Start();
            return task;
        }
    }
}
