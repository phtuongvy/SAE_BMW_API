﻿using Microsoft.AspNetCore.Mvc;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;

namespace SAE_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PossedersController : ControllerBase
    {
        private readonly IDataRepository<Posseder> possederManager;

        public PossedersController(IDataRepository<Posseder> possederRepository)
        {
            this.possederManager = possederRepository;
        }



        // GET: api/Posseders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posseder>>> GetPosseders()
        {
            return await possederManager.GetAllAsync();
        }

        // GET: api/Posseders/5
        [HttpGet("IdEquipementMoto{id}/IdMoto{id2}")]
        [ActionName("GetPossederById")]
        public async Task<ActionResult<Posseder>> GetPossederById(int id,int id2)
        {
            var posseder = possederManager.GetByIdAsync(id,id2);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (posseder == null)
            {
                return NotFound();
            }
            return await posseder;
        }

        // PUT: api/Posseders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("IdEquipementMoto{id}/IdMoto{id2}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPosseder(int id, int id2, Posseder posseder)
        {
            if (id != posseder.IdMoto)
            {
                return BadRequest();
            }
            var possederToUpdate = await possederManager.GetByIdAsync(id, id2);
            if (possederToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await possederManager.UpdateAsync(possederToUpdate.Value, posseder);
                return NoContent();
            }
        }
        // POST: api/Posseders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Posseder>> PostPosseder(Posseder posseder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await possederManager.AddAsync(posseder);
            return CreatedAtAction("GetById", new { id = posseder.IdMoto }, posseder); // GetById : nom de l’action
        }
        // DELETE: api/Posseders/5
        [HttpDelete("IdEquipementMoto{id}/IdMoto{id2}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePosseder(int id , int id2)
        {
            var posseder = await possederManager.GetByIdAsync(id , id2);
            if (posseder == null)
            {
                return NotFound();


            }

            await possederManager.DeleteAsync(posseder.Value);
            return NoContent();
        }

    }
}
