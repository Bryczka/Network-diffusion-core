using System;
using System.Collections.Generic;
using network_diffusion_core.Model;

namespace network_diffusion_core.NetworkGenerators
{
    public class RandomNetwork
    {
        public Network GenerateRandomNetwork(int nodesCount, double probability)
        {
            List<Node> generatedNodes = new();
            List<Edge> generatedEdges = new();
            int edgeId = 0;
            var random = new Random();

            for (int i = 0; i < nodesCount; i++)
            {
                generatedNodes.Add(new Node(i, Utils.susceptibleColor, Utils.susceptibleTitle, Utils.susceptibleId));

                for (int j = i + 1; j < nodesCount; j++)
                {
                    if (random.NextDouble() < probability)
                    {
                        generatedEdges.Add(new Edge(edgeId, i, j));
                        edgeId++;
                    }
                }
            }
            
            return new Network(generatedNodes, generatedEdges);
        }
    }
}
