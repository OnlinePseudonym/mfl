using System.Collections;
using System.Collections.Generic;

namespace MFL.DataTransferObjects
{
    public class FranchiseDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string owner_name { get; set; }
        public string email { get; set; }
        public string division { get; set; }
        public string abbrev { get; set; }
        public string icon { get; set; }
        public string logo { get; set; }
        public string time_zone { get; set; }
        public string waiverSortOrder { get; set; }
        public IEnumerable<PlayerDTO> player { get; set; } = new List<PlayerDTO>();
    }
}
