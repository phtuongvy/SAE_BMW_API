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
    public class FavorisController : ControllerBase
    {
        private readonly IDataRepository<Favoris> _Favoris;

        public FavorisController(IDataRepository<Favoris> FavorisRepository)
        {
            this._Favoris = FavorisRepository;
        }

        [HttpGet]
        [ActionName("GetFavoriss")]
        public async Task<ActionResult<IEnumerable<Favoris>>> GetFavoriss()
        {
            return await _Favoris.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetFavorisById")]
        public async Task<ActionResult<Favoris>> GetFavorisById(int id , int id2)
        {

            var Favoris = await _Favoris.GetByIdAsync(id, id2);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (Favoris == null)
            {
                return NotFound();
            }
            return Favoris;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutFavoris")]
        public async Task<IActionResult> PutFavoris(int id, int id2, Favoris Favoris)
        {
            if (id != Favoris.IdCompteClient && id2 != Favoris.IdConcessionnaire)
            {
                return BadRequest();
            }
            var userToUpdate = await _Favoris.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _Favoris.UpdateAsync(userToUpdate.Value, Favoris);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostFavoris")]
        public async Task<ActionResult<Favoris>> PostFavoris(Favoris Favoris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _Favoris.AddAsync(Favoris);
            return CreatedAtAction("GetFavorisById", new { id = Favoris.IdCompteClient }, Favoris); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteFavoris")]
        public async Task<IActionResult> DeleteFavoris(int id ,int id2)
        {
            var Favoris = await _Favoris.GetByIdAsync(id , id2);
            if (Favoris == null)
            {
                return NotFound();
            }
            await _Favoris.DeleteAsync(Favoris.Value);
            return NoContent();
        }

    }
}
