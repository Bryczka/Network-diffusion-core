using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace network_diffusion_core.DiffusionModels
{
    public class EpidemicModels
    {
        public List<List<Node>> CalculateSimulation(Network network, List<NodeState> nodeStates, int iterationCount)
        {
            var random = new Random();
            var currentIterationChanges = new List<List<Node>>();
            for (int i = 0; i < iterationCount; i++)
            {
                currentIterationChanges
                    .Add(BaseIteration(network, nodeStates, random)
                            .ConvertAll(x => new Node(x.NodeId, x.Color, x.Title, x.NodeStateId)));
            }

            return currentIterationChanges;
        }

        private List<Node> BaseIteration(Network network, List<NodeState> nodeStates, Random random)
        {
            var currentIterationNodes = new List<Node>();
            if (network.ChangedByDiffusion == false)
            {
                network.ChangedByDiffusion = true;
                var rnd = random.Next(0, network.Nodes.Count - 1);
                var firstNode = network.Nodes
                    .Find(x => x.NodeId == rnd);
                Utils.ChangeNodeStatus(firstNode, nodeStates[2]);
                currentIterationNodes.Add(firstNode);
                return network.Nodes;
            }

            nodeStates
                .Where(x => x.BeforeInfection == false)
                .ToList()
                .ForEach(x => UpdateNodesState(network, currentIterationNodes, x));

            network.Nodes
                .Where(x => x.NodeStateId == nodeStates[1].Id)
                .ToList()
                .ForEach(x => Utils.ChangeNodeStatus(x, nodeStates[0]));

            network.Nodes
                .Where(x => x.NodeStateId == nodeStates[2].Id)
                .ToList()
                .ForEach(x => Utils.GetConnectedNodes(x.NodeId, network)
                .Where(x => x.NodeStateId == nodeStates[0].Id)
                .ToList()
                .ForEach(x => Utils.ChangeNodeStatus(x, nodeStates[1])));

            nodeStates
                .Where(x => x.BeforeInfection == true)
                .ToList()
                .ForEach(x => UpdateNodesState(network, currentIterationNodes, x));

            return network.Nodes;
        }

        private static void UpdateNodesState(Network network, List<Node> currentIterationChanges, NodeState state)
        {
            var random = new Random();
            currentIterationChanges.AddRange(network.Nodes
                .Where(x => x.NodeStateId == state.PreviousStateId)
                .Where(x => random.NextDouble() < state.ChangeToPropabilityRate)
                .Select(x => Utils.ChangeNodeStatus(x, state))
                .ToList());
            var a = 321;
        }
    }
}
