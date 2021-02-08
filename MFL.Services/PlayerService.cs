using MFL.Data.Repository;
using MFL.Data.Entities;
using MFL.DataTransferObjects;
using MFL.Services.Clients;
using MFL.Common.Extensions;
using MFL.Common.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MFL.Common.JsonConverters;

namespace MFL.Services
{
    public interface IPlayerService
    {
        Task SyncPlayers(int year);
        IEnumerable<Player> GetAll();
        Player GetById(int id);
        IEnumerable<Player> GetByIds(IEnumerable<int> id);
        Task<IEnumerable<Player>> GetFreeAgents(string leagueId);
        void Create(Player player);
        void Create(IEnumerable<Player> players);
        void Update(Player player);
        void UpdateOrCreate(IEnumerable<PlayerDTO> playerDTOs);
        void Delete(int id);
    }

    public class PlayerService : IPlayerService
    {
        private IUnitOfWork _uow;
        private IRepository<Player> _player;
        private IMFLHttpClient _client;

        public PlayerService(IUnitOfWork uow, IMFLHttpClient client)
        {
            _uow = uow;
            _client = client;
            _player = _uow.GetRepository<Player>();
        }

        public IEnumerable<Player> GetAll()
        {
            return _player.Get();
        }

        public Player GetById(int id)
        {
            try
            {
                return _player.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failure retrieving player", ex);
            }
        }

        public IEnumerable<Player> GetByIds(IEnumerable<int> ids)
        {
            try
            {
                return _player.Get(x => ids.Contains(x.Id));
            }
            catch (Exception ex)
            {
                throw new Exception("Failure retrieving players", ex);
            }
        }

        public void Create(Player player)
        {
            try
            {
                _player.Insert(player);
                _uow.Save();
            }
            catch(Exception ex)
            {
                throw new Exception("Failure creating player", ex);
            }
        }

        public void Create(IEnumerable<Player> players)
        {
            try
            {
                foreach (var player in players)
                {
                    _player.Insert(player);
                }
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failure inserting players", ex);
            }
        }

        public void Update(Player player)
        {
            try
            {
                _player.Update(player);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failure updating player", ex);
            }
        }

        public void UpdateOrCreate(IEnumerable<PlayerDTO> playerDTOs)
        {
            try
            {
                foreach (var dto in playerDTOs)
                {
                    if (_player.Exists(dto.id.ToInt()))
                    {
                        var entity = _player.GetById(dto.id.ToInt());
                        var player = DTOSerializer.UpdatePlayerFromDTO(dto, entity);

                        _player.Update(player);
                    }
                    else
                    {
                        var player = DTOSerializer.PlayerDTOtoEntity(dto);
                        _player.Insert(player);
                    }
                }
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failure updating players", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _player.Delete(id);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failure deleting player", ex);
            }
        }

        public async Task SyncPlayers(int year)
        {
            var lastModified = GetLastModifiedDate();
            
            if (lastModified.Date == DateTime.Today)
            {
                return;
            }

            var unixTime = ((DateTimeOffset)lastModified).ToUnixTimeSeconds();
            var endpoint = $"/{year}/export?TYPE=players&DETAILS=1&SINCE={unixTime}&JSON=1";
            MFLApiResponse results = await _client.GetFromJsonAsync(endpoint, new SingleOrManyConverter<PlayerDTO>());

            if (results.players != null)
            {
                var players = results.players.player;
                UpdateOrCreate(players);
            }
        }

        private DateTime GetLastModifiedDate()
        {
            var lastUpdated = _player.Get(orderBy: players => players.OrderByDescending(x => x.UpdatedDate)).FirstOrDefault();
            return lastUpdated?.UpdatedDate ?? DateTime.MinValue;
        }

        public async Task<IEnumerable<Player>> GetFreeAgents(string leagueId)
        {
            MFLApiResponse results = await _client.GetFromJsonAsync($"/2020/export?TYPE=freeAgents&L={leagueId}&JSON=1");
            var leagues = new List<Player>();

            var ids = results.freeAgents.leagueUnit.player.Select(x => x.id.ToInt());
            var players = GetByIds(ids);

            return players;
        }
    }
}
