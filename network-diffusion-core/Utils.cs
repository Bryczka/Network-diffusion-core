using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core
{
    public static class Utils
    {
        public static readonly int susceptibleId = 0;
        public static readonly string susceptibleColor = "#b4ff42";
        public static readonly string susceptibleTitle = "Susceptible";

        public static readonly int infectedId = 99;
        public static readonly string infectedColor = "#ff9999";
        public static readonly string infectedTitle = "Infected";

        public static List<Node> GetConnectedNodes(int nodeId, Network network)
        {
            return network.Edges
                .Where(x => x.From == nodeId || x.To == nodeId)
                .Select(x => x.To != nodeId ? x.To : x.From)
                .Distinct()
                .Select(x => network.Nodes
                .Find(y => y.NodeId == x))
                .Where(x => x !=null)
                .ToList();
        }

        public static Node ChangeNodeStatus(Node node, NodeState nodeState)
        {
            node.NodeStateId = nodeState.Id;
            node.Color = nodeState.Color;
            node.Title = nodeState.Title;
            return node;
        }

        public static List<Node> GenerateRandomNetworkOpinions(Network network, List<NodeState> nodeState)
        {
            var random = new Random();
            var initialNodes = new List<Node>();
            foreach (var node in network.Nodes)
            {
                if (random.NextDouble() < 0.5)
                {
                    Utils.ChangeNodeStatus(node, nodeState[0]);
                    initialNodes.Add(node);
                }
                else
                {
                    Utils.ChangeNodeStatus(node, nodeState[1]);
                    initialNodes.Add(node);
                }
            }
            network.ChangedByDiffusion = true;
            return initialNodes;
        }
    }
}
