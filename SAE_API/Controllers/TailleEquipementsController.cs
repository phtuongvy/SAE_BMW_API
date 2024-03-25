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
    public class TailleEquipementsController : ControllerBase
    {
        private readonly IDataRepository<TailleEquipement> _tailleEquipement;

        public TailleEquipementsController(IDataRepository<TailleEquipement> tailleEquipementRepository)
        {
            this._tailleEquipement = tailleEquipementRepository;
        }

        [HttpGet]
        [ActionName("GetTailleEquipements")]
        public async Task<ActionResult<IEnumerable<TailleEquipement>>> GetTailleEquipements()
        {
            return await _tailleEquipement.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetTailleEquipementById")]
        public async Task<ActionResult<TailleEquipement>> GetTailleEquipementById(int id)
        {

            var tailleEquipement = await _tailleEquipement.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (tailleEquipement == null)
            {
                return NotFound();
            }
            return tailleEquipement;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutTailleEquipement")]
        public async Task<IActionResult> PutTailleEquipement(int id, TailleEquipement tailleEquipement)
        {
            if (id != tailleEquipement.IdTailleEquipement)
            {
                return BadRequest();
            }
            var userToUpdate = await _tailleEquipement.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _tailleEquipement.UpdateAsync(userToUpdate.Value, tailleEquipement);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostTailleEquipement")]
        public async Task<ActionResult<TailleEquipement>> PostTailleEquipement(TailleEquipement tailleEquipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _tailleEquipement.AddAsync(tailleEquipement);
            return CreatedAtAction("GetTailleEquipementById", new { id = tailleEquipement.IdTailleEquipement }, tailleEquipement); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteTailleEquipement")]
        public async Task<IActionResult> DeleteTailleEquipement(int id)
        {
            var tailleEquipement = await _tailleEquipement.GetByIdAsync(id);
            if (tailleEquipement == null)
            {
                return NotFound();
            }
            await _tailleEquipement.DeleteAsync(tailleEquipement.Value);
            return NoContent();
        }
    }
}
