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
    public class EquipementAccessoiresController : ControllerBase
    {
        private readonly IDataRepository<EquipementAccessoire> _equipementAccessoire;

        public EquipementAccessoiresController(IDataRepository<EquipementAccessoire> equipementAccessoireRepository)
        {
            this._equipementAccessoire = equipementAccessoireRepository;
        }

        [HttpGet]
        [ActionName("GetEquipementAccessoires")]
        public async Task<ActionResult<IEnumerable<EquipementAccessoire>>> GetEquipementAccessoires()
        {
            return await _equipementAccessoire.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetEquipementAccessoireById")]
        public async Task<ActionResult<EquipementAccessoire>> GetEquipementAccessoireById(int id)
        {

            var equipementAccessoire = await _equipementAccessoire.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (equipementAccessoire == null)
            {
                return NotFound();
            }
            return equipementAccessoire;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutEquipementAccessoire")]
        public async Task<IActionResult> PutEquipementAccessoire(int id, EquipementAccessoire equipementAccessoire)
        {
            if (id != equipementAccessoire.IdEquipementMoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _equipementAccessoire.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _equipementAccessoire.UpdateAsync(userToUpdate.Value, equipementAccessoire);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostEquipementAccessoire")]
        public async Task<ActionResult<EquipementAccessoire>> PostEquipementAccessoire(EquipementAccessoire equipementAccessoire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _equipementAccessoire.AddAsync(equipementAccessoire);
            return CreatedAtAction("GetEquipementAccessoireById", new { id = equipementAccessoire.IdEquipementMoto }, equipementAccessoire); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteEquipementAccessoire")]
        public async Task<IActionResult> DeleteEquipementAccessoire(int id)
        {
            var equipementAccessoire = await _equipementAccessoire.GetByIdAsync(id);
            if (equipementAccessoire == null)
            {
                return NotFound();
            }
            await _equipementAccessoire.DeleteAsync(equipementAccessoire.Value);
            return NoContent();
        }

    }
}
