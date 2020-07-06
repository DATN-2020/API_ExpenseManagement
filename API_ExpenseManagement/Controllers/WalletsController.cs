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
    public class WalletsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public WalletsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Wallets
        [HttpGet]
        public IEnumerable<Wallet> GetWallets([FromBody] WalletsForUser obj)
        {
            int userId = obj.User_Id;
            return _context.Wallets.Where(x => x.User_Id == userId);
        }

        // GET: api/Wallets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWallet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var wallet = await _context.Wallets.FindAsync(id);

            if (wallet == null)
            {
                return NotFound();
            }

            return Ok(wallet);
        }

        // PUT: api/Wallets/5
        [HttpPut("{id}")]
        public ResponseModel PutWallet([FromRoute] int id, [FromBody] Wallet wallet)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }

            if (id != wallet.Id_Wallet)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }
            try
            {
                _context.Entry(wallet).State = EntityState.Modified;
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Update success", null, "404");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletExists(id))
                {
                    ResponseModel res = new ResponseModel("Not found", null, "404");
                    return res;
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/wallet
        [HttpPost]
        public ResponseModel PostCreateWallet([FromBody] CreateWallet createWallet)
        {
            int user_Id = createWallet.User_Id;
            float amount = createWallet.Amount;
            string name = createWallet.Name_Wallet;
            string des = createWallet.Description;
            int typeWallet = createWallet.Id_Type_Wallet;

            Wallet insert = new Wallet();
            var check = false;
            insert.Amount_Wallet = amount;
            insert.User_Id = user_Id;
            if (name == null || name.Equals(""))
            {
                insert.Name_Wallet = "Ví tiền mặt";
            }
            else {
                insert.Name_Wallet = name;
            }

            if (des == null)
            {
                insert.Description = "";
            }
            else {
                insert.Description = des;
            }

            if (typeWallet == null || typeWallet == 0)
            {
                insert.Id_Type_Wallet = 1;
            }
            else {
                insert.Id_Type_Wallet = typeWallet;
            }
           
            User user = _context.Users.Where(x => x.User_Id == user_Id).FirstOrDefault();
            user.Check_Wallet = true;
            try
            {
                _context.Wallets.Add(insert);
                _context.Users.Update(user);
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

        // DELETE: api/Wallets/5
        [HttpDelete("{id}")]
        public ResponseModel DeleteWallet([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }

            Wallet wallet = _context.Wallets.Find(id);
            if (wallet == null)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }
            else
            {
                _context.Wallets.Remove(wallet);
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Delete success", null, "404");
                return res;
            }
        }

        private bool WalletExists(int id)
        {
            return _context.Wallets.Any(e => e.Id_Wallet == id);
        }
    }
}