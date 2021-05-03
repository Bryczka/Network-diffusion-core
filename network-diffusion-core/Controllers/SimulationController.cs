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
        [HttpPost("{modelType}/{infectionRate}/{recoveryRate}/{iterationCount}")]
        public string GetSiSimultion(double infectionRate, double recoveryRate, int iterationCount, int modelType, [FromBody] Network network)
        {
            //SIS MODEL
            var simodel = new SiModel();
            var currentInfections = new List<List<Node>>();
            var currentStep = simodel.CalculateSimulation(network, null, infectionRate, recoveryRate);
            currentInfections.Add(currentStep.currentInfectedNodes);
            for (int i = 0; i < iterationCount - 1; i++)
            {
                currentStep = simodel.CalculateSimulation(network, currentStep.infectedNodes, infectionRate, recoveryRate);
                currentInfections.Add(new List<Node>(currentStep.currentInfectedNodes));
            }
            return JsonSerializer.Serialize(currentInfections);

            //var simodel = new SirModel();
            //var currentInfections = new List<List<Node>>();
            //var currentStep = simodel.CalculateSimulation(network, null, infectionRate, recoveryRate);
            //currentInfections.Add(currentStep.currentInfectedNodes);
            //for (int i = 0; i < iterationCount - 1; i++)
            //{
            //    currentStep = simodel.CalculateSimulation(network, currentStep.infectedNodes, infectionRate, recoveryRate);
            //    currentInfections.Add(new List<Node>(currentStep.currentInfectedNodes));
            //}
            //return JsonSerializer.Serialize(currentInfections);
        }
    }
}
