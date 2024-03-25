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
    public class PacksController : ControllerBase
    {
        private readonly IDataRepository<Pack> _pack;

        public PacksController(IDataRepository<Pack> packRepository)
        {
            this._pack = packRepository;
        }

        [HttpGet]
        [ActionName("GetPacks")]
        public async Task<ActionResult<IEnumerable<Pack>>> GetPacks()
        {
            return await _pack.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetPackById")]
        public async Task<ActionResult<Pack>> GetPackById(int id)
        {

            var pack = await _pack.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (pack == null)
            {
                return NotFound();
            }
            return pack;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutPack")]
        public async Task<IActionResult> PutPack(int id, Pack pack)
        {
            if (id != pack.PackId)
            {
                return BadRequest();
            }
            var userToUpdate = await _pack.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _pack.UpdateAsync(userToUpdate.Value, pack);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostPack")]
        public async Task<ActionResult<Pack>> PostPack(Pack pack)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _pack.AddAsync(pack);
            return CreatedAtAction("GetPackById", new { id = pack.PackId }, pack); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeletePack")]
        public async Task<IActionResult> DeletePack(int id)
        {
            var pack = await _pack.GetByIdAsync(id);
            if (pack == null)
            {
                return NotFound();
            }
            await _pack.DeleteAsync(pack.Value);
            return NoContent();
        }
    }
}
