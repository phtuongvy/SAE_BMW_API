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
    public class CompteClientProfessionnelController : ControllerBase
    {
        private readonly IDataRepository<CompteClientProfessionnel> _CompteClientProfessionnel;

        public CompteClientProfessionnelController(IDataRepository<CompteClientProfessionnel> CompteClientProfessionnelRepository)
        {
            this._CompteClientProfessionnel = CompteClientProfessionnelRepository;
        }

        [HttpGet]
        [ActionName("GetCompteClientProfessionnels")]
        public async Task<ActionResult<IEnumerable<CompteClientProfessionnel>>> GetCompteClientProfessionnels()
        {
            return await _CompteClientProfessionnel.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetCompteClientProfessionnelById")]
        public async Task<ActionResult<CompteClientProfessionnel>> GetCompteClientProfessionnelById(int id)
        {

            var CompteClientProfessionnel = await _CompteClientProfessionnel.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (CompteClientProfessionnel == null)
            {
                return NotFound();
            }
            return CompteClientProfessionnel;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCompteClientProfessionnel")]
        public async Task<IActionResult> PutCompteClientProfessionnel(int id, CompteClientProfessionnel CompteClientProfessionnel)
        {
            if (id != CompteClientProfessionnel.IdCompteClient)
            {
                return BadRequest();
            }
            var userToUpdate = await _CompteClientProfessionnel.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _CompteClientProfessionnel.UpdateAsync(userToUpdate.Value, CompteClientProfessionnel);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCompteClientProfessionnel")]
        public async Task<ActionResult<CompteClientProfessionnel>> PostCompteClientProfessionnel(CompteClientProfessionnel CompteClientProfessionnel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _CompteClientProfessionnel.AddAsync(CompteClientProfessionnel);
            return CreatedAtAction("GetCompteClientProfessionnelById", new { id = CompteClientProfessionnel.IdCompteClient }, CompteClientProfessionnel); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCompteClientProfessionnel")]
        public async Task<IActionResult> DeleteCompteClientProfessionnel(int id)
        {
            var CompteClientProfessionnel = await _CompteClientProfessionnel.GetByIdAsync(id);
            if (CompteClientProfessionnel == null)
            {
                return NotFound();
            }
            await _CompteClientProfessionnel.DeleteAsync(CompteClientProfessionnel.Value);
            return NoContent();
        }

    }
}
