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
    public class ColorisController : ControllerBase
    {
        private readonly IDataRepository<Collection> _collection;

        public ColorisController(IDataRepository<Collection> collectionRepository)
        {
            this._collection = collectionRepository;
        }

        [HttpGet]
        [ActionName("GetCollections")]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollections()
        {
            return await _collection.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetCollectionById")]
        public async Task<ActionResult<Collection>> GetCollectionById(int id)
        {

            var collection = await _collection.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            return collection;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutCollection")]
        public async Task<IActionResult> PutCollection(int id, Collection collection)
        {
            if (id != collection.IdCollection)
            {
                return BadRequest();
            }
            var userToUpdate = await _collection.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _collection.UpdateAsync(userToUpdate.Value, collection);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostCollection")]
        public async Task<ActionResult<Collection>> PostCollection(Collection collection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _collection.AddAsync(collection);
            return CreatedAtAction("GetCollectionById", new { id = collection.IdCollection }, collection); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteCollection")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            var collection = await _collection.GetByIdAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            await _collection.DeleteAsync(collection.Value);
            return NoContent();
        }

    }
}
