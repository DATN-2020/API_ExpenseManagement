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
    public class TransfersController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public TransfersController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Transfers
        [HttpGet]
        public IEnumerable<Transfers> GetTransfers()
        {
            return _context.Transfers;
        }

        // GET: api/Transfers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransfers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transfers = await _context.Transfers.FindAsync(id);

            if (transfers == null)
            {
                return NotFound();
            }

            return Ok(transfers);
        }

        // PUT: api/Transfers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransfers([FromRoute] int id, [FromBody] Transfers transfers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transfers.id_chuyen)
            {
                return BadRequest();
            }

            _context.Entry(transfers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransfersExists(id))
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

        // POST: api/Transfers
        [HttpPost]
        public ResponseModel PostTransfers([FromBody] Transfers transfers)
        {
            int id_chuyen = transfers.id_chuyen;
            int id_nhan = transfers.id_nhan;
            float amount = transfers.amount;
            Wallet wallet_chuyen = _context.Wallets.Where(x => x.Id_Wallet == id_chuyen).FirstOrDefault();
            Wallet wallet_nhan = _context.Wallets.Where(x => x.Id_Wallet == id_nhan).FirstOrDefault();
            try
            {
                    if (wallet_chuyen != null && wallet_nhan != null)
                    {
                        wallet_chuyen.Amount_Wallet = wallet_chuyen.Amount_Wallet - amount;
                        wallet_nhan.Amount_Wallet = wallet_nhan.Amount_Wallet + amount;
                        transfers.id_chuyen = id_chuyen;
                        transfers.id_nhan = id_nhan;
                        transfers.amount = amount;
                        _context.Transfers.Add(transfers);
                        _context.Update(wallet_chuyen);
                        _context.Update(wallet_nhan);
                        _context.SaveChanges();
                        ResponseModel res = new ResponseModel("Transfers success", null, "404");
                        return res;
                    }
                    else
                    {
                        ResponseModel res = new ResponseModel("Does not exits wallet", null, "404");
                        return res;
                    }
            }
            catch
            {
                ResponseModel res = new ResponseModel("Transfers fail", null, "404");
                return res;
            }
        }

        // DELETE: api/Transfers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransfers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transfers = await _context.Transfers.FindAsync(id);
            if (transfers == null)
            {
                return NotFound();
            }

            _context.Transfers.Remove(transfers);
            await _context.SaveChangesAsync();

            return Ok(transfers);
        }

        private bool TransfersExists(int id)
        {
            return _context.Transfers.Any(e => e.id_chuyen == id);
        }
    }
}