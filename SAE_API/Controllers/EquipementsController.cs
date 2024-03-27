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
    public class EquipementsController : ControllerBase
    {
        private readonly IDataRepository<Equipement> _equipementsRepository;

        public EquipementsController(IDataRepository<Equipement> equipementsRepository)
        {
            this._equipementsRepository = equipementsRepository;
        }

        [HttpGet]
        [ActionName("GetEquipement")]
        public async Task<ActionResult<IEnumerable<object>>> GetEquipement()
        {
            return await _equipementsRepository.GetAllAsync1();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetEquipementById")]
        public async Task<ActionResult<object>> GetEquipementById(int id)
        {

            var equipement = await _equipementsRepository.GetByIdCustomAsync1(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (equipement == null)
            {
                return NotFound();
            }
            return equipement;
        }

        // GET : api/Utilisateurs/nom
        [HttpGet("{nom}")]
        [ActionName("GetEquipementByEmail")]
        public async Task<ActionResult<Equipement>> GetEquipementByEmail(string nom)
        {
            var equipement = await _equipementsRepository.GetByStringAsync(nom);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (equipement == null)
            {
                return NotFound();
            }
            return equipement;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutEquipement")]
        public async Task<IActionResult> PutEquipement(int id, Equipement equipement)
        {
            if (id != equipement.IdEquipement)
            {
                return BadRequest();
            }
            var userToUpdate = await _equipementsRepository.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _equipementsRepository.UpdateAsync(userToUpdate.Value, equipement);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostEquipement")]
        public async Task<ActionResult<Equipement>> PostEquipement(Equipement equipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _equipementsRepository.AddAsync(equipement);
            return CreatedAtAction("GetUtilisateurById", new { id = equipement.IdEquipement }, equipement); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteEquipement")]
        public async Task<IActionResult> DeleteEquipement(int id)
        {
            var equipement = await _equipementsRepository.GetByIdAsync(id);
            if (equipement == null)
            {
                return NotFound();
            }
            await _equipementsRepository.DeleteAsync(equipement.Value);
            return NoContent();
        }

    }
}
