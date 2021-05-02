using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace network_diffusion_core.Model
{
    public class Network
    {
        public Network(List<Node> nodes, List<Edge> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }
        public Network()
        {

        }

        [JsonPropertyName("nodes")]
        [JsonProperty("nodes")]
        public List<Node> Nodes { get; set; }

        [JsonPropertyName("edges")]
        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }
    }
}
