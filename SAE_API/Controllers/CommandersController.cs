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
    public class CommanderController : ControllerBase
    {
        private readonly IDataRepository<Commander> _Commander;

        public CommanderController(IDataRepository<Commander> CommanderRepository)
        {
            this._Commander = CommanderRepository;
        }

        [HttpGet]
        [ActionName("GetCommanders")]
        public async Task<ActionResult<IEnumerable<Commander>>> GetCommanders()
        {
            return await _Commander.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdCommande{id}/IdEquipement{id2}/IdConfigurationMoto{id3}")]
        [ActionName("GetCommanderById")]
        public async Task<ActionResult<Commander>> GetCommanderById(int id , int id2 , int id3)
        {

            var Commander = await _Commander.GetByIdAsync(id , id2 , id3);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (Commander == null)
            {
                return NotFound();
            }
            return Commander;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCommander")]
        public async Task<IActionResult> PutCommander(int id, Commander Commander)
        {
            if (id != Commander.IdConfigurationMoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _Commander.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _Commander.UpdateAsync(userToUpdate.Value, Commander);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCommander")]
        public async Task<ActionResult<Commander>> PostCommander(Commander Commander)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _Commander.AddAsync(Commander);
            return CreatedAtAction("GetCommanderById", new { id = Commander.IdCommande }, Commander); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCommander")]
        public async Task<IActionResult> DeleteCommander(int id)
        {
            var Commander = await _Commander.GetByIdAsync(id);
            if (Commander == null)
            {
                return NotFound();
            }
            await _Commander.DeleteAsync(Commander.Value);
            return NoContent();
        }

    }
}
