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
    public class TypeCategoriesController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public TypeCategoriesController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/TypeCategories
        [HttpGet]
        public IEnumerable<TypeCategory> GetTypeCategories()
        {
            return _context.TypeCategories.Include(t=>t.Categories);
        }

        // GET: api/TypeCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeCategory = await _context.TypeCategories.FindAsync(id);

            if (typeCategory == null)
            {
                return NotFound();
            }

            return Ok(typeCategory);
        }

        // PUT: api/TypeCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeCategory([FromRoute] int id, [FromBody] TypeCategory typeCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != typeCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeCategoryExists(id))
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

        // POST: api/TypeCategories
        [HttpPost]
        public async Task<IActionResult> PostTypeCategory([FromBody] TypeCategory typeCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TypeCategories.Add(typeCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeCategory", new { id = typeCategory.Id }, typeCategory);
        }

        // DELETE: api/TypeCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var typeCategory = await _context.TypeCategories.FindAsync(id);
            if (typeCategory == null)
            {
                return NotFound();
            }

            _context.TypeCategories.Remove(typeCategory);
            await _context.SaveChangesAsync();

            return Ok(typeCategory);
        }

        private bool TypeCategoryExists(int id)
        {
            return _context.TypeCategories.Any(e => e.Id == id);
        }
    }
}