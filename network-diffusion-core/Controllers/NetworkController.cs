using Microsoft.AspNetCore.Mvc;
using network_diffusion_core.Model;
using network_diffusion_core.NetworkGenerators;
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
        [HttpPost("{modelType}/{nodesCount}")]
        public string GetNetwork(int modelType, int nodesCount, [FromBody] List<NetworkParameter> parameters)
        {
            switch (modelType)
            {
                case 0:
                    var regularNetwork = new RegularNetwork();
                    return JsonSerializer.Serialize(regularNetwork.GenerateRegularNetwork(nodesCount));
                case 1:
                    var randomNetwork = new RandomNetwork();
                    return JsonSerializer.Serialize(randomNetwork.GenerateRandomNetwork(nodesCount, parameters[0].Value));
                case 2:
                    var scaleFreeNetwork = new ScaleFreeNetwork();
                    return JsonSerializer.Serialize(scaleFreeNetwork.GenerateScaleFreeNetwork(nodesCount, (int)parameters[0].Value));
                case 3:
                    var smallWorldNetwork = new SmallWorldNetwork();
                    return JsonSerializer.Serialize(smallWorldNetwork.GenerateSmallWorldNetwork(nodesCount, parameters[0].Value));
            }
            return "";
        }

        [HttpGet("model/{modelType}")]
        public string GetModelParameters(int modelType)
        {
            var parametersList = new List<NetworkParameter>();

            switch (modelType)
            {
                case 0:
                    return JsonSerializer.Serialize(parametersList);
                case 1:
                    parametersList.Add(new NetworkParameter(0 ,"Prawdopodobieństwo połącznia krawędzi", 0.05));
                    return JsonSerializer.Serialize(parametersList);
                case 2:
                    parametersList.Add(new NetworkParameter(0, "Liczba węzłów bazowych", 5));
                    return JsonSerializer.Serialize(parametersList);
                case 3:
                    parametersList.Add(new NetworkParameter(0, "Prawdopodobieństwo przełączenia krawędzi", 0.3));
                    return JsonSerializer.Serialize(parametersList);
                default:
                    return "";

            }
        }
    }
}
