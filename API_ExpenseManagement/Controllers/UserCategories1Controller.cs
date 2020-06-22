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
    public class UserCategories1Controller : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public UserCategories1Controller(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/UserCategories1
        [HttpGet]
        public IEnumerable<UserCategory> GetUserCategory()
        {
            return _context.UserCategory;
        }

        // GET: api/UserCategories1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCategory = await _context.UserCategory.FindAsync(id);

            if (userCategory == null)
            {
                return NotFound();
            }

            return Ok(userCategory);
        }

        // PUT: api/UserCategories1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCategory([FromRoute] int id, [FromBody] UserCategory userCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userCategory.Id_UserCategory)
            {
                return BadRequest();
            }

            _context.Entry(userCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCategoryExists(id))
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

        // POST: api/UserCategories1
        [HttpPost]
        public async Task<IActionResult> PostUserCategory([FromBody] UserCategory userCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserCategory.Add(userCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCategory", new { id = userCategory.Id_UserCategory }, userCategory);
        }

        // DELETE: api/UserCategories1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCategory = await _context.UserCategory.FindAsync(id);
            if (userCategory == null)
            {
                return NotFound();
            }

            _context.UserCategory.Remove(userCategory);
            await _context.SaveChangesAsync();

            return Ok(userCategory);
        }

        private bool UserCategoryExists(int id)
        {
            return _context.UserCategory.Any(e => e.Id_UserCategory == id);
        }
    }
}