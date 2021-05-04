using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace network_diffusion_core.DiffusionModels
{
    public class SirModel
    {
        public (List<Node> infectedNodes, List<Node> currentInfectedNodes) CalculateSimulation(Network network, List<Node> changedNodes, double infenctionRate, double recoveryRate)
        {
            var random = new Random();
            var exposedNodes = new HashSet<Node>();
            var nodesToUpdate = new List<Node>();

            //jeśli pierwszy węzeł
            if (changedNodes == null )
            {
                changedNodes = new List<Node>();
                var firstInfectedNode = network.Nodes.Find(x => x.NodeId == random.Next(0, network.Nodes.Count - 1));
                firstInfectedNode.Color = Utils.susceptibleColor;
                firstInfectedNode.Title = Utils.susceptibleTitle;
                firstInfectedNode.NodeStatus = NodeStatus.Susceptible;
                nodesToUpdate.Add(new Node(firstInfectedNode.NodeId, Utils.infectedColor, Utils.infectedTitle, NodeStatus.Infected));
                changedNodes.Add(firstInfectedNode);
                return (changedNodes, nodesToUpdate);
            }

            //szukanie podatnych sąsiadów
            foreach (var infectedNode in changedNodes)
            {
                if (infectedNode.NodeStatus == NodeStatus.Recovered)
                {
                    continue;
                }

                var connectedEdges = network.Edges.FindAll(x => x.From == infectedNode.NodeId || x.To == infectedNode.NodeId);
                var exposedNodesId = new List<int>();

                exposedNodesId.AddRange(connectedEdges.Where(x => x.From != infectedNode.NodeId).Select(x => x.From));
                exposedNodesId.AddRange(connectedEdges.Where(x => x.To != infectedNode.NodeId).Select(x => x.To));

                foreach (var exposedNodeId in exposedNodesId)
                {
                    exposedNodes.Add(network.Nodes.Find(x => x.NodeId == exposedNodeId));
                }
            }

            for (int i = 0; i < changedNodes.Count; i++)
            {
                if (random.NextDouble() < recoveryRate)
                {
                    changedNodes[i].Color = Utils.recoveredColor;
                    changedNodes[i].Title = Utils.recoveredTitle;
                    changedNodes[i].NodeStatus = NodeStatus.Recovered;
                    nodesToUpdate.Add(new Node(changedNodes[i].NodeId, Utils.recoveredColor, Utils.recoveredTitle, NodeStatus.Recovered));
                }
            }

            //losowanie nowych zachorowań z sąsiadów
            foreach (var node in exposedNodes)
            {
                if (random.NextDouble() < infenctionRate && !changedNodes.Contains(node) && node != null)
                {
                    nodesToUpdate.Add(new Node(node.NodeId, Utils.infectedColor, Utils.infectedTitle, NodeStatus.Infected));
                    node.Color = Utils.susceptibleColor;
                    node.Title = Utils.susceptibleTitle;
                    node.NodeStatus = NodeStatus.Susceptible;
                    changedNodes.Add(node);
                }

            }

            return (changedNodes, nodesToUpdate);
        }
    }
}

