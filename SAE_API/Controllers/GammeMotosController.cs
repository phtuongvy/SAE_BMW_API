using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GammeMotosController : ControllerBase
    {
        private readonly IDataRepository<GammeMoto> _gammeMoto;

        public GammeMotosController(IDataRepository<GammeMoto> gammeMotoRepository)
        {
            this._gammeMoto = gammeMotoRepository;
        }

        [HttpGet]
        [ActionName("GetGammeMotos")]
        public async Task<ActionResult<IEnumerable<GammeMoto>>> GetGammeMotos()
        {
            return await _gammeMoto.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetGammeMotoById")]
        public async Task<ActionResult<GammeMoto>> GetGammeMotoById(int id)
        {

            var gammeMoto = await _gammeMoto.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (gammeMoto == null)
            {
                return NotFound();
            }
            return gammeMoto;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGammeMoto(int id, GammeMoto gammeMoto)
        {
            if (id != gammeMoto.IdGammeMoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _gammeMoto.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _gammeMoto.UpdateAsync(userToUpdate.Value, gammeMoto);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostGammeMoto")]
        public async Task<ActionResult<GammeMoto>> PostGammeMoto(GammeMoto gammeMoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _gammeMoto.AddAsync(gammeMoto);
            return CreatedAtAction("GetGammeMotoById", new { id = gammeMoto.IdGammeMoto }, gammeMoto); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteGammeMoto")]
        public async Task<IActionResult> DeleteGammeMoto(int id)
        {
            var gammeMoto = await _gammeMoto.GetByIdAsync(id);
            if (gammeMoto == null)
            {
                return NotFound();
            }
            await _gammeMoto.DeleteAsync(gammeMoto.Value);
            return NoContent();
        }

    }
}
