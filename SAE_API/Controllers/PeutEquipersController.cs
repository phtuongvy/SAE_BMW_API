using Microsoft.AspNetCore.Mvc;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PeutEquipersController : ControllerBase
    {
        private readonly IDataRepository<PeutEquiper> peutEquiperManager;

        public PeutEquipersController(IDataRepository<PeutEquiper> peutEquiperRepository)
        {
            this.peutEquiperManager = peutEquiperRepository;
        }



        // GET: api/PeutEquipers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeutEquiper>>> GetPeutEquipers()
        {
            return await peutEquiperManager.GetAllAsync();
        }

        // GET: api/PeutEquipers/5
        [HttpGet("IdPack{id}/IdMoto{id2}")]
        [ActionName("GetById")]
        public async Task<ActionResult<PeutEquiper>> GetPeutEquiperById(int id, int id2)
        {
            var peutEquiper = peutEquiperManager.GetByIdAsync(id, id2);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (peutEquiper == null)
            {
                return NotFound();
            }
            return await peutEquiper;
        }

        // PUT: api/PeutEquipers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("IdPack{id}/IdMoto{id2}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPeutEquiper(int id,int id2, PeutEquiper peutEquiper)
        {
            if (id != peutEquiper.IdMoto)
            {
                return BadRequest();
            }
            var peutEquiperToUpdate = await peutEquiperManager.GetByIdAsync(id, id2);
            if (peutEquiperToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await peutEquiperManager.UpdateAsync(peutEquiperToUpdate.Value, peutEquiper);
                return NoContent();
            }
        }
        // POST: api/PeutEquipers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PeutEquiper>> PostPeutEquiper(PeutEquiper peutEquiper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await peutEquiperManager.AddAsync(peutEquiper);
            return CreatedAtAction("GetById", new { id = peutEquiper.IdMoto }, peutEquiper); // GetById : nom de l’action
        }
        // DELETE: api/PeutEquipers/5
        [HttpDelete("IdPack{id}/IdMoto{id2}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePeutEquiper(int id, int id2)
        {
            var peutEquiper = await peutEquiperManager.GetByIdAsync(id, id2);
            if (peutEquiper == null)
            {
                return NotFound();


            }

            await peutEquiperManager.DeleteAsync(peutEquiper.Value);
            return NoContent();
        }

    }
}
