using MFL.Common.Extensions;
using MFL.Common.Serialization;
using MFL.Data.Models;
using MFL.DataTransferObjects;
using MFL.Services.Clients;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFL.Services
{
    public interface IFranchiseService
    {
        Task<IEnumerable<Franchise>> GetAll(int leagueId);
        Task<Franchise> GetById(int leagueId, int franchiseId);
    }
    public class FranchiseService : IFranchiseService
    {
        private IMFLHttpClient _client;
        private IPlayerService _playerService;
        public FranchiseService(IMFLHttpClient client, IPlayerService playerService)
        {
            _client = client;
            _playerService = playerService;
        }

        public async Task<IEnumerable<Franchise>> GetAll(int leagueId)
        {
            IEnumerable<FranchiseDTO> franchiseDTOs = await GetFranchises(leagueId);
            return franchiseDTOs.Select(x => GetFranchiseFromDTO(x));
        }

        public async Task<Franchise> GetById(int leagueId, int franchiseId)
        {
            FranchiseDTO franchiseDTO = await GetFranchiseById(leagueId, franchiseId);
            return GetFranchiseFromDTO(franchiseDTO);
        }

        private Franchise GetFranchiseFromDTO(FranchiseDTO dto)
        {
            var franchise = DTOSerializer.FranchiseDTOtoModel(dto);
            franchise.Roster = _playerService.GetByIds(dto.player.Select(x => x.id.ToInt()));

            return franchise;
        }

        private async Task<IEnumerable<FranchiseDTO>> GetFranchises(int leagueId)
        {
            MFLApiResponse result = await _client.GetFromJsonAsync($"/2020/export?TYPE=rosters&L={leagueId}&JSON=1");
            return result.rosters.franchise;
        }

        private async Task<FranchiseDTO> GetFranchiseById(int leagueId, int franchiseId)
        {
            MFLApiResponse result = await _client.GetFromJsonAsync($"/2020/export?TYPE=rosters&L={leagueId}&FRANCHISE={franchiseId:D4}&JSON=1");
            return result.rosters.franchise.FirstOrDefault();
        }
    }
}
