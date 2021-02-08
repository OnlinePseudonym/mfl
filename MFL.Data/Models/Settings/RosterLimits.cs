using System.Collections.Generic;
using System.Linq;

namespace MFL.Data.Models.Settings
{
    public class RosterLimits
    {
        public IEnumerable<PositionLimit> PositionLimits { get; set; } = Enumerable.Empty<PositionLimit>();
    }
}
