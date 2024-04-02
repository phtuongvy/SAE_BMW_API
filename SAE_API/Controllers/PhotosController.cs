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
    public class PhotosController : ControllerBase
    {
        private readonly IDataRepository<Photo> _acquerir;

        public PhotosController(IDataRepository<Photo> acquerirRepository)
        {
            this._acquerir = acquerirRepository;
        }

        [HttpGet]
        [ActionName("GetPhotos")]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            return await _acquerir.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetPhotoById")]
        public async Task<ActionResult<Photo>> GetPhotoById(int id)
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
        [ActionName("PutPhoto")]
        public async Task<IActionResult> PutPhoto(int id, Photo acquerir)
        {
            if (id != acquerir.PhotoId)
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
        [ActionName("PostPhoto")]
        public async Task<ActionResult<Photo>> PostPhoto(Photo acquerir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _acquerir.AddAsync(acquerir);
            return CreatedAtAction("GetPhotoById", new { id = acquerir.PhotoId }, acquerir); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeletePhoto")]
        public async Task<IActionResult> DeletePhoto(int id)
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
