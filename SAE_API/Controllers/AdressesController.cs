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
    public class AdressesController : ControllerBase
    {
        private readonly IDataRepository<Adresse> _adresse;

        public AdressesController(IDataRepository<Adresse> adresseRepository)
        {
            this._adresse = adresseRepository;
        }

        [HttpGet]
        [ActionName("GetAdresses")]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresses()
        {
            return await _adresse.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetAdresseById")]
        public async Task<ActionResult<Adresse>> GetAdresseById(int id)
        {

            var adresse = await _adresse.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }
            return adresse;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutAdresse")]
        public async Task<IActionResult> PutAdresse(int id, Adresse adresse)
        {
            if (id != adresse.IdAdresse)
            {
                return BadRequest();
            }
            var userToUpdate = await _adresse.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _adresse.UpdateAsync(userToUpdate.Value, adresse);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAdresse")]
        public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _adresse.AddAsync(adresse);
            return CreatedAtAction("GetAdresseById", new { id = adresse.IdAdresse }, adresse); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteAdresse")]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            var adresse = await _adresse.GetByIdAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }
            await _adresse.DeleteAsync(adresse.Value);
            return NoContent();
        }

    }
}
