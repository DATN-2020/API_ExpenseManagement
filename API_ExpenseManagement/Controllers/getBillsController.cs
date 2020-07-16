﻿using System;
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
    public class getBillsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public getBillsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/getBills
        [HttpGet]
        public IEnumerable<getBill> GetgetBill()
        {
            return _context.getBill;
        }

        // GET: api/getBills/5
        [HttpGet("{id}")]
        public ResponseModel GetgetBill([FromQuery] string id)
        {
            var log = from a in _context.Bill
                      join b in _context.Categories
                      on a.Id_Category equals b.Id_Cate.ToString()
                      join c in _context.TypeCategories
                      on a.Id_Type equals c.Id_type.ToString()
                      join d in _context.Time_Periodic
                      on a.id_Time equals d.id_Time.ToString()
                      select new
                      {
                          idwallet = a.Id_Wallet,
                          idBill = a.Id_Bill,
                          name = (b.Id_Cate == 1 ? c.Name_Type : b.NameCate),
                          image = (b.Id_Cate == 1 ? c.Image_Type : b.ImageCate),
                          amount = a.Amount_Bill,
                          date_s = a.date_s,
                          date_e = a.date_e,
                          isPay = a.isPay,
                          isDeadline = a.date_e >= DateTime.Now ? false : true,
                          time = d.desciption,
                          date_time_s = a.date_s,
                          date_time_e = 
                          (a.id_Time == "1" ? a.date_s.AddDays(1):
                          a.id_Time == "2" ? a.date_s.AddDays(7):
                          a.id_Time == "3" ? DateTime.Today.AddDays(DateTime.DaysInMonth(2020, DateTime.Today.Month)- DateTime.Today.Day) :
                          DateTime.Today.AddDays(365-(a.date_s.DayOfYear -1)))
                      };
            var bill = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
            var bills = _context.Bill.Where(w => w.Id_Wallet == id.ToString());
            foreach (Bill bill1 in bills)
            {
                if (bill1.date_e <= DateTime.Today)
                {
                    bill1.isFinnish = true;
                    _context.Bill.Update(bill1);
                }
            }
            _context.SaveChanges();
            if (log == null)
            {
                ResponseModel res = new ResponseModel("Fail", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Bill", bill, "200");
                return res;
            }
        }

        // PUT: api/getBills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutgetBill([FromRoute] int id, [FromBody] getBill getBill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getBill.id_getBill)
            {
                return BadRequest();
            }

            _context.Entry(getBill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!getBillExists(id))
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

        // POST: api/getBills
        [HttpPost]
        public async Task<IActionResult> PostgetBill([FromBody] getBill getBill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.getBill.Add(getBill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetgetBill", new { id = getBill.id_getBill }, getBill);
        }

        // DELETE: api/getBills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletegetBill([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getBill = await _context.getBill.FindAsync(id);
            if (getBill == null)
            {
                return NotFound();
            }

            _context.getBill.Remove(getBill);
            await _context.SaveChangesAsync();

            return Ok(getBill);
        }

        private bool getBillExists(int id)
        {
            return _context.getBill.Any(e => e.id_getBill == id);
        }
    }
}