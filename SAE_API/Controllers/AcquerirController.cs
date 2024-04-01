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
    public class AcquerirController : ControllerBase
    {
        private readonly IDataRepository<Acquerir> _acquerir;

        public AcquerirController(IDataRepository<Acquerir> acquerirRepository)
        {
            this._acquerir = acquerirRepository;
        }

        [HttpGet]
        [ActionName("GetAcquerirs")]
        public async Task<ActionResult<IEnumerable<Acquerir>>> GetAcquerirs()
        {
            return await _acquerir.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdCompteClient{id}/IdCb{id2}")]
        [ActionName("GetAcquerirById")]
        public async Task<ActionResult<Acquerir>> GetAcquerirById(int id)
        {

            var acquerir = await _acquerir.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (acquerir == null)
            {
                return NotFound();
            }
            return acquerir;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutAcquerir")]
        public async Task<IActionResult> PutAcquerir(int id, Acquerir acquerir)
        {
            if (id != acquerir.IdCb)
            {
                return BadRequest();
            }
            var userToUpdate = await _acquerir.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _acquerir.UpdateAsync(userToUpdate.Value, acquerir);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAcquerir")]
        public async Task<ActionResult<Acquerir>> PostAcquerir(Acquerir acquerir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _acquerir.AddAsync(acquerir);
            return CreatedAtAction("GetAcquerirById", new { id = acquerir.IdCb }, acquerir); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteAcquerir")]
        public async Task<IActionResult> DeleteAcquerir(int id)
        {
            var acquerir = await _acquerir.GetByIdAsync(id);
            if (acquerir == null)
            {
                return NotFound();
            }
            await _acquerir.DeleteAsync(acquerir.Value);
            return NoContent();
        }

    }
}
