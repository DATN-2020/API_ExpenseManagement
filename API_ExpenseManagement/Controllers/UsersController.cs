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
        public ResponseModel PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }

            if (id != user.User_Id)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Update success", null, "404");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    ResponseModel res = new ResponseModel("NotFound", null, "404");
                    return res;
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Users
        [HttpPost]
        public ResponseModel PostUser([FromBody] User user)
        {
            string User_Name = user.User_Name;
            string password = user.Password;
            string full_name = user.FullName;
            bool check_wallet = user.Check_Wallet;
            User user1 = _context.Users.Where(m => m.User_Name == user.User_Name).FirstOrDefault();
            if(user1 != null)
            {
                ResponseModel res = new ResponseModel("User has existed", null, "200");
                return res;    
            }
            else
            {
                user.User_Name = User_Name;
                user.Password = password;
                user.FullName = full_name;
                user.Check_Wallet = check_wallet;
                _context.Users.Add(user);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Create success", null, "200");
                return res;
            }
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