using MFL.Services.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;
using MFL.DataTransferObjects;
using MFL.Common.Serialization;
using MFL.Data.Models;
using MFL.Common.Extensions;

namespace MFL.Services
{
    public interface ILeagueService
    {
        Task<IEnumerable<League>> GetMyLeagues();
        Task<League> GetById(int id);
    }

    public class LeagueService : ILeagueService
    {
        private IMFLHttpClient _client;

        public LeagueService(IMFLHttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<League>> GetMyLeagues()
        {
            MFLApiResponse results = await _client.GetFromJsonAsync("/2020/export?TYPE=myleagues&FRANCHISE_NAMES=1&JSON=1");
            var leagues = new List<League>();

            foreach (var leagueDto in results.leagues.league)
            {
                var league = await GetById(leagueDto.league_id.ToInt());

                var merged = league.Merge(DTOSerializer.LeagueInstanceDTOtoModel(leagueDto));
                leagues.Add(merged);
            }

            return leagues;
        }

        public async Task<League> GetById(int id)
        {
            MFLApiResponse results = await _client.GetFromJsonAsync($"2020/export?TYPE=league&L={id}&JSON=1");
            var league = DTOSerializer.LeagueDTOtoModel(results.league);

            return league;
        }
    }
}
