using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.Model
{
    public class Node
    {
        public Node(int nodeId, string color, string title)
        {
            NodeId = nodeId;
            Color = color;
            Title = title;
        }

        public int NodeId { get; set; }
        public string Color { get; set; }
        public string Title { get; set; }
    }
}
