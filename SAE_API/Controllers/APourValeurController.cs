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
    public class APourValeurController : ControllerBase
    {
        private readonly IDataRepository<APourValeur> _aPourValeur;

        public APourValeurController(IDataRepository<APourValeur> aPourValeurRepository)
        {
            this._aPourValeur = aPourValeurRepository;
        }

        [HttpGet]
        [ActionName("GetAPourValeurs")]
        public async Task<ActionResult<IEnumerable<APourValeur>>> GetAPourValeurs()
        {
            return await _aPourValeur.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetAPourValeurById")]
        public async Task<ActionResult<APourValeur>> GetAPourValeurById(int id)
        {

            var aPourValeur = await _aPourValeur.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (aPourValeur == null)
            {
                return NotFound();
            }
            return aPourValeur;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutAPourValeur")]
        public async Task<IActionResult> PutAPourValeur(int id, APourValeur aPourValeur)
        {
            if (id != aPourValeur.IdCaracteristiqueMoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _aPourValeur.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _aPourValeur.UpdateAsync(userToUpdate.Value, aPourValeur);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAPourValeur")]
        public async Task<ActionResult<APourValeur>> PostAPourValeur(APourValeur aPourValeur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _aPourValeur.AddAsync(aPourValeur);
            return CreatedAtAction("GetAPourValeurById", new { id = aPourValeur.IdCaracteristiqueMoto }, aPourValeur); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteAPourValeur")]
        public async Task<IActionResult> DeleteAPourValeur(int id)
        {
            var aPourValeur = await _aPourValeur.GetByIdAsync(id);
            if (aPourValeur == null)
            {
                return NotFound();
            }
            await _aPourValeur.DeleteAsync(aPourValeur.Value);
            return NoContent();
        }

    }
}
