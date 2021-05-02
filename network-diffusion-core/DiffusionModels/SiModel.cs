using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace network_diffusion_core.DiffusionModels
{
    public class SiModel
    {
        public (List<Node> infectedNodes, List<Node> currentInfectedNodes) CalculateSimulation(Network network, List<Node> previouslyInfectedNodes, double infenctionRate)
        {
            var random = new Random();
            var exposedNodes = new HashSet<Node>();
            var infectedNodes = previouslyInfectedNodes;
            var currentInfectedNodes = new List<Node>();

            if (infectedNodes == null)
            {
                infectedNodes = new List<Node>();
                var randomNodeId = random.Next(0, network.Nodes.Count - 1);
                var firstInfectedNode = network.Nodes.Find(x => x.NodeId == randomNodeId);
                firstInfectedNode.Color = "red";
                firstInfectedNode.Title = "Infected";
                infectedNodes.Add(firstInfectedNode);
                currentInfectedNodes.Add(firstInfectedNode);
                return (infectedNodes, currentInfectedNodes);
            }

            foreach (var infectedNode in infectedNodes)
            {
                var connectedEdges = network.Edges.FindAll(x => x.From == infectedNode.NodeId || x.To == infectedNode.NodeId);
                var exposedNodesId = new List<int>();
                exposedNodesId.AddRange(connectedEdges.Where(x => x.From != infectedNode.NodeId).Select(x => x.From));
                exposedNodesId.AddRange(connectedEdges.Where(x => x.To != infectedNode.NodeId).Select(x => x.To));

                foreach (var exposedNodeId in exposedNodesId)
                {
                    exposedNodes.Add(network.Nodes.Find(x => x.NodeId == exposedNodeId));
                }
            }

            foreach (var node in exposedNodes)
            {
                if (random.NextDouble() < infenctionRate && !infectedNodes.Contains(node) && node != null)
                {
                    node.Color = "red";
                    node.Title = "Infected";
                    infectedNodes.Add(node);
                    currentInfectedNodes.Add(node);
                }
            }
            return (infectedNodes, currentInfectedNodes);
        }
    }
}
