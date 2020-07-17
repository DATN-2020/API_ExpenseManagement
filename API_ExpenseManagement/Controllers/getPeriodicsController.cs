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
    public class getPeriodicsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public getPeriodicsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/getPeriodics
        [HttpGet]
        public IEnumerable<getPeriodic> GetgetPeriodic()
        {
            return _context.getPeriodic;
        }

        // GET: api/getPeriodics/5
        [HttpGet("{id}")]
        public ResponseModel GetgetPeriodic([FromQuery] string id)
        {
            Wallet wallet = _context.Wallets.Where(m => m.Id_Wallet.ToString() == id).FirstOrDefault();
            var per = _context.Periodic.Where(w => w.Id_Wallet == id.ToString());
            foreach (Periodic periodic1 in per)
            {
                //thanh toán định kì theo ngày
                if(periodic1.id_Time == "1" && periodic1.date_s.AddDays(1) == DateTime.Today)
                {
                    Income_Outcome income = new Income_Outcome();
                    income.Amount = periodic1.Amount_Per;
                    income.Description_come = "Thanh toán định kì";
                    income.Date_come = DateTime.Today.ToString();
                    income.Id_Per = periodic1.Id_Per.ToString();
                    income.WalletId_Wallet = periodic1.Id_Wallet;
                    wallet.Amount_now = wallet.Amount_now - periodic1.Amount_Per;
                    _context.Income_Outcomes.Add(income);
                }
                //thanh toán định kì theo tuần
                if (periodic1.id_Time == "2" && periodic1.date_s.AddDays(7) == DateTime.Today)
                {
                    Income_Outcome income = new Income_Outcome();
                    income.Amount = periodic1.Amount_Per;
                    income.Description_come = "Thanh toán định kì";
                    income.Date_come = DateTime.Today.ToString();
                    income.Id_Per = periodic1.Id_Per.ToString();
                    income.WalletId_Wallet = periodic1.Id_Wallet;
                    income.Is_Come = false;
                    wallet.Amount_now = wallet.Amount_now - periodic1.Amount_Per;
                    _context.Income_Outcomes.Add(income);
                }
                //thanh toán định kì theo tháng
                if (periodic1.id_Time == "2" && 
                    periodic1.date_s.AddDays(DateTime.DaysInMonth(periodic1.date_s.Year,periodic1.date_s.Month)) == DateTime.Today)
                {
                    Income_Outcome income = new Income_Outcome();
                    income.Amount = periodic1.Amount_Per;
                    income.Description_come = "Thanh toán định kì";
                    income.Date_come = DateTime.Today.ToString();
                    income.Id_Per = periodic1.Id_Per.ToString();
                    income.WalletId_Wallet = periodic1.Id_Wallet;
                    income.Is_Come = false;
                    wallet.Amount_now = wallet.Amount_now - periodic1.Amount_Per;
                    _context.Income_Outcomes.Add(income);
                }
                //thanh toán định kì theo tháng
                if (periodic1.id_Time == "2" && periodic1.date_s.AddDays(365) == DateTime.Today)
                {
                    Income_Outcome income = new Income_Outcome();
                    income.Amount = periodic1.Amount_Per;
                    income.Description_come = "Thanh toán định kì";
                    income.Date_come = DateTime.Today.ToString();
                    income.Id_Per = periodic1.Id_Per.ToString();
                    income.WalletId_Wallet = periodic1.Id_Wallet;
                    income.Is_Come = false;
                    wallet.Amount_now = wallet.Amount_now - periodic1.Amount_Per;
                    _context.Income_Outcomes.Add(income);
                }
                if (periodic1.date_e <= DateTime.Today)
                {
                    periodic1.isFinnish = true;
                    _context.Periodic.Update(periodic1);
                }
            }
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
            try
            {
                var log = from a in _context.Periodic
                          join b in _context.Categories
                          on a.Id_Cate equals b.Id_Cate.ToString()
                          join c in _context.TypeCategories
                          on a.Id_Type equals c.Id_type.ToString()
                          join d in _context.Time_Periodic
                          on a.id_Time equals d.id_Time.ToString()
                          select new
                          {
                              idwallet = a.Id_Wallet,
                              idPeriodic = a.Id_Per,
                              name = (b.Id_Cate == 1 ? c.Name_Type : b.NameCate),
                              image = (b.Id_Cate == 1 ? c.Image_Type : b.ImageCate),
                              amount = a.Amount_Per,
                              date_s = a.date_s,
                              date_e = a.date_e,
                              time = d.desciption,
                              is_Finish = a.date_e >= DateTime.Now ? false : true,
                              date_time_s = a.date_s,
                              date_time_e =
                              (a.id_Time == "1" ? a.date_s.AddDays(1) :
                              a.id_Time == "2" ? a.date_s.AddDays(7) :
                              a.id_Time == "3" ? DateTime.Today.AddDays(DateTime.DaysInMonth(2020, DateTime.Today.Month) - DateTime.Today.Day) :
                              DateTime.Today.AddDays(365 - (a.date_s.DayOfYear - 1)))
                          };
                var bill = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                
                if (log == null)
                {
                    ResponseModel res = new ResponseModel("Fail", null, "404");
                    return res;
                }
                else
                {
                    ResponseModel res = new ResponseModel("Periodic", bill, "200");
                    return res;
                }
            }
            catch
            {
                ResponseModel res = new ResponseModel("Fail", null, "404");
                return res;
            }
        }

        // PUT: api/getPeriodics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutgetPeriodic([FromRoute] int id, [FromBody] getPeriodic getPeriodic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getPeriodic.id_getBudget)
            {
                return BadRequest();
            }

            _context.Entry(getPeriodic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!getPeriodicExists(id))
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

        // POST: api/getPeriodics
        [HttpPost]
        public async Task<IActionResult> PostgetPeriodic([FromBody] getPeriodic getPeriodic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.getPeriodic.Add(getPeriodic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetgetPeriodic", new { id = getPeriodic.id_getBudget }, getPeriodic);
        }

        // DELETE: api/getPeriodics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletegetPeriodic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getPeriodic = await _context.getPeriodic.FindAsync(id);
            if (getPeriodic == null)
            {
                return NotFound();
            }

            _context.getPeriodic.Remove(getPeriodic);
            await _context.SaveChangesAsync();

            return Ok(getPeriodic);
        }

        private bool getPeriodicExists(int id)
        {
            return _context.getPeriodic.Any(e => e.id_getBudget == id);
        }
    }
}