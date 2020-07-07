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
    public class PeriodicsController : ControllerBase
    {
        private readonly ExpenseManagementContext _context;

        public PeriodicsController(ExpenseManagementContext context)
        {
            _context = context;
        }

        // GET: api/Periodics
        [HttpGet]
        public IEnumerable<Periodic> GetPeriodic()
        {
            return _context.Periodic;
        }

        // GET: api/Periodics/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeriodic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var periodic = await _context.Periodic.FindAsync(id);

            if (periodic == null)
            {
                return NotFound();
            }

            return Ok(periodic);
        }

        // PUT: api/Periodics/5
        [HttpPut("{id}")]
        public ResponseModel PutPeriodic([FromRoute] int id, [FromBody] Periodic periodic)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }

            if (id != periodic.Id_Per)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }
            try
            {
                if (periodic.Id_Wallet == null)
                {
                    periodic.Id_Wallet = 1;
                }
                _context.Entry(periodic).State = EntityState.Modified;
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Update success", null, "200");
                return res;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodicExists(id))
                {
                    ResponseModel res = new ResponseModel("Not found", null, "200");
                    return res;
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Periodics
        [HttpPost]
        public ResponseModel PostPeriodic([FromBody] Periodic periodic)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Fail", null, "200");
                return res;
            }
            float amount = periodic.Amount_Per;
            string desciption = periodic.Desciption;
            int id_cate = periodic.Id_Cate;
            int id_wallet = periodic.Id_Wallet;
            int id_custom = periodic.Id_Custom;
            try
            {
                if (periodic.Id_Wallet == null)
                {
                    periodic.Id_Wallet = 1;
                }
                _context.Periodic.Add(periodic);
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

        // DELETE: api/Periodics/5
        [HttpDelete("{id}")]
        public ResponseModel DeletePeriodic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ResponseModel res = new ResponseModel("Delete fail", null, "200");
                return res;
            }

            var per = _context.Periodic.Find(id);
            if (per == null)
            {
                ResponseModel res = new ResponseModel("Not found", null, "200");
                return res;
            }
            else
            {
                _context.Periodic.Remove(per);
                _context.SaveChangesAsync();
                ResponseModel res = new ResponseModel("Delete success", null, "200");
                return res;
            }
        }

        private bool PeriodicExists(int id)
        {
            return _context.Periodic.Any(e => e.Id_Per == id);
        }
    }
}