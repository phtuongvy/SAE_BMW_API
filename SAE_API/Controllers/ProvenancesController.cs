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
    public class ProvenancesController : ControllerBase
    {
        private readonly IDataRepository<Provenance> _provenance;

        public ProvenancesController(IDataRepository<Provenance> provenanceRepository)
        {
            this._provenance = provenanceRepository;
        }

        [HttpGet]
        [ActionName("GetProvenances")]
        public async Task<ActionResult<IEnumerable<Provenance>>> GetProvenances()
        {
            return await _provenance.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetProvenanceById")]
        public async Task<ActionResult<Provenance>> GetProvenanceById(int id)
        {

            var provenance = await _provenance.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (provenance == null)
            {
                return NotFound();
            }
            return provenance;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutProvenance")]
        public async Task<IActionResult> PutProvenance(int id, Provenance provenance)
        {
            if (id != provenance.IdConcessionnaire)
            {
                return BadRequest();
            }
            var userToUpdate = await _provenance.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _provenance.UpdateAsync(userToUpdate.Value, provenance);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostProvenance")]
        public async Task<ActionResult<Provenance>> PostProvenance(Provenance provenance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _provenance.AddAsync(provenance);
            return CreatedAtAction("GetProvenanceById", new { id = provenance.IdConcessionnaire }, provenance); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteProvenance")]
        public async Task<IActionResult> DeleteProvenance(int id)
        {
            var provenance = await _provenance.GetByIdAsync(id);
            if (provenance == null)
            {
                return NotFound();
            }
            await _provenance.DeleteAsync(provenance.Value);
            return NoContent();
        }
    }
}
