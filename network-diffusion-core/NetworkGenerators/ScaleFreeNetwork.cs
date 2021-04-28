using network_diffusion_core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.NetworkGenerators
{
    public class ScaleFreeNetwork
    {
        public Network GenerateScaleFreeNetwork(int nodesCount)
        {
            int baseNodesCount = 5;
            List<Node> generatedNodes = new();
            List<Edge> generatedEdges = new();
            int edgeId = 0;
            int nodeId = 0;

            for (int i = 0; i < baseNodesCount - 1; i++)
            {
                generatedNodes.Add(new Node(nodeId++, "black", "none"));
                generatedEdges.Add(new Edge(edgeId++, i, i + 1));
            }
            generatedNodes.Add(new Node(nodeId++, "black", "none"));
            generatedEdges.Add(new Edge(edgeId, baseNodesCount - 1, 0));

            for (int i = 0; i < nodesCount - baseNodesCount; i++)
            {
                var nodesSelectingPropability = CountNetworkConnectionPropability(generatedEdges, generatedNodes);
                for (int j = 0; j < 3; j++)
                {
                    generatedEdges.Add(new Edge(edgeId++, nodeId, nodesSelectingPropability[j].NodeId));
                }
                generatedNodes.Add(new Node(nodeId++, "red", "none"));
            }

            return new Network(generatedNodes, generatedEdges);
        }

        private List<(int NodeId, double Propability)> CountNetworkConnectionPropability(List<Edge> generatedEdges, List<Node> generatedNodes)
        {
            List<(int NodeId, double Propability)> nodesSelectingPropability = new();
            foreach (var node in generatedNodes)
            {
                nodesSelectingPropability.Add((node.NodeId,
                    (generatedEdges.FindAll(x => x.To == node.NodeId).Count + generatedEdges.FindAll(x => x.From == node.NodeId).Count) / generatedEdges.Count));
            }
            nodesSelectingPropability.OrderBy(x => x.Propability);
            return nodesSelectingPropability;
        }

    }
}
