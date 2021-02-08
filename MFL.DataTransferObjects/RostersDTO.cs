using System.Collections.Generic;

namespace MFL.DataTransferObjects
{
    public class RostersDTO
    {
        public string count { get; set; }
        public IEnumerable<FranchiseDTO> franchise { get; set; } = new List<FranchiseDTO>();
    }
}
