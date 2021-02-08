using MFL.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MFL.Data.Models
{
    public class Franchise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Email { get; set; }
        public int Division { get; set; }
        public string Abbreviation { get; set; }
        public string Icon { get; set; }
        public string Logo { get; set; }
        public string TimeZone { get; set; }
        public int WaiverSortOrder { get; set; }
        public IEnumerable<Player> Roster { get; set; } = Enumerable.Empty<Player>();
    }
}
