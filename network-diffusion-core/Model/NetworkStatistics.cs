using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace network_diffusion_core.Model
{
    public class NetworkStatistics
    {
        public NetworkStatistics(double meanDegree, double meanClusteringRate)
        {
            MeanDegree = meanDegree;
            MeanClusteringRate = meanClusteringRate;
        }

        [JsonPropertyName("meanDegree")]
        public double MeanDegree { get; set; }

        [JsonPropertyName("meanClusteringRate")]
        public double MeanClusteringRate { get; set; }
    }
}
