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
    public class CreateWalletsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public CreateWalletsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/CreateWallets
        [HttpGet]
        public IEnumerable<CreateWallet> GetCreateWallet()
        {
            return _context.CreateWallet;
        }

        // GET: api/CreateWallets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreateWallet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createWallet = await _context.CreateWallet.FindAsync(id);

            if (createWallet == null)
            {
                return NotFound();
            }

            return Ok(createWallet);
        }

        // PUT: api/CreateWallets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreateWallet([FromRoute] int id, [FromBody] CreateWallet createWallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != createWallet.User_Id)
            {
                return BadRequest();
            }

            _context.Entry(createWallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreateWalletExists(id))
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

        // POST: api/CreateWallets
        [HttpPost]
        public ResponseModel PostCreateWallet([FromBody] CreateWallet createWallet)
        {
            int user_Id = createWallet.User_Id;
            float amount = createWallet.Amount;
            Wallet insert = new Wallet();
            var check = false;
            insert.Amount_Wallet = amount;
            insert.User_Id = user_Id;
            insert.Name_Wallet ="Ví tiền mặt";
            insert.Description = "";
            insert.Id_Type_Wallet = 1;
            //insert.Id_Wallet = '1';
            try
            {
                _context.Wallets.Add(insert);
                _context.SaveChanges();
                check = true;
            }
            catch { check = false; }
            if (check == false)
            {
                ResponseModel res = new ResponseModel("Create fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Create success", null, "200");
                return res;
            }
        }

        // DELETE: api/CreateWallets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreateWallet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createWallet = await _context.CreateWallet.FindAsync(id);
            if (createWallet == null)
            {
                return NotFound();
            }

            _context.CreateWallet.Remove(createWallet);
            await _context.SaveChangesAsync();

            return Ok(createWallet);
        }

        private bool CreateWalletExists(int id)
        {
            return _context.CreateWallet.Any(e => e.User_Id == id);
        }
    }
}