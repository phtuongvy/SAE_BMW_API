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
    public class PresentesController : ControllerBase
    {
        private readonly IDataRepository<Presente> _presente;

        public PresentesController(IDataRepository<Presente> presenteRepository)
        {
            this._presente = presenteRepository;
        }

        [HttpGet]
        [ActionName("GetPresentes")]
        public async Task<ActionResult<IEnumerable<Presente>>> GetPresentes()
        {
            return await _presente.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetPresenteById")]
        public async Task<ActionResult<Presente>> GetPresenteById(int id)
        {

            var presente = await _presente.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (presente == null)
            {
                return NotFound();
            }
            return presente;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutPresente")]
        public async Task<IActionResult> PutPresente(int id, Presente presente)
        {
            if (id != presente.IdPhoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _presente.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _presente.UpdateAsync(userToUpdate.Value, presente);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostPresente")]
        public async Task<ActionResult<Presente>> PostPresente(Presente presente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _presente.AddAsync(presente);
            return CreatedAtAction("GetPresenteById", new { id = presente.IdPhoto }, presente); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeletePresente")]
        public async Task<IActionResult> DeletePresente(int id)
        {
            var presente = await _presente.GetByIdAsync(id);
            if (presente == null)
            {
                return NotFound();
            }
            await _presente.DeleteAsync(presente.Value);
            return NoContent();
        }
    }
}
