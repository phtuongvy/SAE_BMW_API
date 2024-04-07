using System;
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
        private readonly IDataRepository<Coloris> _coloris;

        public ColorisController(IDataRepository<Coloris> colorisRepository)
        {
            this._coloris = colorisRepository;
        }

        [HttpGet]
        [ActionName("GetColoriss")]
        public async Task<ActionResult<IEnumerable<Coloris>>> GetColoriss()
        {
            return await _coloris.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetColorisById")]
        public async Task<ActionResult<Coloris>> GetColorisById(int id)
        {

            var coloris = await _coloris.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (coloris == null)
            {
                return NotFound();
            }
            return coloris;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutColoris")]
        public async Task<IActionResult> PutColoris(int id, Coloris coloris)
        {
            if (id != coloris.IdColoris)
            {
                return BadRequest();
            }
            var userToUpdate = await _coloris.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _coloris.UpdateAsync(userToUpdate.Value, coloris);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostColoris")]
        public async Task<ActionResult<Coloris>> PostColoris(Coloris coloris)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _coloris.AddAsync(coloris);
            return CreatedAtAction("GetColorisById", new { id = coloris.IdColoris }, coloris); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteColoris")]
        public async Task<IActionResult> DeleteColoris(int id)
        {
            var coloris = await _coloris.GetByIdAsync(id);
            if (coloris == null)
            {
                return NotFound();
            }
            await _coloris.DeleteAsync(coloris.Value);
            return NoContent();
        }

    }
}
