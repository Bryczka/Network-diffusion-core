using Microsoft.AspNetCore.Mvc;
using network_diffusion_core.DiffusionModels;
using network_diffusion_core.Model;
using network_diffusion_core.NetworkGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace network_diffusion_core.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        [HttpPut("{infectionRate}/{iterationCount}")]
        public string GetSiSimultion(double infectionRate, int iterationCount, [FromBody] Network network)
        {
            var simodel = new SiModel();
            var currentInfections = new List<List<Node>>();

            var currentStep = simodel.CalculateSimulation(network, null, 0.4);

            currentInfections.Add(currentStep.currentInfectedNodes);

            for (int i = 0; i < 50; i++)
            {
                currentStep = simodel.CalculateSimulation(network, currentStep.infectedNodes, 0.4);
                currentInfections.Add(new List<Node>(currentStep.currentInfectedNodes));
            }
            return JsonSerializer.Serialize(currentInfections);
        }
    }
}
