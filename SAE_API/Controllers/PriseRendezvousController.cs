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
    [Route("api/[controller]")]
    [ApiController]
    public class PriseRendezvousController : ControllerBase
    {
        private readonly IDataRepository<PriseRendezvous> _priseRendezvous;

        public PriseRendezvousController(IDataRepository<PriseRendezvous> priseRendezvousRepository)
        {
            this._priseRendezvous = priseRendezvousRepository;
        }

        [HttpGet]
        [ActionName("GetPriseRendezvouss")]
        public async Task<ActionResult<IEnumerable<PriseRendezvous>>> GetPriseRendezvouss()
        {
            return await _priseRendezvous.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetPriseRendezvousById")]
        public async Task<ActionResult<PriseRendezvous>> GetPriseRendezvousById(int id)
        {

            var priseRendezvous = await _priseRendezvous.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (priseRendezvous == null)
            {
                return NotFound();
            }
            return priseRendezvous;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutPriseRendezvous")]
        public async Task<IActionResult> PutPriseRendezvous(int id, PriseRendezvous priseRendezvous)
        {
            if (id != priseRendezvous.IdConcessionnaire)
            {
                return BadRequest();
            }
            var userToUpdate = await _priseRendezvous.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _priseRendezvous.UpdateAsync(userToUpdate.Value, priseRendezvous);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostPriseRendezvous")]
        public async Task<ActionResult<PriseRendezvous>> PostPriseRendezvous(PriseRendezvous priseRendezvous)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _priseRendezvous.AddAsync(priseRendezvous);
            return CreatedAtAction("GetPriseRendezvousById", new { id = priseRendezvous.IdConcessionnaire }, priseRendezvous); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeletePriseRendezvous")]
        public async Task<IActionResult> DeletePriseRendezvous(int id)
        {
            var priseRendezvous = await _priseRendezvous.GetByIdAsync(id);
            if (priseRendezvous == null)
            {
                return NotFound();
            }
            await _priseRendezvous.DeleteAsync(priseRendezvous.Value);
            return NoContent();
        }
    }
}
