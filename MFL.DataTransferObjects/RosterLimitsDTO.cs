using System.Collections.Generic;

namespace MFL.DataTransferObjects
{
    public class RosterLimitsDTO
    {
        public IEnumerable<PositionLimitDTO> position { get; set; }
    }
}
