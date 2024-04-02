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
    public class EquipementMotoOptionsController : ControllerBase
    {
        private readonly IDataRepository<EquipementAccessoire> _equipementMotoOptions;

        public EquipementMotoOptionsController(IDataRepository<EquipementAccessoire> acquerirRepository)
        {
            this._equipementMotoOptions = acquerirRepository;
        }

        [HttpGet]
        [ActionName("GetEquipementAccessoires")]
        public async Task<ActionResult<IEnumerable<EquipementAccessoire>>> GetEquipementAccessoires()
        {
            return await _equipementMotoOptions.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetEquipementAccessoireById")]
        public async Task<ActionResult<EquipementAccessoire>> GetEquipementAccessoireById(int id)
        {

            var acquerir = await _equipementMotoOptions.GetByIdAsync(id);
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
        [ActionName("PutEquipementAccessoire")]
        public async Task<IActionResult> PutEquipementAccessoire(int id, EquipementAccessoire acquerir)
        {
            if (id != acquerir.IdEquipementMoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _equipementMotoOptions.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _equipementMotoOptions.UpdateAsync(userToUpdate.Value, acquerir);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostEquipementAccessoire")]
        public async Task<ActionResult<EquipementAccessoire>> PostEquipementAccessoire(EquipementAccessoire acquerir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _equipementMotoOptions.AddAsync(acquerir);
            return CreatedAtAction("GetEquipementAccessoireById", new { id = acquerir.IdEquipementMoto }, acquerir); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteEquipementAccessoire")]
        public async Task<IActionResult> DeleteEquipementAccessoire(int id)
        {
            var acquerir = await _equipementMotoOptions.GetByIdAsync(id);
            if (acquerir == null)
            {
                return NotFound();
            }
            await _equipementMotoOptions.DeleteAsync(acquerir.Value);
            return NoContent();
        }

    }
}
