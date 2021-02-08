using System.Collections.Generic;

namespace MFL.DataTransferObjects
{
    public class HistoryDTO
    {
        public IEnumerable<LeagueInstanceDTO> league { get; set; }
    }
}
