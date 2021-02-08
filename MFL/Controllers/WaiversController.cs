using MFL.Data.Entities;
using MFL.DataTransferObjects;
using MFL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MFL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaiversController : ControllerBase
    {
        private readonly IWaiverService _waiverService;

        public WaiversController(IWaiverService waiverService)
        {
            _waiverService = waiverService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var players = _waiverService.GetAll();
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
                var player = _waiverService.GetById(id);
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
                _waiverService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{leagueId}")]
        public async Task<IActionResult> AddDropPlayers(string leagueId, string addIds = "", string dropIds = "")
        {
            try
            {
                var players = await _waiverService.AddDropPlayers(leagueId, addIds, dropIds);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(WaiverDTO waiver)
        {
            try
            {
                var transaction = new WaiverTransaction()
                {
                    LeagueId = waiver.LeagueId,
                    PlayerId = waiver.PlayerId,
                    DropIds = waiver.DropIds
                };

                _waiverService.Create(transaction);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update(WaiverTransaction transaction)
        {
            try
            {
                _waiverService.Update(transaction);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
