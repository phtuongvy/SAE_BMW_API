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
    public class CouleurEquipementsController : ControllerBase
    {
        private readonly IDataRepository<CouleurEquipement> _couleurEquipement;

        public CouleurEquipementsController(IDataRepository<CouleurEquipement> couleurEquipementRepository)
        {
            this._couleurEquipement = couleurEquipementRepository;
        }

        [HttpGet]
        [ActionName("GetCouleurEquipements")]
        public async Task<ActionResult<IEnumerable<CouleurEquipement>>> GetCouleurEquipements()
        {
            return await _couleurEquipement.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetCouleurEquipementById")]
        public async Task<ActionResult<CouleurEquipement>> GetCouleurEquipementById(int id)
        {

            var couleurEquipement = await _couleurEquipement.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (couleurEquipement == null)
            {
                return NotFound();
            }
            return couleurEquipement;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCouleurEquipement")]
        public async Task<IActionResult> PutCouleurEquipement(int id, CouleurEquipement couleurEquipement)
        {
            if (id != couleurEquipement.IdCouleurEquipement)
            {
                return BadRequest();
            }
            var userToUpdate = await _couleurEquipement.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _couleurEquipement.UpdateAsync(userToUpdate.Value, couleurEquipement);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCouleurEquipement")]
        public async Task<ActionResult<CouleurEquipement>> PostCouleurEquipement(CouleurEquipement couleurEquipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _couleurEquipement.AddAsync(couleurEquipement);
            return CreatedAtAction("GetCouleurEquipementById", new { id = couleurEquipement.IdCouleurEquipement }, couleurEquipement); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCouleurEquipement")]
        public async Task<IActionResult> DeleteCouleurEquipement(int id)
        {
            var couleurEquipement = await _couleurEquipement.GetByIdAsync(id);
            if (couleurEquipement == null)
            {
                return NotFound();
            }
            await _couleurEquipement.DeleteAsync(couleurEquipement.Value);
            return NoContent();
        }

    }
}
