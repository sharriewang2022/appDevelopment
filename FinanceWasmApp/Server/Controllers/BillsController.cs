using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceWasmApp.Server.Models;
using FinanceWasmApp.Shared.Models;
using AntDesign;
using System.Linq.Expressions;

namespace FinanceWasmApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : Controller
    {
        private readonly BillDBContext _context;

        public BillsController(BillDBContext context)
        {
            _context = context;
        }

        // GET: all Bills
        [Route("getAllBillsAsync")]
        [HttpGet]
        public async Task<List<Bill>> Index()
        {
            bool a = _context.gl_bill == null;
            if (a)
            {
                return new List<Bill>();
            }
            List<Bill> billList = await _context.gl_bill.ToListAsync();
            return billList;
        }

        // GET: Bills/Details/5
        [Route("getBillDetailsAsync")]
        [HttpGet]
        public async Task<Bill> Details(int? id)
        {
            if (id == null || _context.gl_bill == null)
            {
                return null;
            }

            var bill = await _context.gl_bill.FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return new Bill();
            }

            return bill;
        }

        // GET: Bills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("createOneBillAsync")]
        public async Task<Bill> Create([Bind("Id,BillType,BillNo,BillName,BillDate,BillMonth,BillYear,BillDirection,InputDate,BillAmount,BillAbstract,Remark,IsAdd")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                RedirectToAction(nameof(Index));
            }
            return bill;
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.gl_bill == null)
            {
                return NotFound();
            }

            var bill = await _context.gl_bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("updateOneBillAsync")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BillType,BillNo,BillName,BillDate,BillMonth,BillYear,BillDirection,InputDate,BillAmount,BillAbstract,Remark,IsAdd")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.gl_bill == null)
            {
                return NotFound();
            }

            var bill = await _context.gl_bill.FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost("deleteOneBillAsync"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.gl_bill == null)
            {
                return Problem("Entity set 'BillDBContext.gl_bill'  is null.");
            }
            var bill = await _context.gl_bill.FindAsync(id);
            if (bill != null)
            {
                _context.gl_bill.Remove(bill);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
          return (_context.gl_bill?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
