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
    public class APourCouleurController : ControllerBase
    {
        private readonly IDataRepository<APourCouleur> _aPourCouleur;

        public APourCouleurController(IDataRepository<APourCouleur> aPourCouleurRepository)
        {
            this._aPourCouleur = aPourCouleurRepository;
        }

        [HttpGet]
        [ActionName("GetAPourCouleurs")]
        public async Task<ActionResult<IEnumerable<APourCouleur>>> GetAPourCouleurs()
        {
            return await _aPourCouleur.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdEquipement{id}/IdCouleurEquipement{id2}")]
        [ActionName("GetAPourCouleurById")]
        public async Task<ActionResult<APourCouleur>> GetAPourCouleurById(int id)
        {

            var aPourCouleur = await _aPourCouleur.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (aPourCouleur == null)
            {
                return NotFound();
            }
            return aPourCouleur;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutAPourCouleur")]
        public async Task<IActionResult> PutAPourCouleur(int id, APourCouleur aPourCouleur)
        {
            if (id != aPourCouleur.IdCouleurEquipement)
            {
                return BadRequest();
            }
            var userToUpdate = await _aPourCouleur.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _aPourCouleur.UpdateAsync(userToUpdate.Value, aPourCouleur);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAPourCouleur")]
        public async Task<ActionResult<APourCouleur>> PostAPourCouleur(APourCouleur aPourCouleur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _aPourCouleur.AddAsync(aPourCouleur);
            return CreatedAtAction("GetAPourCouleurById", new { id = aPourCouleur.IdCouleurEquipement }, aPourCouleur); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteAPourCouleur")]
        public async Task<IActionResult> DeleteAPourCouleur(int id)
        {
            var aPourCouleur = await _aPourCouleur.GetByIdAsync(id);
            if (aPourCouleur == null)
            {
                return NotFound();
            }
            await _aPourCouleur.DeleteAsync(aPourCouleur.Value);
            return NoContent();
        }

    }
}
