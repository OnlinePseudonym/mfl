using System.Collections;

namespace MFL.DataTransferObjects

{
    public class MFLApiResponse
    {
        public string version { get; set; }
        public PlayersDTO players { get; set; }
        public LeaguesDTO leagues { get; set; }
        public LeagueDTO league { get; set; }
        public RostersDTO rosters { get; set; }
        public FreeAgentsDTO freeAgents { get; set; }
    }
}
