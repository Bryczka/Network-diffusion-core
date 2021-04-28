using network_diffusion_core.Model;
using System.Collections.Generic;

namespace network_diffusion_core.NetworkGenerators
{
    public class RegularNetwork
    {
        public Network GenerateRegularNetwork(int nodesCount)
        {
            List<Node> generatedNodes = new();
            List<Edge> generatedEdges = new();
            int edgeId = 0;

            for (int i = 0; i < nodesCount; i++)
            {
                generatedNodes.Add(new Node(i, "black", "none"));
                generatedEdges.AddRange(
                new Edge[]
                {
                        new Edge(edgeId, i, i + 1),
                        new Edge(edgeId + 1, i, i + 2)
                });
                edgeId += 2;
            }

            generatedEdges.AddRange(
                    new Edge[]
                    {
                        new Edge(edgeId ,nodesCount - 2, nodesCount -1),
                        new Edge(edgeId +1, nodesCount - 2, 0),
                        new Edge(edgeId +2, nodesCount - 1, 0),
                        new Edge(edgeId + 3,nodesCount - 1, 1)
                    });
            
            return new Network(generatedNodes, generatedEdges);
        }
    }
}
