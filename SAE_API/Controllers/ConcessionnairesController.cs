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
    public class ConcessionnairesController : ControllerBase
    {
        private readonly IDataRepository<Concessionnaire> _concessionnaire;

        public ConcessionnairesController(IDataRepository<Concessionnaire> concessionnaireRepository)
        {
            this._concessionnaire = concessionnaireRepository;
        }

        [HttpGet]
        [ActionName("GetConcessionnaires")]
        public async Task<ActionResult<IEnumerable<Concessionnaire>>> GetConcessionnaires()
        {
            return await _concessionnaire.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetConcessionnaireById")]
        public async Task<ActionResult<Concessionnaire>> GetConcessionnaireById(int id)
        {

            var concessionnaire = await _concessionnaire.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (concessionnaire == null)
            {
                return NotFound();
            }
            return concessionnaire;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutConcessionnaire")]
        public async Task<IActionResult> PutConcessionnaire(int id, Concessionnaire concessionnaire)
        {
            if (id != concessionnaire.IdConcessionnaire)
            {
                return BadRequest();
            }
            var userToUpdate = await _concessionnaire.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _concessionnaire.UpdateAsync(userToUpdate.Value, concessionnaire);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostConcessionnaire")]
        public async Task<ActionResult<Concessionnaire>> PostConcessionnaire(Concessionnaire concessionnaire)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _concessionnaire.AddAsync(concessionnaire);
            return CreatedAtAction("GetConcessionnaireById", new { id = concessionnaire.IdConcessionnaire }, concessionnaire); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteConcessionnaire")]
        public async Task<IActionResult> DeleteConcessionnaire(int id)
        {
            var concessionnaire = await _concessionnaire.GetByIdAsync(id);
            if (concessionnaire == null)
            {
                return NotFound();
            }
            await _concessionnaire.DeleteAsync(concessionnaire.Value);
            return NoContent();
        }


    }
}
