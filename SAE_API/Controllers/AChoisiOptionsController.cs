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
    public class AChoisiOptionController : ControllerBase
    {
        private readonly IDataRepository<AChoisiOption> _AChoisiOption;

        public AChoisiOptionController(IDataRepository<AChoisiOption> AChoisiOptionRepository)
        {
            this._AChoisiOption = AChoisiOptionRepository;
        }

        [HttpGet]
        [ActionName("GetAChoisiOptions")]
        public async Task<ActionResult<IEnumerable<AChoisiOption>>> GetAChoisiOptions()
        {
            return await _AChoisiOption.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IDConfiguaration{id}/IdEquipementMoto{id2}")]
        [ActionName("GetAChoisiOptionById")]
        public async Task<ActionResult<AChoisiOption>> GetAChoisiOptionById(int id , int id2)
        {

            var AChoisiOption = await _AChoisiOption.GetByIdAsync(id , id2);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (AChoisiOption == null)
            {
                return NotFound();
            }
            return AChoisiOption;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("IDConfiguaration{id}/IdEquipementMoto{id2}")]
        [ActionName("PutAChoisiOption")]
        public async Task<IActionResult> PutAChoisiOption(int id, int id2, AChoisiOption AChoisiOption)
        {
            if (id != AChoisiOption.IdConfigurationMoto && id2 != AChoisiOption.IdEquipementMoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _AChoisiOption.GetByIdAsync(id, id2);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _AChoisiOption.UpdateAsync(userToUpdate.Value, AChoisiOption);
                return CreatedAtAction("GetAChoisiOptions", new { id = AChoisiOption.IdConfigurationMoto, AChoisiOption.IdEquipementMoto }, AChoisiOption);
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostAChoisiOption")]
        public async Task<ActionResult<AChoisiOption>> PostAChoisiOption(AChoisiOption AChoisiOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _AChoisiOption.AddAsync(AChoisiOption);
            return CreatedAtAction("GetAChoisiOptions", new { id = AChoisiOption.IdConfigurationMoto, AChoisiOption.IdEquipementMoto }, AChoisiOption); 
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}/{id2}")]
        [ActionName("DeleteAChoisiOption")]
        public async Task<IActionResult> DeleteAChoisiOption(int id, int id2)
        {
            var AChoisiOption = await _AChoisiOption.GetByIdAsync(id, id2);
            if (AChoisiOption == null)
            {
                return NotFound();
            }
            await _AChoisiOption.DeleteAsync(AChoisiOption.Value);
            return NoContent();
        }

    }
}
