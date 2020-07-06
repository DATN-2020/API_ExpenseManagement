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
    public class GetCategoriesController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public GetCategoriesController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/GetCategories
        [HttpGet]
        public IEnumerable<GetCategory> GetGetCategory()
        {
            return _context.GetCategory;
        }

        // GET: api/GetCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getCategory = await _context.GetCategory.FindAsync(id);

            if (getCategory == null)
            {
                return NotFound();
            }

            return Ok(getCategory);
        }

        // PUT: api/GetCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGetCategory([FromRoute] int id, [FromBody] GetCategory getCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getCategory.userId)
            {
                return BadRequest();
            }

            _context.Entry(getCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GetCategoryExists(id))
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

        // POST: api/GetCategories
        [HttpPost]
        public async Task<IActionResult> PostGetCategory([FromBody] GetCategory getCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GetCategory.Add(getCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGetCategory", new { id = getCategory.userId }, getCategory);
        }

        // DELETE: api/GetCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getCategory = await _context.GetCategory.FindAsync(id);
            if (getCategory == null)
            {
                return NotFound();
            }

            _context.GetCategory.Remove(getCategory);
            await _context.SaveChangesAsync();

            return Ok(getCategory);
        }

        private bool GetCategoryExists(int id)
        {
            return _context.GetCategory.Any(e => e.userId == id);
        }
    }
}