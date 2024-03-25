using Microsoft.AspNetCore.Mvc;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoDisponiblesController : ControllerBase
    {
        private readonly IDataRepository<MotoDisponible> motoDisponibleManager;

        public MotoDisponiblesController(IDataRepository<MotoDisponible> motoDisponibleRepository)
        {
            this.motoDisponibleManager = motoDisponibleRepository;
        }



        // GET: api/MotoDisponibles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotoDisponible>>> GetMotoDisponibles()
        {
            return await motoDisponibleManager.GetAllAsync();
        }

        // GET: api/MotoDisponibles/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotoDisponible>> GetMotoDisponibleById(int id)
        {
            var motoDisponible = motoDisponibleManager.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (motoDisponible == null)
            {
                return NotFound();
            }
            return await motoDisponible;
        }

        // PUT: api/MotoDisponibles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutMotoDisponible(int id, MotoDisponible motoDisponible)
        {
            if (id != motoDisponible.IdMoto)
            {
                return BadRequest();
            }
            var motoDisponibleToUpdate = await motoDisponibleManager.GetByIdAsync(id);
            if (motoDisponibleToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await motoDisponibleManager.UpdateAsync(motoDisponibleToUpdate.Value, motoDisponible);
                return NoContent();
            }
        }
        // POST: api/MotoDisponibles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MotoDisponible>> PostMotoDisponible(MotoDisponible motoDisponible)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await motoDisponibleManager.AddAsync(motoDisponible);
            return CreatedAtAction("GetById", new { id = motoDisponible.IdMoto }, motoDisponible); // GetById : nom de l’action
        }
        // DELETE: api/MotoDisponibles/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMotoDisponible(int id)
        {
            var motoDisponible = await motoDisponibleManager.GetByIdAsync(id);
            if (motoDisponible == null)
            {
                return NotFound();


            }

            await motoDisponibleManager.DeleteAsync(motoDisponible.Value);
            return NoContent();
        }

    }
}
