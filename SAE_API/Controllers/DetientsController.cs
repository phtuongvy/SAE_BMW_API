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
    public class DetientController : ControllerBase
    {
        private readonly IDataRepository<Detient> _Detient;

        public DetientController(IDataRepository<Detient> DetientRepository)
        {
            this._Detient = DetientRepository;
        }

        [HttpGet]
        [ActionName("GetDetients")]
        public async Task<ActionResult<IEnumerable<Detient>>> GetDetients()
        {
            return await _Detient.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetDetientById")]
        public async Task<ActionResult<Detient>> GetDetientById(int id)
        {

            var Detient = await _Detient.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (Detient == null)
            {
                return NotFound();
            }
            return Detient;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutDetient")]
        public async Task<IActionResult> PutDetient(int id, Detient Detient)
        {
            if (id != Detient.IdConcessionnaire)
            {
                return BadRequest();
            }
            var userToUpdate = await _Detient.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _Detient.UpdateAsync(userToUpdate.Value, Detient);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostDetient")]
        public async Task<ActionResult<Detient>> PostDetient(Detient Detient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _Detient.AddAsync(Detient);
            return CreatedAtAction("GetDetientById", new { id = Detient.IdConcessionnaire }, Detient); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteDetient")]
        public async Task<IActionResult> DeleteDetient(int id)
        {
            var Detient = await _Detient.GetByIdAsync(id);
            if (Detient == null)
            {
                return NotFound();
            }
            await _Detient.DeleteAsync(Detient.Value);
            return NoContent();
        }

    }
}
