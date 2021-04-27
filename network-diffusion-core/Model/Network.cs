using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<Node> Nodes { get; set; }
        public List<Edge> Edges {get; set;}
    }
}
