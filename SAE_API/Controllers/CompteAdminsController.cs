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
    public class CompteAdminController : ControllerBase
    {
        private readonly IDataRepository<CompteAdmin> _CompteAdmin;

        public CompteAdminController(IDataRepository<CompteAdmin> CompteAdminRepository)
        {
            this._CompteAdmin = CompteAdminRepository;
        }

        [HttpGet]
        [ActionName("GetCompteAdmins")]
        public async Task<ActionResult<IEnumerable<CompteAdmin>>> GetCompteAdmins()
        {
            return await _CompteAdmin.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetCompteAdminById")]
        public async Task<ActionResult<CompteAdmin>> GetCompteAdminById(int id)
        {

            var CompteAdmin = await _CompteAdmin.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (CompteAdmin == null)
            {
                return NotFound();
            }
            return CompteAdmin;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCompteAdmin")]
        public async Task<IActionResult> PutCompteAdmin(int id, CompteAdmin CompteAdmin)
        {
            if (id != CompteAdmin.IdCompteClient)
            {
                return BadRequest();
            }
            var userToUpdate = await _CompteAdmin.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _CompteAdmin.UpdateAsync(userToUpdate.Value, CompteAdmin);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCompteAdmin")]
        public async Task<ActionResult<CompteAdmin>> PostCompteAdmin(CompteAdmin CompteAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _CompteAdmin.AddAsync(CompteAdmin);
            return CreatedAtAction("GetCompteAdminById", new { id = CompteAdmin.IdCompteClient }, CompteAdmin); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCompteAdmin")]
        public async Task<IActionResult> DeleteCompteAdmin(int id)
        {
            var CompteAdmin = await _CompteAdmin.GetByIdAsync(id);
            if (CompteAdmin == null)
            {
                return NotFound();
            }
            await _CompteAdmin.DeleteAsync(CompteAdmin.Value);
            return NoContent();
        }

    }
}
