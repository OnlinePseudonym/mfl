using MFL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MFL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchisesController : ControllerBase
    {
        private readonly IFranchiseService _frachiseService;

        public FranchisesController(IFranchiseService franchiseService)
        {
            _frachiseService = franchiseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int leagueId)
        {
            try
            {
                var rosters = await _frachiseService.GetAll(leagueId);
                return Ok(rosters);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id, int leagueId)
        {
            try
            {
                var roster = await _frachiseService.GetById(leagueId, id);
                return Ok(roster);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
