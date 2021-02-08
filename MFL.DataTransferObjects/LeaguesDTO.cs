using System.Collections.Generic;
using System.Linq;

namespace MFL.DataTransferObjects
{
    public class LeaguesDTO
    {
        public IEnumerable<LeagueInstanceDTO> league { get; set; } = Enumerable.Empty<LeagueInstanceDTO>();
    }
}
