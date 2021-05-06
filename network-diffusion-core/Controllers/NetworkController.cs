using Microsoft.AspNetCore.Mvc;
using network_diffusion_core.NetworkGenerators;
using network_diffusion_core.NetworkStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace network_diffusion_core.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        [HttpGet("{type}/{nodesCount}")]
        public string Get(int type, int nodesCount)
        {
            switch (type)
            {
                case 0:
                    var regularNetwork = new RegularNetwork();
                    return JsonSerializer.Serialize(regularNetwork.GenerateRegularNetwork(nodesCount));
                case 1:
                    var randomNetwork = new RandomNetwork();
                    return JsonSerializer.Serialize(randomNetwork.GenerateRandomNetwork(nodesCount));
                case 2:
                    var scaleFreeNetwork = new ScaleFreeNetwork();
                    return JsonSerializer.Serialize(scaleFreeNetwork.GenerateScaleFreeNetwork(nodesCount));
                case 3:
                    var smallWorldNetwork = new SmallWorldNetwork();
                    return JsonSerializer.Serialize(smallWorldNetwork.GenerateSmallWorldNetwork(nodesCount));
            }
            return "";
        }
    }
}
