using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using network_diffusion_core.DiffusionModels;
using network_diffusion_core.Model;
using network_diffusion_core.NetworkGenerators;
using network_diffusion_core.NetworkStatistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //Stopwatch sw = new Stopwatch();

            //sw.Start();
            var randomNetwork = new RandomNetwork();
            var network = randomNetwork.GenerateRandomNetwork(1000);
            var nsc = new NetworkStatsCounter();
            nsc.CalculateDegreeDistribution(network);
            //var regularNetwork = new RegularNetwork();
            //var network = regularNetwork.GenerateRegularNetwork(1000);

            //var smallWorldNetwork = new SmallWorldNetwork();
            //var network = smallWorldNetwork.GenerateSmallWorldNetwork(1000);

            //var scaleFreeNetwork = new ScaleFreeNetwork();
            //var network = scaleFreeNetwork.GenerateScaleFreeNetwork(1000);

            //var simodel = new SiModel();
            //var currentInfections = new List<List<Node>>();

            //var currentStep = simodel.CalculateSimulation(network, null, 0.05);

            //currentInfections.Add(currentStep.currentInfectedNodes);

            //for (int i = 0; i < 100; i++)
            //{
            //    currentStep = simodel.CalculateSimulation(network, currentStep.infectedNodes, 0.05);
            //    currentInfections.Add(new List<Node>(currentStep.currentInfectedNodes));
            //}

            //sw.Stop();

            //Console.WriteLine("Elapsed={0}", sw.Elapsed);
            //Console.ReadKey();

            //var smallWorldNetwork = new SmallWorldNetwork();
            //var network = smallWorldNetwork.GenerateSmallWorldNetwork(10, 1);

            //var scaleFreeNetwork = new ScaleFreeNetwork();
            //var network = scaleFreeNetwork.GenerateScaleFreeNetwork(1000);

            //Console.ReadKey();

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
