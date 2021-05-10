using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace network_diffusion_core.Model
{
    public class NodeState
    {
        public NodeState(int id, string title, string color, double? changeToPropabilityRate, int? previousStateId, bool? beforeInfection)
        {
            Id = id;
            Title = title;
            Color = color;
            ChangeToPropabilityRate = changeToPropabilityRate;
            PreviousStateId = previousStateId;
            BeforeInfection = beforeInfection;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public double? ChangeToPropabilityRate { get; set; }
        public int? PreviousStateId { get; set; }
        public bool? BeforeInfection { get; set; }
    }
}
