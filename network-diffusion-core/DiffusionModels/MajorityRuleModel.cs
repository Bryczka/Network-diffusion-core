using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.DiffusionModels
{
    public class MajorityRuleModel
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
            var groupSize = 10;

            if (network.ChangedByDiffusion == false)
                return Utils.GenerateRandomNetworkOpinions(network, nodeStates);

            while (randomList.Count < groupSize)
            {
                var rnd = random.Next(0, network.Nodes.Count - 1);
                if (!randomList.Contains(rnd))
                {
                    randomList.Add(rnd);
                }
            }

            var selectedNodes = randomList.Select(x => network.Nodes.Find(y => y.NodeId == x)).ToList();

            if (selectedNodes.Average(x => x.NodeStateId) < 0.5)
            {
                selectedNodes.ForEach(x => Utils.ChangeNodeStatus(x, nodeStates[0]));
            }
            else
            {
                selectedNodes.ForEach(x => Utils.ChangeNodeStatus(x, nodeStates[1]));
            }

            selectedNodes.ForEach(x => currentIterationNodes.Add(x));
            return currentIterationNodes;

        }
    }
}
