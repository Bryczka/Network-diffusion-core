using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using network_diffusion_core.NetworkGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            /*
             * Network Tests
             */

            //var randomNetwork = new RandomNetwork();
            //var network = randomNetwork.GenerateRandomNetwork(10000, 0.5);

            //var regularNetwork = new RegularNetwork();
            //var network = regularNetwork.GenerateRegularNetwork(5);


            //var smallWorldNetwork = new SmallWorldNetwork();
            //var network = smallWorldNetwork.GenerateSmallWorldNetwork(10, 1);

            var scaleFreeNetwork = new ScaleFreeNetwork();
            var network = scaleFreeNetwork.GenerateScaleFreeNetwork(1000);

            Console.ReadKey();

            /*
             * End
             */
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
