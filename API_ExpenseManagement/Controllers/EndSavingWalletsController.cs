using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndSavingWalletsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public EndSavingWalletsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/EndSavingWallets
        [HttpGet]
        public IEnumerable<EndSavingWallet> GetEndSavingWallet()
        {
            return _context.EndSavingWallet;
        }

        // GET: api/EndSavingWallets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEndSavingWallet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var endSavingWallet = await _context.EndSavingWallet.FindAsync(id);

            if (endSavingWallet == null)
            {
                return NotFound();
            }

            return Ok(endSavingWallet);
        }

        // PUT: api/EndSavingWallets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndSavingWallet([FromRoute] int id, [FromBody] EndSavingWallet endSavingWallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != endSavingWallet.id_end)
            {
                return BadRequest();
            }

            _context.Entry(endSavingWallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndSavingWalletExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EndSavingWallets
        [HttpPost]
        public async Task<IActionResult> PostEndSavingWallet([FromBody] EndSavingWallet endSavingWallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EndSavingWallet.Add(endSavingWallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEndSavingWallet", new { id = endSavingWallet.id_end }, endSavingWallet);
        }

        // DELETE: api/EndSavingWallets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndSavingWallet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var endSavingWallet = await _context.EndSavingWallet.FindAsync(id);
            if (endSavingWallet == null)
            {
                return NotFound();
            }

            _context.EndSavingWallet.Remove(endSavingWallet);
            await _context.SaveChangesAsync();

            return Ok(endSavingWallet);
        }

        private bool EndSavingWalletExists(int id)
        {
            return _context.EndSavingWallet.Any(e => e.id_end == id);
        }
    }
}