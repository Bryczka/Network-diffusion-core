using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace network_diffusion_core.DiffusionModels
{
    public class SirModel
    {
        public (List<Node> infectedNodes, List<Node> currentInfectedNodes) CalculateSimulation(Network network, List<Node> infectedNodes, double infenctionRate, double recoveryRate)
        {
            var random = new Random();
            var exposedNodes = new HashSet<Node>();
            var nodesToUpdate = new List<Node>();
            var recoveredNodes = new HashSet<Node>();
            

            //jeśli pierwszy węzeł
            if (infectedNodes == null)
            {
                infectedNodes = new List<Node>();
                var randomNodeId = random.Next(0, network.Nodes.Count - 1);
                var firstInfectedNode = network.Nodes.Find(x => x.NodeId == randomNodeId);
                nodesToUpdate.Add(new Node(firstInfectedNode.NodeId, Utils.infectedColor, Utils.infectedTitle));
                infectedNodes.Add(firstInfectedNode);
                return (infectedNodes, nodesToUpdate);
            }

            //szukanie podatnych sąsiadów
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

            //foreach (var node in infectedNodes)
            //{
                for (int i = 0; i < infectedNodes.Count; i++)
                    if (random.NextDouble() < recoveryRate)
                    {
                        var recoveredNode = new Node(infectedNodes[i].NodeId, Utils.recoveredColor, Utils.recoveredTitle);
                        nodesToUpdate.Add(recoveredNode);
                        recoveredNodes.Add(recoveredNode);
                        infectedNodes.Remove(infectedNodes[i]);
                    }
            //}

            //losowanie nowych zachorowań z sąsiadów
            foreach (var node in exposedNodes)
            {
                //if (recoveredNodes.Contains(node))
                //{
                    if (random.NextDouble() < infenctionRate && !infectedNodes.Contains(node) && node != null)
                    {
                        nodesToUpdate.Add(new Node(node.NodeId, Utils.infectedColor, Utils.infectedTitle));
                        infectedNodes.Add(node);
                    }
                //}
            }

            return (infectedNodes, nodesToUpdate);
        }
    }
}

