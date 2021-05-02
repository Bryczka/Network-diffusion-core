using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace network_diffusion_core.Model
{
    public class Edge
    {
        public Edge(int id, int from, int to)
        {
            EdgeId = id;
            From = from;
            To = to;
        }
        [JsonPropertyName("id")]
        [JsonProperty("id")]
        public int EdgeId { get; set; }

        [JsonPropertyName("from")]
        [JsonProperty("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        [JsonProperty("to")]
        public int To { get; set; }
    }
}
