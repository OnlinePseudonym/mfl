namespace MFL.DataTransferObjects
{
    public class LeagueDTO
    {
        public string id { get; set; }
        public string baseURL { get; set; }
        public string name { get; set; }
        public HistoryDTO history { get; set; }
        public RostersDTO franchises { get; set; }
        public string rosterSize { get; set; }
        public StartersDTO starters { get; set; }
        public RosterLimitsDTO rosterLimits { get; set; }
        public string lastRegularSeasonWeek { get; set; }
        public string maxKeepers { get; set; }
        public DivisionsDTO divisions { get; set; }
        public string rostersPerPlayer { get; set; }
    }
}
