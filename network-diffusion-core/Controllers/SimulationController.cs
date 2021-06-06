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
        [HttpPost("{iterationsCount}/{modelType}")]
        public string GetSiSimultion(int iterationsCount, int modelType, [FromBody] 
        (Network network, List<NodeState> nodesStates) networkWithNodeStates)
        {
            if (modelType == 6) //VOTER
            {
                return JsonSerializer.Serialize(new VoterModel().CalculateSimulation(networkWithNodeStates.network, networkWithNodeStates.nodesStates, iterationsCount));
            }
            else if (modelType == 7) //Majority Rule
            {
                return JsonSerializer.Serialize(new MajorityRuleModel().CalculateSimulation(networkWithNodeStates.network, networkWithNodeStates.nodesStates, iterationsCount));
            }
            else if (modelType == 8) //Sznajd
            {
                return JsonSerializer.Serialize(new SznajdModel().CalculateSimulation(networkWithNodeStates.network, networkWithNodeStates.nodesStates, iterationsCount));
            }
            else //Epidemic models
            {
                return JsonSerializer.Serialize(new EpidemicModels().CalculateSimulation(networkWithNodeStates.network, networkWithNodeStates.nodesStates, iterationsCount));
            }
        }

        [HttpGet("model/{modelType}")]
        public string GetModelParameters(int modelType)
        {
            var stateList = new List<NodeState>();

            switch (modelType)
            {
                case 0:
                    stateList.Add(new NodeState(0, "Podatny", "green", 0.2, null, null));
                    stateList.Add(new NodeState(1, "Podatny", "green", null, 0, null));
                    stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 1, true));
                    return JsonSerializer.Serialize(stateList);
                case 1:
                    stateList.Add(new NodeState(0, "Podatny", "green", 0.2, 2, true));
                    stateList.Add(new NodeState(1, "Podatny", "green", null, 0, null));
                    stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 1, true));
                    return JsonSerializer.Serialize(stateList);
                case 2:
                    stateList.Add(new NodeState(0, "Podatny", "green", 0.2, null, null));
                    stateList.Add(new NodeState(1, "Podatny", "green", null, 0, null));
                    stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 1, true));
                    stateList.Add(new NodeState(3, "Uodporniony", "blue", 0.3, 2, false));
                    return JsonSerializer.Serialize(stateList);
                case 3:
                    stateList.Add(new NodeState(0, "Podatny", "green", 0.2, 3, true));
                    stateList.Add(new NodeState(1, "Podatny", "green", null, 0, null));
                    stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 1, true));
                    stateList.Add(new NodeState(3, "Uodporniony", "blue", 0.3, 2, false));
                    return JsonSerializer.Serialize(stateList);
                case 4:
                    stateList.Add(new NodeState(0, "Podatny", "green", 0.2, null, null));
                    stateList.Add(new NodeState(1, "Podatny", "green", null, 0, null));
                    stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 3, true));
                    stateList.Add(new NodeState(3, "W okresie inkubacji", "darkgrey", 0.2, 1, true));
                    stateList.Add(new NodeState(4, "Uodporniony", "blue", 0.3, 2, false));
                    return JsonSerializer.Serialize(stateList);
                case 5:
                    stateList.Add(new NodeState(0, "Podatny", "green", 0.2, 4, true));
                    stateList.Add(new NodeState(1, "Podatny", "green", null, 0, null));
                    stateList.Add(new NodeState(2, "Zainfekowany", "red", 0.4, 3, true));
                    stateList.Add(new NodeState(3, "W okresie inkubacji", "darkgrey", 0.2, 1, true));
                    stateList.Add(new NodeState(4, "Uodporniony", "blue", 0.3, 2, false));
                    return JsonSerializer.Serialize(stateList);
                case 6:
                    stateList.Add(new NodeState(0, "Opinia A", "yellow", null, 0, null));
                    stateList.Add(new NodeState(1, "Opinia B", "lightblue", null, 0, null));
                    return JsonSerializer.Serialize(stateList);
                case 7:
                    stateList.Add(new NodeState(0, "Opinia A", "yellow", null, 0, null));
                    stateList.Add(new NodeState(1, "Opinia B", "lightblue", null, 0, null));
                    return JsonSerializer.Serialize(stateList);
                case 8:
                    stateList.Add(new NodeState(0, "Opinia A", "yellow", null, 0, null));
                    stateList.Add(new NodeState(1, "Opinia B", "lightblue", null, 0, null));
                    return JsonSerializer.Serialize(stateList);
                default:
                    return "";

            }
        }
    }
}
