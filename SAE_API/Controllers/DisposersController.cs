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
    public class DisposerController : ControllerBase
    {
        private readonly IDataRepository<Disposer> _Disposer;

        public DisposerController(IDataRepository<Disposer> DisposerRepository)
        {
            this._Disposer = DisposerRepository;
        }

        [HttpGet]
        [ActionName("GetDisposers")]
        public async Task<ActionResult<IEnumerable<Disposer>>> GetDisposers()
        {
            return await _Disposer.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdEquipement{id}/IdStock{id2}")]
        [ActionName("GetDisposerById")]
        public async Task<ActionResult<Disposer>> GetDisposerById(int id , int id2)
        {

            var Disposer = await _Disposer.GetByIdAsync(id , id2);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (Disposer == null)
            {
                return NotFound();
            }
            return Disposer;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutDisposer")]
        public async Task<IActionResult> PutDisposer(int id, Disposer Disposer)
        {
            if (id != Disposer.IdEquipement)
            {
                return BadRequest();
            }
            var userToUpdate = await _Disposer.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _Disposer.UpdateAsync(userToUpdate.Value, Disposer);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostDisposer")]
        public async Task<ActionResult<Disposer>> PostDisposer(Disposer Disposer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _Disposer.AddAsync(Disposer);
            return CreatedAtAction("GetDisposerById", new { id = Disposer.IdEquipement }, Disposer); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteDisposer")]
        public async Task<IActionResult> DeleteDisposer(int id)
        {
            var Disposer = await _Disposer.GetByIdAsync(id);
            if (Disposer == null)
            {
                return NotFound();
            }
            await _Disposer.DeleteAsync(Disposer.Value);
            return NoContent();
        }

    }
}
