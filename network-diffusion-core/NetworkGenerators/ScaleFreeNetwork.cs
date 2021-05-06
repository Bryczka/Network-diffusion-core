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

            for (int i = 0; i < nodesCount - 1; i++)
            {
                if (i < baseNodesCount)
                {
                    generatedNodes.Add(new Node(nodeId++, Utils.susceptibleColor, Utils.susceptibleTitle, Utils.susceptibleId));
                    if (i != 0)
                    {
                        generatedEdges.Add(new Edge(edgeId++, i, 0));
                    }
                }
                else
                {
                    generatedNodes.Add(new Node(nodeId, Utils.susceptibleColor, Utils.susceptibleTitle, Utils.susceptibleId));
                    generatedEdges.Add(new Edge(edgeId++, nodeId++,
                        SelectNode(CountNetworkConnectionPropability(generatedEdges, generatedNodes))));
                }

            }
            return new Network(generatedNodes, generatedEdges);
        }

        private List<(int NodeId, double Propability)> CountNetworkConnectionPropability(List<Edge> generatedEdges, List<Node> generatedNodes)
        {
            List<(int NodeId, double Propability)> nodesSelectingPropability = new();
            foreach (var node in generatedNodes)
            {
                if (node.NodeId != 0)
                {
                    nodesSelectingPropability.Add((node.NodeId,
                        (double)(generatedEdges.FindAll(x => x.To == node.NodeId).Count
                        + generatedEdges.FindAll(x => x.From == node.NodeId).Count) / generatedEdges.Count));
                }
            }
            return nodesSelectingPropability;
        }

        private int SelectNode(List<(int NodeId, double Propability)> nodesSelectingPropability)
        {
            Random random = new();
            var rnd = random.NextDouble() * nodesSelectingPropability.Sum(x => x.Propability);
            double sum = 0;
            foreach (var item in nodesSelectingPropability)
            {
                sum += item.Propability;
                if (sum > rnd)
                {
                    return item.NodeId;
                }
            }
            return -1;
        }
    }
}
