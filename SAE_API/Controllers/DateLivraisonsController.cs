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
    public class DateLivraisonController : ControllerBase
    {
        private readonly IDataRepository<DateLivraison> _DateLivraison;

        public DateLivraisonController(IDataRepository<DateLivraison> DateLivraisonRepository)
        {
            this._DateLivraison = DateLivraisonRepository;
        }

        [HttpGet]
        [ActionName("GetDateLivraisons")]
        public async Task<ActionResult<IEnumerable<DateLivraison>>> GetDateLivraisons()
        {
            return await _DateLivraison.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetDateLivraisonById")]
        public async Task<ActionResult<DateLivraison>> GetDateLivraisonById(int id)
        {

            var DateLivraison = await _DateLivraison.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (DateLivraison == null)
            {
                return NotFound();
            }
            return DateLivraison;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutDateLivraison")]
        public async Task<IActionResult> PutDateLivraison(int id, DateLivraison DateLivraison)
        {
            if (id != DateLivraison.IdDateLivraison)
            {
                return BadRequest();
            }
            var userToUpdate = await _DateLivraison.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _DateLivraison.UpdateAsync(userToUpdate.Value, DateLivraison);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostDateLivraison")]
        public async Task<ActionResult<DateLivraison>> PostDateLivraison(DateLivraison DateLivraison)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _DateLivraison.AddAsync(DateLivraison);
            return CreatedAtAction("GetDateLivraisonById", new { id = DateLivraison.IdDateLivraison }, DateLivraison); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteDateLivraison")]
        public async Task<IActionResult> DeleteDateLivraison(int id)
        {
            var DateLivraison = await _DateLivraison.GetByIdAsync(id);
            if (DateLivraison == null)
            {
                return NotFound();
            }
            await _DateLivraison.DeleteAsync(DateLivraison.Value);
            return NoContent();
        }

    }
}
