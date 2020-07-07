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
    public class CustomsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public CustomsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Customs
        [HttpGet]
        public IEnumerable<Custom> GetCustom()
        {
            return _context.Custom;
        }

        // GET: api/Customs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var custom = await _context.Custom.FindAsync(id);

            if (custom == null)
            {
                return NotFound();
            }

            return Ok(custom);
        }

        // PUT: api/Customs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustom([FromRoute] int id, [FromBody] Custom custom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != custom.Id_Custom)
            {
                return BadRequest();
            }

            _context.Entry(custom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomExists(id))
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

        // POST: api/Customs
        [HttpPost]
        public async Task<IActionResult> PostCustom([FromBody] Custom custom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Custom.Add(custom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustom", new { id = custom.Id_Custom }, custom);
        }

        // DELETE: api/Customs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var custom = await _context.Custom.FindAsync(id);
            if (custom == null)
            {
                return NotFound();
            }

            _context.Custom.Remove(custom);
            await _context.SaveChangesAsync();

            return Ok(custom);
        }

        private bool CustomExists(int id)
        {
            return _context.Custom.Any(e => e.Id_Custom == id);
        }
    }
}