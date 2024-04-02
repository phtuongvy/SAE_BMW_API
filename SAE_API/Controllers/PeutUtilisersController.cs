﻿using System;
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
    public class PeutUtilisersController : ControllerBase
    {
        private readonly IDataRepository<PeutUtiliser> _peutUtiliser;

        public PeutUtilisersController(IDataRepository<PeutUtiliser> peutUtiliserRepository)
        {
            this._peutUtiliser = peutUtiliserRepository;
        }

        [HttpGet]
        [ActionName("GetPeutUtilisers")]
        public async Task<ActionResult<IEnumerable<PeutUtiliser>>> GetPeutUtilisers()
        {
            return await _peutUtiliser.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("IdEquipementMoto{id}/IdPack{id2}")]
        [ActionName("GetPeutUtiliserById")]
        public async Task<ActionResult<PeutUtiliser>> GetPeutUtiliserById(int id, int id2)
        {

            var peutUtiliser = await _peutUtiliser.GetByIdAsync(id , id2);

            if (peutUtiliser == null)
            {
                return NotFound();
            }
            return peutUtiliser;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("IdEquipementMoto{id}/IdPack{id2}")]
        [ActionName("PutPeutUtiliser")]
        public async Task<IActionResult> PutPeutUtiliser(int id, int id2, PeutUtiliser peutUtiliser)
        {
            if (id != peutUtiliser.IdPack)
            {
                return BadRequest();
            }
            var userToUpdate = await _peutUtiliser.GetByIdAsync(id, id2);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _peutUtiliser.UpdateAsync(userToUpdate.Value, peutUtiliser);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostPeutUtiliser")]
        public async Task<ActionResult<PeutUtiliser>> PostPeutUtiliser(PeutUtiliser peutUtiliser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _peutUtiliser.AddAsync(peutUtiliser);
            return CreatedAtAction("GetPeutUtiliserById", new { id = peutUtiliser.IdPack }, peutUtiliser); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("IdEquipementMoto{id}/IdPack{id2}")]
        [ActionName("DeletePeutUtiliser")]
        public async Task<IActionResult> DeletePeutUtiliser(int id, int id2)
        {
            var peutUtiliser = await _peutUtiliser.GetByIdAsync(id, id2);
            if (peutUtiliser == null)
            {
                return NotFound();
            }
            await _peutUtiliser.DeleteAsync(peutUtiliser.Value);
            return NoContent();
        }
    }
}
