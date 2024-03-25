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
    public class DonneesConstanteController : ControllerBase
    {
        private readonly IDataRepository<DonneesConstante> _DonneesConstante;

        public DonneesConstanteController(IDataRepository<DonneesConstante> DonneesConstanteRepository)
        {
            this._DonneesConstante = DonneesConstanteRepository;
        }

        [HttpGet]
        [ActionName("GetDonneesConstantes")]
        public async Task<ActionResult<IEnumerable<DonneesConstante>>> GetDonneesConstantes()
        {
            return await _DonneesConstante.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetDonneesConstanteById")]
        public async Task<ActionResult<DonneesConstante>> GetDonneesConstanteById(int id)
        {

            var DonneesConstante = await _DonneesConstante.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (DonneesConstante == null)
            {
                return NotFound();
            }
            return DonneesConstante;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutDonneesConstante")]
        public async Task<IActionResult> PutDonneesConstante(int id, DonneesConstante DonneesConstante)
        {
            if (id != DonneesConstante.IdDonnesConstante)
            {
                return BadRequest();
            }
            var userToUpdate = await _DonneesConstante.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _DonneesConstante.UpdateAsync(userToUpdate.Value, DonneesConstante);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostDonneesConstante")]
        public async Task<ActionResult<DonneesConstante>> PostDonneesConstante(DonneesConstante DonneesConstante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _DonneesConstante.AddAsync(DonneesConstante);
            return CreatedAtAction("GetDonneesConstanteById", new { id = DonneesConstante.IdDonnesConstante }, DonneesConstante); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteDonneesConstante")]
        public async Task<IActionResult> DeleteDonneesConstante(int id)
        {
            var DonneesConstante = await _DonneesConstante.GetByIdAsync(id);
            if (DonneesConstante == null)
            {
                return NotFound();
            }
            await _DonneesConstante.DeleteAsync(DonneesConstante.Value);
            return NoContent();
        }

    }
}
