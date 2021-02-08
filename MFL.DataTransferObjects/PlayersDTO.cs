using System.Collections.Generic;

namespace MFL.DataTransferObjects
{
    public class PlayersDTO
    {
        public string timestamp { get; set; }
        public IEnumerable<PlayerDTO> player { get; set; }
    }
}
