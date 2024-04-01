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
    public class AdressesController : ControllerBase
    {
        private readonly IDataRepository<Adresse> _adresse;

        public AdressesController(IDataRepository<Adresse> adresseRepository)
        {
            this._adresse = adresseRepository;
        }

        // GET: api/Adresses
        [HttpGet]
        [ActionName("GetAdresses")]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresses()
        {
            return await _adresse.GetAllAsync();
        }

        // GET: api/Adresses/5
        [HttpGet("{id}")]
        [ActionName("GetAdresseById")]
        public async Task<ActionResult<Adresse>> GetAdresseById(int id)
        {

            var adresse = await _adresse.GetByIdAsync(id);
            //var utilisateur = await _context.Adresses.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }
            return adresse;
        }


        // PUT: api/Adresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutAdresse")]
        public async Task<IActionResult> PutAdresse(int id, Adresse adresse)
        {
            if (id != adresse.IdAdresse)
            {
                return BadRequest();
            }
            var adresseToUpdate = await _adresse.GetByIdAsync(id);
            if (adresseToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _adresse.UpdateAsync(adresseToUpdate.Value, adresse);
                return NoContent();
            }
        }

        // POST: api/Adresses
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

        // DELETE: api/Adresses/5
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
