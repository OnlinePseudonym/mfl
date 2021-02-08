using System.Collections.Generic;
using System.Linq;

namespace MFL.DataTransferObjects
{
    public class DivisionsDTO
    {
        public string count { get; set; }
        public IEnumerable<GenericDTO> division { get; set; } = Enumerable.Empty<GenericDTO>();
    }
}
