using MFL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MFL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var players = _playerService.GetAll();
                return Ok(players);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var player = _playerService.GetById(id);
                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _playerService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetFreeAgents(string leagueId)
        {
            try
            {
                var players = await _playerService.GetFreeAgents(leagueId);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
