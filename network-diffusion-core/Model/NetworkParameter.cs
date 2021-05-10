using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.Model
{
    public class NetworkParameter
    {
        public NetworkParameter(int id, string name, double value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
