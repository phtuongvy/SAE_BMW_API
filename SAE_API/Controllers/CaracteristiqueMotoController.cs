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
    public class CaracteristiqueMotoController : ControllerBase
    {
        private readonly IDataRepository<CaracteristiqueMoto> _caracteristiqueMoto;

        public CaracteristiqueMotoController(IDataRepository<CaracteristiqueMoto> caracteristiqueMotoRepository)
        {
            this._caracteristiqueMoto = caracteristiqueMotoRepository;
        }

        [HttpGet]
        [ActionName("GetCaracteristiqueMotos")]
        public async Task<ActionResult<IEnumerable<CaracteristiqueMoto>>> GetCaracteristiqueMotos()
        {
            return await _caracteristiqueMoto.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdCaracteristiqueMoto{id}/IdCategorieCaracteristiqueMoto{id2}")]
        [ActionName("GetCaracteristiqueMotoById")]
        public async Task<ActionResult<CaracteristiqueMoto>> GetCaracteristiqueMotoById(int id)
        {

            var caracteristiqueMoto = await _caracteristiqueMoto.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (caracteristiqueMoto == null)
            {
                return NotFound();
            }
            return caracteristiqueMoto;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCaracteristiqueMoto")]
        public async Task<IActionResult> PutCaracteristiqueMoto(int id, CaracteristiqueMoto caracteristiqueMoto)
        {
            if (id != caracteristiqueMoto.IdCaracteristiqueMoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _caracteristiqueMoto.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _caracteristiqueMoto.UpdateAsync(userToUpdate.Value, caracteristiqueMoto);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCaracteristiqueMoto")]
        public async Task<ActionResult<CaracteristiqueMoto>> PostCaracteristiqueMoto(CaracteristiqueMoto caracteristiqueMoto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _caracteristiqueMoto.AddAsync(caracteristiqueMoto);
            return CreatedAtAction("GetCaracteristiqueMotoById", new { id = caracteristiqueMoto.IdCaracteristiqueMoto }, caracteristiqueMoto); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCaracteristiqueMoto")]
        public async Task<IActionResult> DeleteCaracteristiqueMoto(int id)
        {
            var caracteristiqueMoto = await _caracteristiqueMoto.GetByIdAsync(id);
            if (caracteristiqueMoto == null)
            {
                return NotFound();
            }
            await _caracteristiqueMoto.DeleteAsync(caracteristiqueMoto.Value);
            return NoContent();
        }

    }
}
