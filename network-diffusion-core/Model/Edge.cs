using System;
using System.Collections.Generic;
using System.Linq;
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
        public int EdgeId { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}
