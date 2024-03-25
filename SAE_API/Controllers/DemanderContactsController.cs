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
    public class DemanderContactController : ControllerBase
    {
        private readonly IDataRepository<DemanderContact> _DemanderContact;

        public DemanderContactController(IDataRepository<DemanderContact> DemanderContactRepository)
        {
            this._DemanderContact = DemanderContactRepository;
        }

        [HttpGet]
        [ActionName("GetDemanderContacts")]
        public async Task<ActionResult<IEnumerable<DemanderContact>>> GetDemanderContacts()
        {
            return await _DemanderContact.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetDemanderContactById")]
        public async Task<ActionResult<DemanderContact>> GetDemanderContactById(int id)
        {

            var DemanderContact = await _DemanderContact.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (DemanderContact == null)
            {
                return NotFound();
            }
            return DemanderContact;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutDemanderContact")]
        public async Task<IActionResult> PutDemanderContact(int id, DemanderContact DemanderContact)
        {
            if (id != DemanderContact.IdReservationOffre)
            {
                return BadRequest();
            }
            var userToUpdate = await _DemanderContact.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _DemanderContact.UpdateAsync(userToUpdate.Value, DemanderContact);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostDemanderContact")]
        public async Task<ActionResult<DemanderContact>> PostDemanderContact(DemanderContact DemanderContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _DemanderContact.AddAsync(DemanderContact);
            return CreatedAtAction("GetDemanderContactById", new { id = DemanderContact.IdReservationOffre }, DemanderContact); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteDemanderContact")]
        public async Task<IActionResult> DeleteDemanderContact(int id)
        {
            var DemanderContact = await _DemanderContact.GetByIdAsync(id);
            if (DemanderContact == null)
            {
                return NotFound();
            }
            await _DemanderContact.DeleteAsync(DemanderContact.Value);
            return NoContent();
        }

    }
}
