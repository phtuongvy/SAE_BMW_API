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
    public class PeutContenirsController : ControllerBase
    {
        private readonly IDataRepository<PeutContenir> _peutContenir;

        public PeutContenirsController(IDataRepository<PeutContenir> peutContenirRepository)
        {
            this._peutContenir = peutContenirRepository;
        }

        [HttpGet]
        [ActionName("GetPeutContenirs")]
        public async Task<ActionResult<IEnumerable<PeutContenir>>> GetPeutContenirs()
        {
            return await _peutContenir.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetPeutContenirById")]
        public async Task<ActionResult<PeutContenir>> GetPeutContenirById(int id)
        {

            var peutContenir = await _peutContenir.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (peutContenir == null)
            {
                return NotFound();
            }
            return peutContenir;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutPeutContenir")]
        public async Task<IActionResult> PutPeutContenir(int id, PeutContenir peutContenir)
        {
            if (id != peutContenir.IdColoris)
            {
                return BadRequest();
            }
            var userToUpdate = await _peutContenir.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _peutContenir.UpdateAsync(userToUpdate.Value, peutContenir);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostPeutContenir")]
        public async Task<ActionResult<PeutContenir>> PostPeutContenir(PeutContenir peutContenir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _peutContenir.AddAsync(peutContenir);
            return CreatedAtAction("GetPeutContenirById", new { id = peutContenir.IdColoris }, peutContenir); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeletePeutContenir")]
        public async Task<IActionResult> DeletePeutContenir(int id)
        {
            var peutContenir = await _peutContenir.GetByIdAsync(id);
            if (peutContenir == null)
            {
                return NotFound();
            }
            await _peutContenir.DeleteAsync(peutContenir.Value);
            return NoContent();
        }
    }
}
