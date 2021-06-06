using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.DiffusionModels
{
    public class SznajdModel
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
            var randomList = new List<int>();

            if (network.ChangedByDiffusion == false)
                return Utils.GenerateRandomNetworkOpinions(network, nodeStates);

            var rnd = random.Next(0, network.Nodes.Count - 1);
            var firstNode = network.Nodes.Find(x => x.NodeId == rnd);
            var connectedNodes = Utils.GetConnectedNodes(firstNode.NodeId, network);
            if (connectedNodes.Count != 0)
            {
                var secondNode = connectedNodes[random.Next(connectedNodes.Count)];
                if (firstNode.NodeStateId == secondNode.NodeStateId)
                {
                    var exposedNodes =
                        Utils.GetConnectedNodes(firstNode.NodeId, network)
                            .Concat(Utils.GetConnectedNodes(secondNode.NodeId, network))
                            .ToList();

                    exposedNodes.ForEach(x => Utils.ChangeNodeStatus(x, nodeStates.Find(x => x.Id == firstNode.NodeStateId)));
                    exposedNodes.ForEach(x => currentIterationNodes.Add(x));
                }
            }
            return network.Nodes;

        }
    }
}
