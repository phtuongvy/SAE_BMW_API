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
    public class IllustrersController : ControllerBase
    {
        private readonly IDataRepository<Illustrer> _illustrer;

        public IllustrersController(IDataRepository<Illustrer> illustrerRepository)
        {
            this._illustrer = illustrerRepository;
        }

        [HttpGet]
        [ActionName("GetIllustrers")]
        public async Task<ActionResult<IEnumerable<Illustrer>>> GetIllustrers()
        {
            return await _illustrer.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdMoto{id}/IdPhoto{id2}")]
        [ActionName("GetIllustrerById")]
        public async Task<ActionResult<Illustrer>> GetIllustrerById(int id , int id2)
        {

            var illustrer = await _illustrer.GetByIdAsync(id , id2);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (illustrer == null)
            {
                return NotFound();
            }
            return illustrer;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("IdMoto{id}/IdPhoto{id2}")]
        [ActionName("PutIllustrer")]
        public async Task<IActionResult> PutIllustrer(int id, int id2,Illustrer illustrer)
        {
            if (id != illustrer.IdPhoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _illustrer.GetByIdAsync(id, id2);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _illustrer.UpdateAsync(userToUpdate.Value, illustrer);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostIllustrer")]
        public async Task<ActionResult<Illustrer>> PostIllustrer(Illustrer illustrer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _illustrer.AddAsync(illustrer);
            return CreatedAtAction("GetIllustrerById", new { id = illustrer.IdPhoto }, illustrer); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        
        [HttpDelete("IdMoto{id}/IdPhoto{id2}")]
        [ActionName("DeleteIllustrer")]
        public async Task<IActionResult> DeleteIllustrer(int id, int id2)
        {
            var illustrer = await _illustrer.GetByIdAsync(id,id2);
            if (illustrer == null)
            {
                return NotFound();
            }
            await _illustrer.DeleteAsync(illustrer.Value);
            return NoContent();
        }
    }
}
