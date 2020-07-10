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
    public class BillsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public BillsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public IEnumerable<Bill> GetBill()
        {
            return _context.Bill;
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBill([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bill = await _context.Bill.FindAsync(id);

            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        // PUT: api/Bills/5
        [HttpPut("{id}")]
        public ResponseModel PutBill([FromRoute] int id, [FromBody] Bill bill)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }

            if (id != bill.Id_Bill)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }
            if (bill.Id_Wallet == 0)
            {
                bill.Id_Wallet = 1;
            }
            if (bill.Id_Cate == 0)
            {
                bill.Id_Cate = 1;
            }
            if (bill.Id_Type == 0)
            {
                bill.Id_Type = 1;
            }
            try
            {
                if(bill.isEdit == false)
                {
                    bill.isPay = true;
                }    
                _context.Entry(bill).State = EntityState.Modified;
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Update success", null, "404");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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

        // POST: api/Bills
        [HttpPost]
        public ResponseModel PostBill([FromBody] Bill bill)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }
            float amount = bill.Amount_Bill;
            string desciption = bill.Desciption;
            DateTime date_s = bill.date_s;
            DateTime date_e = bill.date_e;
            int id_cate = bill.Id_Cate;
            int id_tpye = bill.Id_Type;
            int id_wallet = bill.Id_Wallet;
            int id_custom = bill.Id_Custom;
            bool isPay = bill.isPay;
            bool isDeadline = bill.isDeadline;
            if (bill.Id_Wallet == 0)
            {
                bill.Id_Wallet = 1;
            }
            if (bill.Id_Cate == 0)
            {
                bill.Id_Cate = 1;
            }
            if (bill.Id_Type == 0)
            {
                bill.Id_Type = 1;
            }
            bill.isPay = false;
            bill.isDeadline = false;
            try
            {
                _context.Bill.Add(bill);
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Create success", null, "200");
                return res;
            }
            catch
            {
                ResponseModel res = new ResponseModel("Create fail", null, "200");
                return res;
            }
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public ResponseModel DeleteBill([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "200");
                return res;
            }

            var bill = _context.Bill.Find(id);
            if (bill == null)
            {
                ResponseModel res = new ResponseModel("Not found", null, "200");
                return res;
            }
            else
            {
                _context.Bill.Remove(bill);
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Delete success", null, "200");
                return res;
            }
        }

        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.Id_Bill == id);
        }
    }
}