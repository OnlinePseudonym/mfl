using System.Collections.Generic;
using System.Linq;

namespace MFL.Data.Models.Settings
{
    public class StartersSettings
    {
        public int Count { get; set; }
        public IEnumerable<PositionLimit> PositionLimits { get; set; } = Enumerable.Empty<PositionLimit>();
        public int IDPStarters { get; set; }
    }
}
