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
    public class TransationsController : ControllerBase
    {
        private readonly IDataRepository<Transation> _transation;

        public TransationsController(IDataRepository<Transation> transationRepository)
        {
            this._transation = transationRepository;
        }

        [HttpGet]
        [ActionName("GetTransations")]
        public async Task<ActionResult<IEnumerable<Transation>>> GetTransations()
        {
            return await _transation.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetTransationById")]
        public async Task<ActionResult<Transation>> GetTransationById(int id)
        {

            var transation = await _transation.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (transation == null)
            {
                return NotFound();
            }
            return transation;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutTransation")]
        public async Task<IActionResult> PutTransation(int id, Transation transation)
        {
            if (id != transation.IdTransaction)
            {
                return BadRequest();
            }
            var userToUpdate = await _transation.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _transation.UpdateAsync(userToUpdate.Value, transation);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostTransation")]
        public async Task<ActionResult<Transation>> PostTransation(Transation transation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _transation.AddAsync(transation);
            return CreatedAtAction("GetTransationById", new { id = transation.IdTransaction }, transation); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteTransation")]
        public async Task<IActionResult> DeleteTransation(int id)
        {
            var transation = await _transation.GetByIdAsync(id);
            if (transation == null)
            {
                return NotFound();
            }
            await _transation.DeleteAsync(transation.Value);
            return NoContent();
        }
    }
}
