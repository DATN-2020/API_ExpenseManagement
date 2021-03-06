﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ExpenseManagement.Context;
using API_ExpenseManagement.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System.Collections;

namespace API_ExpenseManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getBudgetsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public getBudgetsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/getBudgets
        [HttpGet]
        public IEnumerable<getBudget> GetgetBudget()
        {
            return _context.getBudget;
        }

        // GET: api/getBudgets/5
        [HttpGet("{id}")]
        public ResponseModel GetgetBudget([FromQuery] string id)
        {
            var list = new ArrayList();
            var budget = _context.Budget
                .Where(w => w.Id_Wallet == id);
            foreach (Budget budget1 in budget)
            {
                if (budget1.time_e < DateTime.Today)
                {
                    budget1.isFinnish = true;
                    _context.Budget.Update(budget1);
                }
            }
            _context.SaveChanges();

            var budget_ = _context.Budget
                .Where(w => w.Id_Wallet == id);
            foreach (Budget budget1 in budget_)
            {
                if (budget1.Id_Cate == null)
                {
                    var log = from a in _context.Budget
                              join c in _context.TypeCategories
                              on a.Id_type equals c.Id_type.ToString()
                              //join d in _context.Time_Periodic
                              //on a.id_Time equals d.id_Time.ToString()
                              where (a.Id_Cate == null && a.Id_Budget == budget1.Id_Budget)
                              select new
                              {
                                  idwallet = a.Id_Wallet,
                                  idBudget = a.Id_Budget,
                                  name = c.Name_Type,
                                  image = c.Image_Type,
                                  amount = a.Amount_Budget,
                                  remain = a.Remain,
                                  time_s = a.time_s,
                                  time_e = a.time_e,
                                  time_remain = (a.time_e - DateTime.Now),
                                  check = a.time_e < DateTime.Now ? true : false,
                                  date_time_s = a.time_s,
                                  date_time_e = a.time_e
                                  //(a.id_Time == "1" ? a.time_s.AddDays(1) :
                                  //a.id_Time == "2" ? a.time_s.AddDays(7) :
                                  //a.id_Time == "3" ? DateTime.Today.AddDays(DateTime.DaysInMonth(2020, DateTime.Today.Month) - DateTime.Today.Day) :
                                  //DateTime.Today.AddDays(365 - (a.time_s.DayOfYear - 1)))
                              };
                    var get = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                    foreach(object l in get)
                    {
                        list.Add(l);
                    }    
                }
                if (budget1.Id_type == null)
                {
                    var log = from a in _context.Budget
                              join b in _context.Categories
                              on a.Id_Cate equals b.Id_Cate.ToString()
                              //join d in _context.Time_Periodic
                              //on a.id_Time equals d.id_Time.ToString()
                              where (a.Id_type == null && a.Id_Budget == budget1.Id_Budget)
                              select new
                              {
                                  idwallet = a.Id_Wallet,
                                  idBudget = a.Id_Budget,
                                  name = b.NameCate,
                                  image = b.ImageCate,
                                  amount = a.Amount_Budget,
                                  remain = a.Remain,
                                  time_s = a.time_s,
                                  time_e = a.time_e,
                                  time_remain = (a.time_e - DateTime.Now),
                                  check = a.time_e < DateTime.Now ? true : false,
                                  date_time_s = a.time_s,
                                  date_time_e = a.time_e
                                  //(a.id_Time == "1" ? a.time_s.AddDays(1) :
                                  //a.id_Time == "2" ? a.time_s.AddDays(7) :
                                  //a.id_Time == "3" ? DateTime.Today.AddDays(DateTime.DaysInMonth(2020, DateTime.Today.Month) - DateTime.Today.Day) :
                                  //DateTime.Today.AddDays(365 - (a.time_s.DayOfYear - 1)))
                              };
                    var get = log.Where(m => m.idwallet.Equals(id)).AsEnumerable();
                    foreach (object l in get)
                    {
                        list.Add(l);
                    }
                }
            }
            ResponseModel res1 = new ResponseModel("Budget", list, "200");
            return res1;


        }

        // PUT: api/getBudgets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutgetBudget([FromRoute] int id, [FromBody] getBudget getBudget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != getBudget.id_getBudget)
            {
                return BadRequest();
            }

            _context.Entry(getBudget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!getBudgetExists(id))
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

        // POST: api/getBudgets
        [HttpPost]
        public async Task<IActionResult> PostgetBudget([FromBody] getBudget getBudget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.getBudget.Add(getBudget);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetgetBudget", new { id = getBudget.id_getBudget }, getBudget);
        }

        // DELETE: api/getBudgets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletegetBudget([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getBudget = await _context.getBudget.FindAsync(id);
            if (getBudget == null)
            {
                return NotFound();
            }

            _context.getBudget.Remove(getBudget);
            await _context.SaveChangesAsync();

            return Ok(getBudget);
        }

        private bool getBudgetExists(int id)
        {
            return _context.getBudget.Any(e => e.id_getBudget == id);
        }
    }
}