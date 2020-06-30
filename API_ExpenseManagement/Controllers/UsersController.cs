using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
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
    public class UsersController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public UsersController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _context.Users.FindAsync(id);
            var wallet = _context.Wallets.
            Where(x => x.User_Id.Equals(user.User_Id)).FirstOrDefault();
            if (user == null)
            {
                user.Check_Wallet = false;
            }
            else
            {
                user.Check_Wallet = true;
            }
            bool ck = false;
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.
            Where(x => x.User_Id.Equals(wallet.User_Id)).FirstOrDefault();
            if (user == null)
            {
                user.Check_Wallet = ck;
            }
            else
            {
                ck = true;
                user.Check_Wallet = ck;
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.User_Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            //string userName = user.User_Name;
            //string password = user.Password;
            //var log = _context.Users.
            //Where(x => x.User_Id.Equals(userName) && x.Password.Equals(password)).FirstOrDefault();
            //if (log == null)
            //{
            //    ResponseModel res = new ResponseModel("Login fail", null, "404");
            //    return res;
            //}
            //else
            //{
            //    ResponseModel res = new ResponseModel("Login success", log, "200");
            //    return res;
            //}
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.User_Id }, user);

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.User_Id == id);
        }
        //[HttpPost]
        //public ResponseModel employeeLogin(FormCollection form)
        //{
        //    string userName = form["userName"];
        //    string password = form["password"];
        //    var log = _context.Users.
        //    Where(x => x.User_Id.Equals(userName) && x.Password.Equals(password)).FirstOrDefault();
        //    if (log == null)
        //    {
        //        ResponseModel res = new ResponseModel("Login fail", null, "404");
        //        return res;
        //    }
        //    else
        //    {
        //        ResponseModel res = new ResponseModel("Login success", log, "200");
        //        return res;
        //    }
        //}
    }
}