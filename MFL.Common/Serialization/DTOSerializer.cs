using MFL.Common.Extensions;
using MFL.Data.Entities;
using MFL.Data.Models;
using MFL.DataTransferObjects;
using System;
using System.Linq;

namespace MFL.Common.Serialization
{
    public static class DTOSerializer
    {
        public static Player PlayerDTOtoEntity(PlayerDTO playerDTO)
        {
            var entity = new Player();
            UpdatePlayerFromDTO(playerDTO, entity);
            return entity;
        }

        public static Player UpdatePlayerFromDTO(PlayerDTO playerDTO, Player player)
        {

            var names = playerDTO.name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                player.Birthdate = playerDTO.birthdate.ToDateTimeFromUnix();
                player.CbsId = playerDTO.cbs_id.ToInt();
                player.DraftPick = playerDTO.draft_pick.ToInt();
                player.DraftRound = playerDTO.draft_round.ToInt();
                player.DraftTeam = playerDTO.draft_team;
                player.DraftYear = playerDTO.draft_year.ToInt();
                player.EspnId = playerDTO.espn_id.ToInt();
                player.FleaflickerId = playerDTO.fleaflicker_id.ToInt();
                player.Team = playerDTO.team;
                player.FullName = playerDTO.name;
                player.Height = playerDTO.height.ToInt();
                player.Id = playerDTO.id.ToInt();
                player.Jersey = playerDTO.jersey.ToInt();
                player.NflId = playerDTO.nfl_id;
                player.Position = playerDTO.position;
                player.RotowireId = playerDTO.rotowire_id.ToInt();
                player.RotoworldId = playerDTO.rotoworld_id.ToInt();
                player.SportsdataId = playerDTO.sportsdata_id != null ? new Guid(playerDTO.sportsdata_id) : Guid.Empty;
                player.StatsGlobalId = playerDTO.stats_global_id.ToInt();
                player.StatsId = playerDTO.stats_id.ToInt();
                player.Weight = playerDTO.weight.ToInt();
                player.FirstName = (names != null && names.Length > 1 ? names[1] : names[0]).Trim();
                player.LastName = (names != null && names.Length > 1 ? names[0] : string.Empty).Trim();

                return player;
            }
            catch (Exception ex)
            {
                throw new Exception("Error encountered converting player DTO to entity", ex);
            }
        }

        public static Franchise FranchiseDTOtoModel(FranchiseDTO franchiseDTO)
        {
            try
            {
                var model = new Franchise()
                {
                    Id = franchiseDTO.id.ToInt(),
                    Name = franchiseDTO.name,
                    OwnerName = franchiseDTO.owner_name,
                    Email = franchiseDTO.email,
                    Division = franchiseDTO.division.ToInt(),
                    Abbreviation = franchiseDTO.abbrev,
                    Icon = franchiseDTO.icon,
                    Logo = franchiseDTO.logo,
                    TimeZone = franchiseDTO.time_zone,
                    WaiverSortOrder = franchiseDTO.waiverSortOrder.ToInt(),
                    Roster = franchiseDTO.player.Select(x => PlayerDTOtoEntity(x))
                };

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error encountered converting franchise DTO to model", ex);
            }
        }

        public static League LeagueDTOtoModel(LeagueDTO dto)
        {
            try
            {
                var franchises = dto.franchises.franchise.Select(x => FranchiseDTOtoModel(x));
                var divisions = dto.divisions.division.Select(x => new GenericModel() { Id = x.id.ToInt(), Name = x.name });

                var model = new League()
                {
                    Id = dto.id.ToInt(),
                    Name = dto.name,
                    BaseUrl = dto.baseURL,
                    Franchises = franchises,
                    Divisions = divisions
                };

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error encountered converting league DTO to model", ex);
            }
        }

        public static League LeagueInstanceDTOtoModel(LeagueInstanceDTO dto)
        {
            try
            {
                var model = new League()
                {
                    Id = dto.league_id.ToInt(),
                    Name = dto.name,
                    Url = dto.url,
                    MyFranchiseName = dto.franchise_name,
                };

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Error encountered converting league DTO to model", ex);
            }
        }
    }
}
