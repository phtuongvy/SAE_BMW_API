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
    public class APourTaillesController : ControllerBase
    {
        private readonly IDataRepository<APourTaille> _aPourTailles;
        
        public APourTaillesController(IDataRepository<APourTaille> aPourTaillesRepository)
        {
            this._aPourTailles = aPourTaillesRepository;
        }

        [HttpGet]
        [ActionName("GetAPourTailless")]
        public async Task<ActionResult<IEnumerable<APourTaille>>> GetAPourTailles()
        {
            return await _aPourTailles.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdEquipement{id}/IdTailleEquipement{id2}")]
        [ActionName("GetAPourTaillesById")]
        public async Task<ActionResult<APourTaille>> GetAPourTaillesById(int id, int id2)
        {
            var aPourTailles = await _aPourTailles.GetByIdAsync(id, id2 );
            if (aPourTailles == null)
            {
                return NotFound();
            }
            return aPourTailles;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("IdCaracteristiqueMoto{id}/IdMoto{id2}")]
        [ActionName("PutAPourTailles")]
        public async Task<IActionResult> PutAPourTailles(int id,int id2, APourTaille aPourTailles)
        {
            if (id != aPourTailles.IdTailleEquipement)
            {
                return BadRequest();
            }
            var userToUpdate = await _aPourTailles.GetByIdAsync(id, id2);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _aPourTailles.UpdateAsync(userToUpdate.Value, aPourTailles);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAPourTailles")]
        public async Task<ActionResult<APourTaille>> PostAPourTailles(APourTaille aPourTailles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _aPourTailles.AddAsync(aPourTailles);
            return CreatedAtAction("GetAPourTaillesById", new { id = aPourTailles.IdTailleEquipement }, aPourTailles); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("IdEquipement{id}/IdTailleEquipement{id2}")]
        [ActionName("DeleteAPourTailles")]
        public async Task<IActionResult> DeleteAPourTailles(int id, int id2)
        {
            var aPourTailles = await _aPourTailles.GetByIdAsync(id, id2);
            if (aPourTailles == null)
            {
                return NotFound();
            }
            await _aPourTailles.DeleteAsync(aPourTailles.Value);
            return NoContent();
        }

    }
}
