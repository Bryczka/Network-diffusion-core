using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace network_diffusion_core.Model
{
    public class Node
    {
        public Node(int nodeId, string color, string title, int nodeStateId)
        {
            NodeId = nodeId;
            Color = color;
            Title = title;
            NodeStateId = nodeStateId;
        }

        [JsonPropertyName("id")]
        [JsonProperty("id")]
        public int NodeId { get; set; }

        [JsonPropertyName("color")]
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonPropertyName("title")]
        [JsonProperty("title")]
        public string Title { get; set; }

        public int NodeStateId { get; set; }
    }
}
