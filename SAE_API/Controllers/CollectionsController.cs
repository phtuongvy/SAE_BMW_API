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
    public class CollectionsController : ControllerBase
    {
        private readonly IDataRepository<Collection> _acquerir;

        public CollectionsController(IDataRepository<Collection> acquerirRepository)
        {
            this._acquerir = acquerirRepository;
        }

        [HttpGet]
        [ActionName("GetCollections")]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollections()
        {
            return await _acquerir.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetCollectionById")]
        public async Task<ActionResult<Collection>> GetCollectionById(int id)
        {

            var acquerir = await _acquerir.GetByIdAsync(id);
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
        [ActionName("PutCollection")]
        public async Task<IActionResult> PutCollection(int id, Collection acquerir)
        {
            if (id != acquerir.IdCollection)
            {
                return BadRequest();
            }
            var userToUpdate = await _acquerir.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _acquerir.UpdateAsync(userToUpdate.Value, acquerir);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCollection")]
        public async Task<ActionResult<Collection>> PostCollection(Collection acquerir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _acquerir.AddAsync(acquerir);
            return CreatedAtAction("GetCollectionById", new { id = acquerir.IdCollection }, acquerir); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCollection")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            var acquerir = await _acquerir.GetByIdAsync(id);
            if (acquerir == null)
            {
                return NotFound();
            }
            await _acquerir.DeleteAsync(acquerir.Value);
            return NoContent();
        }

    }
}
