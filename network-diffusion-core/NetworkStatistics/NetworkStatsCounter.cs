using network_diffusion_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.NetworkStatistics
{
    public static class NetworkStatsCounter
    {        
        //Średni stopień i rozkład stopnii
        public static (double MeanDegree, List<(int Degree, int Count)> DegreeHistogram, List<int> NodesDegree) CalculateDegreeStatistics(Network network)
        {
            var degreesList = new List<int>();
            foreach (var node in network.Nodes)
            {
                var degree = network.Edges.FindAll(x => x.From == node.NodeId || x.To == node.NodeId).Count;
                degreesList.Add(degree);
            }
            double meanDegree = (double)degreesList.Sum() / network.Nodes.Count;
            var degreeHistogram = degreesList.GroupBy(i => i).Select(x => (x.Key, x.Count())).OrderBy(x => x.Key).ToList();
            return (meanDegree, degreeHistogram, degreesList);
        }

        //Średni współczynnik gronowania i rozkład współczynnika

        public static (double MeanClusteringRate, List<(double CluseringRate, int Count)>, List<double> NodesClusteringRate) CalculateClusteringRateStatistics(Network network)
        {
            var clusteringRatesList = new List<double>();

            foreach (var node in network.Nodes)
            {
                var nodesId = new List<int>();
                double degree = network.Edges.FindAll(x => x.From == node.NodeId || x.To == node.NodeId).Count;
                var connectedEdges = network.Edges.FindAll(x => x.From == node.NodeId || x.To == node.NodeId);
                nodesId.AddRange(connectedEdges.Where(x => x.From != node.NodeId).Select(x => x.From));
                nodesId.AddRange(connectedEdges.Where(x => x.To != node.NodeId).Select(x => x.To));
                var connectionCount = network.Edges.Where(x => nodesId.Contains(x.From) && nodesId.Contains(x.To)).Count();
                clusteringRatesList.Add(2 * connectionCount / degree * (degree - 1));
            }
            var clusteringRateHistogram = clusteringRatesList.GroupBy(i => i).Select(x => (x.Key, x.Count())).OrderBy(x => x.Key).ToList();
            double meanClusteringRate = (double) clusteringRatesList.Sum() /  network.Nodes.Count;
            return (meanClusteringRate, clusteringRateHistogram, clusteringRatesList);
        }

        //** Wyrysowanie na sieci grafik z wiki (miary centralności)

    }
}
