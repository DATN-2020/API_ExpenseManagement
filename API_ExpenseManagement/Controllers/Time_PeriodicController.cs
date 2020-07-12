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
    public class Time_PeriodicController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public Time_PeriodicController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Time_Periodic
        [HttpGet]
        public IEnumerable<Time_Periodic> GetTime_Periodic()
        {
            return _context.Time_Periodic;
        }

        // GET: api/Time_Periodic/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTime_Periodic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var time_Periodic = await _context.Time_Periodic.FindAsync(id);

            if (time_Periodic == null)
            {
                return NotFound();
            }

            return Ok(time_Periodic);
        }

        // PUT: api/Time_Periodic/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTime_Periodic([FromRoute] int id, [FromBody] Time_Periodic time_Periodic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != time_Periodic.id_Time)
            {
                return BadRequest();
            }

            _context.Entry(time_Periodic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Time_PeriodicExists(id))
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

        // POST: api/Time_Periodic
        [HttpPost]
        public async Task<IActionResult> PostTime_Periodic([FromBody] Time_Periodic time_Periodic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Time_Periodic.Add(time_Periodic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTime_Periodic", new { id = time_Periodic.id_Time }, time_Periodic);
        }

        // DELETE: api/Time_Periodic/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTime_Periodic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var time_Periodic = await _context.Time_Periodic.FindAsync(id);
            if (time_Periodic == null)
            {
                return NotFound();
            }

            _context.Time_Periodic.Remove(time_Periodic);
            await _context.SaveChangesAsync();

            return Ok(time_Periodic);
        }

        private bool Time_PeriodicExists(int id)
        {
            return _context.Time_Periodic.Any(e => e.id_Time == id);
        }
    }
}