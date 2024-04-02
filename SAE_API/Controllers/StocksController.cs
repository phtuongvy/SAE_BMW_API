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
    public class StocksController : ControllerBase
    {
        private readonly IDataRepository<Stock> _stock;

        public StocksController(IDataRepository<Stock> stockRepository)
        {
            this._stock = stockRepository;
        }

        [HttpGet]
        [ActionName("GetStocks")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            return await _stock.GetAllAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        [ActionName("GetStockById")]
        public async Task<ActionResult<Stock>> GetStockById(int id)
        {

            var stock = await _stock.GetByIdAsync(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return stock;
        }


        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName("PutStock")]
        public async Task<IActionResult> PutStock(int id, Stock stock)
        {
            if (id != stock.IdStock)
            {
                return BadRequest();
            }
            var userToUpdate = await _stock.GetByIdAsync(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await _stock.UpdateAsync(userToUpdate.Value, stock);
                return NoContent();
            }
        }

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName("PostStock")]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _stock.AddAsync(stock);
            return CreatedAtAction("GetStockById", new { id = stock.IdStock }, stock); // GetById : nom de l’action
        }

        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ActionName("DeleteStock")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _stock.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            await _stock.DeleteAsync(stock.Value);
            return NoContent();
        }
    }
}
