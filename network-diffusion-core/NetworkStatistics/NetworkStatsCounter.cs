using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.NetworkStatistics
{
    public class NetworkStatsCounter
    {
        //Średni stopień
        //Liczba zdrowych
        //Liczba zarażonych
        //Rozkład stopni
        //Rozkład współczynnika gronowania
        
        public double CalculateMeanDegree(Network network)
        {
            var degreeSum = 0;
            foreach (var node in network.Nodes)
            {
                degreeSum += network.Edges.FindAll(x => x.From == node.NodeId || x.To == node.NodeId).Count;
            }

            return degreeSum/network.Nodes.Count;
        }

        public List<(int Degree , int Count)> CalculateDegreeDistribution (Network network)
        {
            var degreesList = new List<int>();
            foreach (var node in network.Nodes)
            {
                degreesList.Add(network.Edges.FindAll(x => x.From == node.NodeId || x.To == node.NodeId).Count);
            }

            var degreeHistogram = degreesList.GroupBy(i => i).Select(x => ( x.Key, x.Count() )).OrderBy(x=>x.Key).ToList();
            return degreeHistogram;
        }

        //** Wyrysowanie na sieci grafik z wiki (miary centralności)
    }
}
