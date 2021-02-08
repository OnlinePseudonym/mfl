using MFL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MFL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;

        public TeamsController(IFranchiseService franchiseService)
        {
            _franchiseService = franchiseService;
        }

        [Route("[action]/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var teams = await _franchiseService.GetAll(id);
                return Ok(teams);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        public IActionResult GetByFranchiseId(int leagueId, int franchiseId)
        {
            try
            {
                var team = _franchiseService.GetById(leagueId, franchiseId);
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
