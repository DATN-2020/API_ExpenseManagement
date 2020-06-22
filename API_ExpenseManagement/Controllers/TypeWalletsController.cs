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
    public class TypeWalletsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public TypeWalletsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/TypeWallets
        [HttpGet]
        public IEnumerable<TypeWallet> GetTypeWallets()
        {
            return _context.TypeWallets;
        }

        // GET: api/TypeWallets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeWallet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeWallet = await _context.TypeWallets.FindAsync(id);

            if (typeWallet == null)
            {
                return NotFound();
            }

            return Ok(typeWallet);
        }

        // PUT: api/TypeWallets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeWallet([FromRoute] int id, [FromBody] TypeWallet typeWallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeWallet.Id_Type_Wallet)
            {
                return BadRequest();
            }

            _context.Entry(typeWallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeWalletExists(id))
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

        // POST: api/TypeWallets
        [HttpPost]
        public async Task<IActionResult> PostTypeWallet([FromBody] TypeWallet typeWallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TypeWallets.Add(typeWallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeWallet", new { id = typeWallet.Id_Type_Wallet }, typeWallet);
        }

        // DELETE: api/TypeWallets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeWallet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeWallet = await _context.TypeWallets.FindAsync(id);
            if (typeWallet == null)
            {
                return NotFound();
            }

            _context.TypeWallets.Remove(typeWallet);
            await _context.SaveChangesAsync();

            return Ok(typeWallet);
        }

        private bool TypeWalletExists(int id)
        {
            return _context.TypeWallets.Any(e => e.Id_Type_Wallet == id);
        }
    }
}