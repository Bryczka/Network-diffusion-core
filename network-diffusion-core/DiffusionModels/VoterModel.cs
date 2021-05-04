using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.DiffusionModels
{
    public class VoterModel
    {
        //public List<Node> CalculateSimulation(Network network, List<Node> changedNodes)
        //{

            
        //}

        public List<Node> PrepareInitialState(Network network)
        {
            var random = new Random();
            var initialNodes = new List<Node>();
            foreach (var node in network.Nodes)
            {
                if (random.NextDouble() < 0.5)
                {
                    node.Color = Utils.infectedColor;
                    node.Title = Utils.infectedTitle;
                    node.NodeStatus = NodeStatus.Infected;
                    initialNodes.Add(node);
                }
            }

            return initialNodes;
        }
    }
}
