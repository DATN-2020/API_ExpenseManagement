using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;
using Newtonsoft.Json.Linq;

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public TransactionsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public IEnumerable<Transactions> GetTransactions()
        {
            return _context.Transactions;
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactions = await _context.Transactions.FindAsync(id);

            if (transactions == null)
            {
                return NotFound();
            }

            return Ok(transactions);
        }

        // PUT: api/Transactions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactions([FromRoute] int id, [FromBody] Transactions transactions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transactions.id_trans)
            {
                return BadRequest();
            }

            _context.Entry(transactions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionsExists(id))
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

        // POST: api/Transactions
        [HttpPost]
        public ResponseModel PostTransactions([FromBody] Transactions transactions)
        {
            float price = transactions.price_trans;
            string name = transactions.name_trans;
            string date = transactions.date_trans;
            string id_saving = transactions.id_saving;
            SavingWallet savingWallet = _context.SavingWallet.Where(m => m.id_saving.ToString() == id_saving).FirstOrDefault();
            if(savingWallet == null)
            {
                ResponseModel res1 = new ResponseModel("Transactions fail", null, "404");
                return res1;
            }
            transactions.price_trans = price;
            transactions.date_trans = date;
            transactions.id_saving = id_saving;
            if(name == "Gửi vào")
            {
                savingWallet.price = savingWallet.price + price;
            }    
            if(name == "Rút ra")
            {
                savingWallet.price = savingWallet.price - price;
            }
            _context.SavingWallet.Update(savingWallet);
            _context.Transactions.Add(transactions);
            _context.SaveChanges();
            ResponseModel res = new ResponseModel("Transactions success", null, "404");
            return res;
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transactions);
            await _context.SaveChangesAsync();

            return Ok(transactions);
        }

        private bool TransactionsExists(int id)
        {
            return _context.Transactions.Any(e => e.id_trans == id);
        }
    }
}