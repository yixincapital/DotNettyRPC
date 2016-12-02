using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpc.Console
{
    using System.Diagnostics;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using DotNetty.Codecs;
    using DotNetty.Handlers.Tls;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using rpc.core;
    using rpc.serialize;
    using Console = System.Console;

    class Program
    {
       
        static async Task RunClient()
        {
            var executor = new MessageSendExecutor("127.0.0.1", 8007, new DefaultSerializer());
            var myService = executor.Execute<IIntParameter>();
            var sw =new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10000; i++)
            {
                myService.Method(1);
            }
   
           sw.Stop();
            Console.WriteLine("一万次调用耗时:{0}毫秒",sw.ElapsedMilliseconds);
        }

        static void Main(string[] args)
        {
          
                Task.Run(() => RunClient()).Wait();
          

           
               
          
            Console.ReadLine();
        }
    }
}
