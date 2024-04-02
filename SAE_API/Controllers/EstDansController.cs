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
    public class EstDansController : ControllerBase
    {
        private readonly IDataRepository<EstDans> _estDans;

        public EstDansController(IDataRepository<EstDans> estDansRepository)
        {
            this._estDans = estDansRepository;
        }

        [HttpGet]
        [ActionName("GetEstDanss")]
        public async Task<ActionResult<IEnumerable<EstDans>>> GetEstDanss()
        {
            return await _estDans.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdMoto{id}/IdStock{id2}")]
        [ActionName("GetEstDansById")]
        public async Task<ActionResult<EstDans>> GetEstDansById(int id , int id2)
        {

            var estDans = await _estDans.GetByIdAsync(id , id2 );
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (estDans == null)
            {
                return NotFound();
            }
            return estDans;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("IdMoto{id}/IdStock{id2}")]
        [ActionName("PutEstDans")]
        public async Task<IActionResult> PutEstDans(int id,int id2, EstDans estDans)
        {
            if (id != estDans.IdMoto)
            {
                return BadRequest();
            }
            var userToUpdate = await _estDans.GetByIdAsync(id,id2);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _estDans.UpdateAsync(userToUpdate.Value, estDans);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostEstDans")]
        public async Task<ActionResult<EstDans>> PostEstDans(EstDans estDans)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _estDans.AddAsync(estDans);
            return CreatedAtAction("GetEstDansById", new { id = estDans.IdMoto }, estDans); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("IdMoto{id}/IdStock{id2}")]
        [ActionName("DeleteEstDans")]
        public async Task<IActionResult> DeleteEstDans(int id, int id2)
        {
            var estDans = await _estDans.GetByIdAsync(id, id2);
            if (estDans == null)
            {
                return NotFound();
            }
            await _estDans.DeleteAsync(estDans.Value);
            return NoContent();
        }

    }
}
