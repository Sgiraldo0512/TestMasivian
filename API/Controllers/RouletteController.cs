using Masivian.Roulette.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masivian.Roulette.Controllers
{
    public class RouletteController : Controller
    {

        protected readonly BusinessRules.Interfaces.IRoulette _ibusinessRules;

        public RouletteController(BusinessRules.Interfaces.IRoulette IbusinessRules)
        {
            this._ibusinessRules = IbusinessRules;
        }

        [HttpPost("Create")]
        public Task<Entities.Roulette> Create()
        {            
            return Task.FromResult(this._ibusinessRules.Create());
        }

        [HttpGet("GetById/{id}")]
        public Task<Entities.Roulette> GetById(string id)
        {
            return Task.FromResult(this._ibusinessRules.GetById(id));
        }

        [HttpGet("Open/{id}")]
        public Task<bool> Open(string id)
        {
            return Task.FromResult(this._ibusinessRules.Open(id));

        }

        [HttpGet("GetAll")]
        public Task<List<Entities.Roulette>> GetAll()
        {
            return Task.FromResult(this._ibusinessRules.GetAll());

        }

        [HttpPost("Bet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Bet([FromHeader(Name = "userId")] string userId, string id, [FromBody] Bet bet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = true,
                    message = "Bad Request"
                });
            }
            return Ok(this._ibusinessRules.Bet(userId, id, bet));

        }

        [HttpPost("Close/{id}")]
        public Task<Entities.CloseRoulette> Close(string id)
        {
            return Task.FromResult(this._ibusinessRules.Close(id));

        }

        [HttpDelete("Delete/{id}")]
        public Task<bool> Delete(string id)
        {
            return Task.FromResult(this._ibusinessRules.Delete(id));

        }
    }
}
