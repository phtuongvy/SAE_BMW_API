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
    public class SegementsController : ControllerBase
    {
        private readonly IDataRepository<Segement> _segement;

        public SegementsController(IDataRepository<Segement> segementRepository)
        {
            this._segement = segementRepository;
        }

        [HttpGet]
        [ActionName("GetSegements")]
        public async Task<ActionResult<IEnumerable<Segement>>> GetSegements()
        {
            return await _segement.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetSegementById")]
        public async Task<ActionResult<Segement>> GetSegementById(int id)
        {

            var segement = await _segement.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (segement == null)
            {
                return NotFound();
            }
            return segement;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutSegement")]
        public async Task<IActionResult> PutSegement(int id, Segement segement)
        {
            if (id != segement.IdSegement)
            {
                return BadRequest();
            }
            var userToUpdate = await _segement.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _segement.UpdateAsync(userToUpdate.Value, segement);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostSegement")]
        public async Task<ActionResult<Segement>> PostSegement(Segement segement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _segement.AddAsync(segement);
            return CreatedAtAction("GetSegementById", new { id = segement.IdSegement }, segement); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteSegement")]
        public async Task<IActionResult> DeleteSegement(int id)
        {
            var segement = await _segement.GetByIdAsync(id);
            if (segement == null)
            {
                return NotFound();
            }
            await _segement.DeleteAsync(segement.Value);
            return NoContent();
        }
    }
}
