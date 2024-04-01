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
    public class CategorieCaracteristiqueMotoController : ControllerBase
    {
        private readonly IDataRepository<CategorieCaracteristiqueMoto> _categorieCaracteristiqueMoto;

        public CategorieCaracteristiqueMotoController(IDataRepository<CategorieCaracteristiqueMoto> categorieCaracteristiqueMotoRepository)
        {
            this._categorieCaracteristiqueMoto = categorieCaracteristiqueMotoRepository;
        }

        [HttpGet]
        [ActionName("GetCategorieCaracteristiqueMotos")]
        public async Task<ActionResult<IEnumerable<CategorieCaracteristiqueMoto>>> GetCategorieCaracteristiqueMotos()
        {
            return await _categorieCaracteristiqueMoto.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetCategorieCaracteristiqueMotoById")]
        public async Task<ActionResult<CategorieCaracteristiqueMoto>> GetCategorieCaracteristiqueMotoById(int id)
        {

            var categorieCaracteristiqueMoto = await _categorieCaracteristiqueMoto.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (categorieCaracteristiqueMoto == null)
            {
                return NotFound();
            }
            return categorieCaracteristiqueMoto;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCategorieCaracteristiqueMoto")]
        public async Task<IActionResult> PutCategorieCaracteristiqueMoto(int id, CategorieCaracteristiqueMoto categorieCaracteristiqueMoto)
        {
            if (id != categorieCaracteristiqueMoto.IdCategorieCaracteristiqueMoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _categorieCaracteristiqueMoto.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _categorieCaracteristiqueMoto.UpdateAsync(userToUpdate.Value, categorieCaracteristiqueMoto);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCategorieCaracteristiqueMoto")]
        public async Task<ActionResult<CategorieCaracteristiqueMoto>> PostCategorieCaracteristiqueMoto(CategorieCaracteristiqueMoto categorieCaracteristiqueMoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _categorieCaracteristiqueMoto.AddAsync(categorieCaracteristiqueMoto);
            return CreatedAtAction("GetCategorieCaracteristiqueMotoById", new { id = categorieCaracteristiqueMoto.IdCategorieCaracteristiqueMoto }, categorieCaracteristiqueMoto); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCategorieCaracteristiqueMoto")]
        public async Task<IActionResult> DeleteCategorieCaracteristiqueMoto(int id)
        {
            var categorieCaracteristiqueMoto = await _categorieCaracteristiqueMoto.GetByIdAsync(id);
            if (categorieCaracteristiqueMoto == null)
            {
                return NotFound();
            }
            await _categorieCaracteristiqueMoto.DeleteAsync(categorieCaracteristiqueMoto.Value);
            return NoContent();
        }

    }
}
