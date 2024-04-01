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
    public class AChoisiController : ControllerBase
    {
        private readonly IDataRepository<AChoisi> _AChoisi;

        public AChoisiController(IDataRepository<AChoisi> AChoisiRepository)
        {
            this._AChoisi = AChoisiRepository;
        }

        [HttpGet]
        [ActionName("GetAChoisis")]
        public async Task<ActionResult<IEnumerable<AChoisi>>> GetAChoisis()
        {
            return await _AChoisi.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IDPack{id}/IDConfigurationMoto{id2}")]
        [ActionName("GetAChoisiById")]
        public async Task<ActionResult<AChoisi>> GetAChoisiById(int id , int id2)
        {

            var AChoisi = await _AChoisi.GetByIdAsync(id , id2);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (AChoisi == null)
            {
                return NotFound();
            }
            return AChoisi;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutAChoisi")]
        public async Task<IActionResult> PutAChoisi(int id, AChoisi AChoisi)
        {
            if (id != AChoisi.IDPack)
            {
                return BadRequest();
            }
            var userToUpdate = await _AChoisi.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _AChoisi.UpdateAsync(userToUpdate.Value, AChoisi);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAChoisi")]
        public async Task<ActionResult<AChoisi>> PostAChoisi(AChoisi AChoisi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _AChoisi.AddAsync(AChoisi);
            return CreatedAtAction("GetAChoisiById", new { id = AChoisi.IDPack }, AChoisi); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteAChoisi")]
        public async Task<IActionResult> DeleteAChoisi(int id)
        {
            var AChoisi = await _AChoisi.GetByIdAsync(id);
            if (AChoisi == null)
            {
                return NotFound();
            }
            await _AChoisi.DeleteAsync(AChoisi.Value);
            return NoContent();
        }

    }
}
