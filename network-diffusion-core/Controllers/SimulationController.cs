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
        [HttpPost("{modelType}/{iterationCount}")]
        public string GetSiSimultion(int iterationCount, int modelType, [FromBody] Network network)
        {
            var stateList = new List<NodeState>();

            if (modelType == 0)//SI MODEL
            {
                stateList.Add(new NodeState(0, "Podatny", "green", 0.2, null, null));
                stateList.Add(new NodeState(1, "Po kontakcie", "yellow", 0.2, 0, null));
                stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 1, true));
                return JsonSerializer.Serialize(new EpidemicModels().CalculateSimulation(network, stateList, iterationCount));
            }
            else if (modelType == 1)//SIS MODEL
            {
                stateList.Add(new NodeState(0, "Podatny", "green", 0.2, 2, true));
                stateList.Add(new NodeState(1, "Po kontakcie", "yellow", 0.2, 0, null));
                stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 1, true));
                return JsonSerializer.Serialize(new EpidemicModels().CalculateSimulation(network, stateList, iterationCount));
            }
            else if (modelType == 2)//SIR MODEL
            {
                stateList.Add(new NodeState(0, "Podatny", "green", 0.2, null, null));
                stateList.Add(new NodeState(1, "Po kontakcie", "yellow", 0.2, 0, null));
                stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 1, true));
                stateList.Add(new NodeState(3, "Uodporniony", "blue", 0.3, 2, false));
                return JsonSerializer.Serialize(new EpidemicModels().CalculateSimulation(network, stateList, iterationCount));
            }
            else if (modelType == 3)//SIRS
            {
                stateList.Add(new NodeState(0, "Podatny", "green", 0.2, 3, true));
                stateList.Add(new NodeState(1, "Po kontakcie", "yellow", 0.2, 0, null));
                stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 1, true));
                stateList.Add(new NodeState(3, "Uodporniony", "blue", 0.3, 2, false));
                return JsonSerializer.Serialize(new EpidemicModels().CalculateSimulation(network, stateList, iterationCount));
            }
            else if (modelType == 4)//SEIR
            {
                stateList.Add(new NodeState(0, "Podatny", "green", 0.2, null, null));
                stateList.Add(new NodeState(1, "Po kontakcie", "yellow", 0.2, 0, null));
                stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 3, true));
                stateList.Add(new NodeState(3, "W okresie inkubacji", "darkgrey", 0.2, 1, true));
                stateList.Add(new NodeState(4, "Uodporniony", "blue", 0.3, 2, false));
                return JsonSerializer.Serialize(new EpidemicModels().CalculateSimulation(network, stateList, iterationCount));
            }
            else if (modelType == 5)//SEIRS
            {
                stateList.Add(new NodeState(0, "Podatny", "green", 0.2, 4, true));
                stateList.Add(new NodeState(1, "Po kontakcie", "yellow", 0.2, 0, null));
                stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 3, true));
                stateList.Add(new NodeState(3, "W okresie inkubacji", "darkgrey", 0.2, 1, true));
                stateList.Add(new NodeState(4, "Uodporniony", "blue", 0.3, 2, false));
                return JsonSerializer.Serialize(new EpidemicModels().CalculateSimulation(network, stateList, iterationCount));
            }
            else if (modelType == 6)//VOTER
            {
                stateList.Add(new NodeState(0, "Opinia A", "yellow", 0, 0, null));
                stateList.Add(new NodeState(1, "Opinia B", "lightblue", 0, 0, null));
                return JsonSerializer.Serialize(new VoterModel().CalculateSimulation(network, stateList, iterationCount));
            }
            else if (modelType == 7) //Majority Rule
            {
                stateList.Add(new NodeState(0, "Opinia A", "yellow", 0, 0, null));
                stateList.Add(new NodeState(1, "Opinia B", "lightblue", 0, 0, null));
                return JsonSerializer.Serialize(new MajorityRuleModel().CalculateSimulation(network, stateList, iterationCount));
            }
            else
            {
                stateList.Add(new NodeState(0, "Opinia A", "yellow", 0, 0, null));
                stateList.Add(new NodeState(1, "Opinia B", "lightblue", 0, 0, null));
                return JsonSerializer.Serialize(new SznajdModel().CalculateSimulation(network, stateList, iterationCount));
            }
        }
    }
}
