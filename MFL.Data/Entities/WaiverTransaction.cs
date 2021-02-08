using System;
using System.Collections.Generic;
using System.Text;

namespace MFL.Data.Entities
{
    public class WaiverTransaction : IUpdatable
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public string DropIds { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
