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
    public class IncomeContactsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public IncomeContactsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/IncomeContacts
        [HttpGet]
        public IEnumerable<IncomeContact> GetIncomeContacts()
        {
            return _context.IncomeContacts;
        }

        // GET: api/IncomeContacts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var incomeContact = await _context.IncomeContacts.FindAsync(id);

            if (incomeContact == null)
            {
                return NotFound();
            }

            return Ok(incomeContact);
        }

        // PUT: api/IncomeContacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncomeContact([FromRoute] int id, [FromBody] IncomeContact incomeContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != incomeContact.Id_IncomeContact)
            {
                return BadRequest();
            }

            _context.Entry(incomeContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeContactExists(id))
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

        // POST: api/IncomeContacts
        [HttpPost]
        public async Task<IActionResult> PostIncomeContact([FromBody] IncomeContact incomeContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.IncomeContacts.Add(incomeContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncomeContact", new { id = incomeContact.Id_IncomeContact }, incomeContact);
        }

        // DELETE: api/IncomeContacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeContact([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var incomeContact = await _context.IncomeContacts.FindAsync(id);
            if (incomeContact == null)
            {
                return NotFound();
            }

            _context.IncomeContacts.Remove(incomeContact);
            await _context.SaveChangesAsync();

            return Ok(incomeContact);
        }

        private bool IncomeContactExists(int id)
        {
            return _context.IncomeContacts.Any(e => e.Id_IncomeContact == id);
        }
    }
}