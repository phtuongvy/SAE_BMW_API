using Microsoft.AspNetCore.Mvc;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimersController : ControllerBase
    {
        private readonly IDataRepository<Estimer> estimerManager;

        public EstimersController(IDataRepository<Estimer> estimerRepository)
        {
            this.estimerManager = estimerRepository;
        }



        // GET: api/Estimers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estimer>>> GetEstimers()
        {
            return await estimerManager.GetAllAsync();
        }

        // GET: api/Estimers/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Estimer>> GetEstimerById(int id)
        {
            var estimer = estimerManager.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (estimer == null)
            {
                return NotFound();
            }
            return await estimer;
        }

        // PUT: api/Estimers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutEstimer(int id, Estimer estimer)
        {
            if (id != estimer.IdCompteClient)
            {
                return BadRequest();
            }
            var estimerToUpdate = await estimerManager.GetByIdAsync(id);
            if (estimerToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await estimerManager.UpdateAsync(estimerToUpdate.Value, estimer);
                return NoContent();
            }
        }
        // POST: api/Estimers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Estimer>> PostEstimer(Estimer estimer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await estimerManager.AddAsync(estimer);
            return CreatedAtAction("GetById", new { id = estimer.IdCompteClient }, estimer); // GetById : nom de l’action
        }
        // DELETE: api/Estimers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEstimer(int id)
        {
            var estimer = await estimerManager.GetByIdAsync(id);
            if (estimer == null)
            {
                return NotFound();


            }

            await estimerManager.DeleteAsync(estimer.Value);
            return NoContent();
        }

    }
}
