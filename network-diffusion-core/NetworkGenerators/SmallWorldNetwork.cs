using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace network_diffusion_core.NetworkGenerators
{
    public class SmallWorldNetwork
    {
        public Network GenerateSmallWorldNetwork(int nodesCount, double probability)
        {
            var generatedRegularNetwork = new RegularNetwork().GenerateRegularNetwork(nodesCount);
            var modifiedEdges = 0;

            while (modifiedEdges < probability * nodesCount)
            {
                var randomNodes = TwoRandomNodes(nodesCount - 1);
                while (generatedRegularNetwork.Edges.Where(x => x.From == randomNodes[0]).FirstOrDefault() == null)
                {
                    randomNodes = TwoRandomNodes(nodesCount - 1);
                };
                var firstRandomEdge = generatedRegularNetwork.Edges.Where(x => x.From == randomNodes[0]).FirstOrDefault();
                var secondRandomEdge = generatedRegularNetwork.Edges.Where(x => x.From == randomNodes[1]).FirstOrDefault();
                generatedRegularNetwork.Edges.Remove(firstRandomEdge);
                generatedRegularNetwork.Edges.Remove(secondRandomEdge);
                generatedRegularNetwork.Edges.Add(new Edge(firstRandomEdge.EdgeId, randomNodes[0], randomNodes[1]));
                modifiedEdges++;
            }

            return generatedRegularNetwork;
        }

        private int[] TwoRandomNodes(int nodesCount)
        {
            var random = new Random();
            var rnd = random.Next(nodesCount - 1);
            var rnd1 = random.Next(nodesCount - 1);

            while (rnd == rnd1)
            {
                rnd1 = random.Next(nodesCount - 1);
            }
            return new int[] { rnd, rnd1 };
        }
    }
}
