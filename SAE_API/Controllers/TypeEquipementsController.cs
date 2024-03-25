using Microsoft.AspNetCore.Mvc;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeEquipementsController : ControllerBase
    {
        private readonly IDataRepository<TypeEquipement> typeEquipementManager;

        public TypeEquipementsController(IDataRepository<TypeEquipement> typeEquipementRepository)
        {
            this.typeEquipementManager = typeEquipementRepository;
        }



        // GET: api/TypeEquipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeEquipement>>> GetTypeEquipements()
        {
            return await typeEquipementManager.GetAllAsync();
        }

        // GET: api/TypeEquipements/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeEquipement>> GetTypeEquipementById(int id)
        {
            var typeEquipement = typeEquipementManager.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (typeEquipement == null)
            {
                return NotFound();
            }
            return await typeEquipement;
        }

        // PUT: api/TypeEquipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutTypeEquipement(int id, TypeEquipement typeEquipement)
        {
            if (id != typeEquipement.IdTypeEquipement)
            {
                return BadRequest();
            }
            var typeEquipementToUpdate = await typeEquipementManager.GetByIdAsync(id);
            if (typeEquipementToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await typeEquipementManager.UpdateAsync(typeEquipementToUpdate.Value, typeEquipement);
                return NoContent();
            }
        }
        // POST: api/TypeEquipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeEquipement>> PostTypeEquipement(TypeEquipement typeEquipement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await typeEquipementManager.AddAsync(typeEquipement);
            return CreatedAtAction("GetById", new { id = typeEquipement.IdTypeEquipement }, typeEquipement); // GetById : nom de l’action
        }
        // DELETE: api/TypeEquipements/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTypeEquipement(int id)
        {
            var typeEquipement = await typeEquipementManager.GetByIdAsync(id);
            if (typeEquipement == null)
            {
                return NotFound();


            }

            await typeEquipementManager.DeleteAsync(typeEquipement.Value);
            return NoContent();
        }

    }
}
