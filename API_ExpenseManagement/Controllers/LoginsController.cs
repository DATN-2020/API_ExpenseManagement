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
    public class LoginsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public LoginsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Logins
        [HttpGet]
        public IEnumerable<Login> GetLogin()
        {
            return _context.Login;
        }

        // GET: api/Logins/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogin([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var login = await _context.Login.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        // PUT: api/Logins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin([FromRoute] string id, [FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != login.User_Name)
            {
                return BadRequest();
            }

            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
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

        // POST: api/Logins
        [HttpPost]
        public ResponseModel PostLogin([FromBody] Login login)
        {
            string userName = login.User_Name;
            string password = login.Password;
            var log = _context.Users.
            Where(x => x.User_Name.Equals(userName) && x.Password.Equals(password)).FirstOrDefault();
            if (log == null)
            {
                ResponseModel res = new ResponseModel("Login fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Login success", log, "200");
                return res;
            }
        }

        // DELETE: api/Logins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();

            return Ok(login);
        }

        private bool LoginExists(string id)
        {
            return _context.Login.Any(e => e.User_Name == id);
        }
    }
}