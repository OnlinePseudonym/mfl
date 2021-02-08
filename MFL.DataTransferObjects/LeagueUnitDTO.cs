using System.Collections.Generic;
using System.Linq;

namespace MFL.DataTransferObjects
{
    public class LeagueUnitDTO
    {
        public string unit { get; set; }
        public IEnumerable<PlayerDTO> player { get; set; } = Enumerable.Empty<PlayerDTO>();
    }
}
