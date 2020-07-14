using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;
using System.Net.Http;

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetWalletsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public GetWalletsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/GetWallets
        [HttpGet]
        public IEnumerable<GetWallet> GetGetWallet()
        {
            return _context.GetWallet;
        }

        // GET: api/GetWallets/5
        [HttpGet("{id}")]
        public ResponseModel GetGetWallet([FromQuery] int id)
        {
            Wallet wallet = _context.Wallets.Where(x => x.User_Id == id).FirstOrDefault();
            int userId = id;
            var log = _context.Wallets.
            Where(x => x.User_Id.Equals(userId)).AsEnumerable();
            //var queryUrl = "/api/GetWallets/5?userId=" + id;  
            if (log == null)
            {
                ResponseModel res = new ResponseModel("Fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Wallets", log, "200");
                return res;
            }
        }

        // PUT: api/GetWallets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGetWallet([FromRoute] int id, [FromBody] GetWallet getWallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getWallet.Userid)
            {
                return BadRequest();
            }

            _context.Entry(getWallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GetWalletExists(id))
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

        // POST: api/GetWallets
        [HttpPost]
        public async Task<IActionResult> PostGetWallet([FromBody] GetWallet getWallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.GetWallet.Add(getWallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGetWallet", new { id = getWallet.Userid }, getWallet);
        }

        // DELETE: api/GetWallets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGetWallet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getWallet = await _context.GetWallet.FindAsync(id);
            if (getWallet == null)
            {
                return NotFound();
            }

            _context.GetWallet.Remove(getWallet);
            await _context.SaveChangesAsync();

            return Ok(getWallet);
        }

        private bool GetWalletExists(int id)
        {
            return _context.GetWallet.Any(e => e.Userid == id);
        }
    }
}