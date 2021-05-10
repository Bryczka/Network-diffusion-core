using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.DiffusionModels
{
    public class VoterModel
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
                return Utils.GenerateRandomNetworkOpinions(network, nodeStates);

            var rnd = random.Next(0, network.Nodes.Count - 1);
            var node = network.Nodes.Find(x => x.NodeId == rnd);
            var connectedNodes = Utils.GetConnectedNodes(node.NodeId, network);
            var secondNode = connectedNodes[random.Next(connectedNodes.Count)];

            Utils.ChangeNodeStatus(node, nodeStates.Find(x => x.Id == secondNode.NodeStateId));
            currentIterationNodes.Add(node);
            return network.Nodes;

        }
    }
}
