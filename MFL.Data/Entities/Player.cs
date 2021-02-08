using System;

namespace MFL.Data.Entities
{
    public class Player : IUpdatable
    {
        public int Id { get; set; }
        public int RotoworldId { get; set; }
        public int StatsId { get; set; }
        public int StatsGlobalId { get; set; }
        public Guid SportsdataId { get; set; }
        public int RotowireId { get; set; }
        public int CbsId { get; set; }
        public string NflId { get; set; }
        public int EspnId { get; set; }
        public int FleaflickerId { get; set; }
        public int DraftYear { get; set; }
        public int DraftPick { get; set; }
        public int DraftRound { get; set; }
        public string DraftTeam { get; set; }
        public int Jersey { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime Birthdate { get; set; } 
        public string Position { get; set; }
        public string Team { get; set; }
        public string Status { get; set; }
        public int? TeamId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
