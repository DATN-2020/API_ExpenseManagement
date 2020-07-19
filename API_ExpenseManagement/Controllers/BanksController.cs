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
    public class BanksController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public BanksController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Banks
        [HttpGet]
        public IEnumerable<Bank> GetBank()
        {
            return _context.Bank;
        }

        // GET: api/Banks/5
        [HttpGet("{id}")]
        public ResponseModel GetBank([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Trip", null, "404");
                return res;
            }

            var bank = _context.Bank.FindAsync(id);

            if (bank == null)
            {
                ResponseModel res = new ResponseModel("Trip", null, "404");
                return res;
            }
            ResponseModel res1 = new ResponseModel("Trip", bank, "404");
            return res1;
        }

        // PUT: api/Banks/5
        [HttpPut("{id}")]
        public ResponseModel PutBank([FromRoute] string id, [FromBody] Bank bank)
        {
            string name = bank.Name_Bank;
            float interest = bank.Interest;
            bank = _context.Bank.Where(m => m.Id_Bank.ToString() == id).FirstOrDefault();
            if (bank == null)
            {
                ResponseModel res1 = new ResponseModel("Update fail", null, "404");
                return res1;
            }
            bank.Name_Bank = name;
            bank.Interest = interest;
            _context.Bank.Update(bank);
            _context.SaveChanges();
            ResponseModel res = new ResponseModel("Update successs", null, "404");
            return res;
        }

        // POST: api/Banks
        [HttpPost]
        public ResponseModel PostBank([FromBody] Bank bank)
        {
            string name = bank.Name_Bank;
            float interset = bank.Interest;
            if (bank != null)
            {
                bank.Name_Bank = name;
                bank.Interest = interset;
                _context.Bank.Add(bank);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Create success", null, "404");
                return res;
            }
            else
            {
                ResponseModel res = new ResponseModel("Create fail", null, "404");
                return res;
            }
        }

        // DELETE: api/Banks/5
        [HttpDelete("{id}")]
        public ResponseModel DeleteBank([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }
            var bank = _context.Bank.Find(id);
            if (bank == null)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }
            else
            {
                _context.Bank.Remove(bank);
                _context.SaveChanges();
                ResponseModel res = new ResponseModel("Delete success", null, "404");
                return res;
            }
        }

        private bool BankExists(int id)
        {
            return _context.Bank.Any(e => e.Id_Bank == id);
        }
    }
}