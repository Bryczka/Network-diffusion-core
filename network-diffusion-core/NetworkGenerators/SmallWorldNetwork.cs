using network_diffusion_core.Model;
using System;
using System.Linq;

namespace network_diffusion_core.NetworkGenerators
{
    public class SmallWorldNetwork
    {
        public Network GenerateSmallWorldNetwork(int nodesCount, double probability)
        {
            var random = new Random();
            var regularNetwork = new RegularNetwork();
            var generatedRegularNetwork = regularNetwork.GenerateRegularNetwork(nodesCount);
            var modifiedEdges = 0;

            while(modifiedEdges< probability * nodesCount)
            {
                var randomNodes = TwoRandomNodes();
                var firstRandomEdge = generatedRegularNetwork.Edges.Where(x => x.From == randomNodes[0]).FirstOrDefault();
                var secondRandomEdge = generatedRegularNetwork.Edges.Where(x => x.From == randomNodes[1]).FirstOrDefault();
                generatedRegularNetwork.Edges.Remove(firstRandomEdge);
                generatedRegularNetwork.Edges.Remove(secondRandomEdge);
                generatedRegularNetwork.Edges.Add(new Edge(firstRandomEdge.EdgeId, randomNodes[0], randomNodes[1]));
                modifiedEdges++;
            }



            foreach (var edge in generatedRegularNetwork.Edges)
            {
                if (random.NextDouble() < probability)
                {
                    var rnd = random.Next(nodesCount - 1);

                    //ucinamy połączenie z węzłą a
                    

                    //Czy nie pętla
                    //if (rnd == edge.From)
                    //{
                    //    //losuj ponownie
                    //}

                    ////Czy nie takie samo połączenie
                    //if (rnd == edge.To)
                    //{

                    //}
                    //Czy nie dubluje się z innym
                    //if (rnd ==)

                    //Edge selectedEdge = generatedRegularNetwork.Edges.Where(x => x.From == rnd).FirstOrDefault();
                    smallWorldNetwork.Edges.RemoveAt(1);
                    //smallWorldNetwork.Edges. generatedRegularNetwork.Edges.Where()
                    //edge.To = rnd;

                }
            }

            int[] TwoRandomNodes()
            {
                var rnd = random.Next(nodesCount - 1);
                var rnd1 = random.Next(nodesCount - 1);

                while (rnd == rnd1)
                {
                    rnd1 = random.Next(nodesCount - 1);
                }

                return new int[] { rnd, rnd1 };
            }

            return smallWorldNetwork;
        }
    }
}
