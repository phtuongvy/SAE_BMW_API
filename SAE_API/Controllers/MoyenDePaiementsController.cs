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
    public class MoyenDePaiementsController : ControllerBase
    {
        private readonly IDataRepository<MoyenDePaiement> _moyenDePaiement;

        public MoyenDePaiementsController(IDataRepository<MoyenDePaiement> moyenDePaiementRepository)
        {
            this._moyenDePaiement = moyenDePaiementRepository;
        }

        [HttpGet]
        [ActionName("GetMoyenDePaiements")]
        public async Task<ActionResult<IEnumerable<MoyenDePaiement>>> GetMoyenDePaiements()
        {
            return await _moyenDePaiement.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetMoyenDePaiementById")]
        public async Task<ActionResult<MoyenDePaiement>> GetMoyenDePaiementById(int id)
        {

            var moyenDePaiement = await _moyenDePaiement.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (moyenDePaiement == null)
            {
                return NotFound();
            }
            return moyenDePaiement;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutMoyenDePaiement")]
        public async Task<IActionResult> PutMoyenDePaiement(int id, MoyenDePaiement moyenDePaiement)
        {
            if (id != moyenDePaiement.IdMoyenDePaiement)
            {
                return BadRequest();
            }
            var userToUpdate = await _moyenDePaiement.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _moyenDePaiement.UpdateAsync(userToUpdate.Value, moyenDePaiement);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostMoyenDePaiement")]
        public async Task<ActionResult<MoyenDePaiement>> PostMoyenDePaiement(MoyenDePaiement moyenDePaiement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _moyenDePaiement.AddAsync(moyenDePaiement);
            return CreatedAtAction("GetMoyenDePaiementById", new { id = moyenDePaiement.IdMoyenDePaiement }, moyenDePaiement); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteMoyenDePaiement")]
        public async Task<IActionResult> DeleteMoyenDePaiement(int id)
        {
            var moyenDePaiement = await _moyenDePaiement.GetByIdAsync(id);
            if (moyenDePaiement == null)
            {
                return NotFound();
            }
            await _moyenDePaiement.DeleteAsync(moyenDePaiement.Value);
            return NoContent();
        }
    }
}
