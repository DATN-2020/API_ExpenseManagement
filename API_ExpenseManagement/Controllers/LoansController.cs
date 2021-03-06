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
    public class LoansController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public LoansController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Loans
        [HttpGet]
        public IEnumerable<Loan> GetLoans()
        {
            return _context.Loans;
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoan([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return NotFound();
            }

            return Ok(loan);
        }

        // PUT: api/Loans/5
        [HttpPut("{id}")]
        public ResponseModel PutLoan([FromRoute] int id, [FromBody] Loan loan)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }

            if (id != loan.Id_Loan)
            {
                ResponseModel res = new ResponseModel("Update fail", null, "404");
                return res;
            }

            

            try
            {
                _context.Entry(loan).State = EntityState.Modified;
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Update success", null, "404");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
                {
                    ResponseModel res = new ResponseModel("Update fail", null, "404");
                    return res;

                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Loans
        [HttpPost]
        public async Task<IActionResult> PostLoan([FromBody] Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoan", new { id = loan.Id_Loan }, loan);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public ResponseModel DeleteLoan([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }

            var loan = _context.Loans.Find(id);
            if (loan == null)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "404");
                return res;
            }
            else
            {
                _context.Loans.Remove(loan);
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Delete success", null, "404");
                return res;
            }
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.Id_Loan == id);
        }
    }
}