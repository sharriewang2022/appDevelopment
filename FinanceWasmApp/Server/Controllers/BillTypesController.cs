using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceWasmApp.Server.Models;
using FinanceWasmApp.Shared.Models;
using static System.Net.WebRequestMethods;

namespace FinanceWasmApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillTypesController : Controller
    {
        private readonly BillDBContext _context;

        private String StatusMessage = String.Empty;

        private static readonly string[] BillTypeArray = new[]
        {
        "salary","business cost", "material fees", "journey costs", "entertainment fees",
        "education fees", "property fees", "water costs", "electricity costs",
        "heating costs", "fuel cost", "others"
         };

        public BillTypesController(BillDBContext context)
        {
            _context = context;
        }

        // GET: BillTypes
        [Route("getAllBillTypsAsync")]
        [HttpGet]
        public async Task<List<BillType>> Index()
        {
            try
            {
                bool a = _context.gl_billType == null;
                if (a) {
                    return new List<BillType>();
                }
                List<BillType> billTypeList = await _context.gl_billType.ToListAsync();
                if (billTypeList == null || billTypeList.Count ==0 ) {
                        //initial bill type in the database
                        for (int i = 1; i <= BillTypeArray.Length; i++)
                        {
                            BillType billType = new BillType();
                            billType.BillTypeNo = i + "";
                            billType.BillTypeName = BillTypeArray[i];
                            billType.IsAdd = 0;
                            billType.Remark = "preset";
                            billTypeList.Add(billType);
                            Create(billType);
                            //await Http.PostAsJsonAsync<BillType>("api/BillTypes/createOneBillTypeAsync", billType);
                        }                    
                }
                return billTypeList;
                /*
                bool a = _context.gl_billType == null;
                return _context.gl_billType != null ?
                          View(await _context.gl_billType.ToListAsync()) :
                          Problem("Entity set 'BillDBContext.gl_billType'  is null.");*/
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve a bill detail. {0}", ex.Message);
            }
            return new List<BillType>();
        }

        // GET: BillTypes/Details/5
        [Route("getBillTypeDetailsAsync")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.gl_billType == null)
            {
                return NotFound();
            }

            var billType = await _context.gl_billType.FirstOrDefaultAsync(m => m.Id == id);
            if (billType == null)
            {
                return NotFound();
            }
            return View(billType);
        }

        // GET: BillTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("createOneBillTypeAsync")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BillTypeNo,BillTypeName,IsAdd,Remark")] BillType billType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billType);
        }

        // GET: BillTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.gl_billType == null)
            {
                return NotFound();
            }

            var billType = await _context.gl_billType.FindAsync(id);
            if (billType == null)
            {
                return NotFound();
            }
            return View(billType);
        }

        // POST: BillTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("updateOneBillTypeAsync")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BillTypeNo,BillTypeName,IsAdd,Remark")] BillType billType)
        {
            if (id != billType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillTypeExists(billType.Id))
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
            return View(billType);
        }

        // GET: BillTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.gl_billType == null)
            {
                return NotFound();
            }

            var billType = await _context.gl_billType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billType == null)
            {
                return NotFound();
            }

            return View(billType);
        }

        // POST: BillTypes/Delete/5
        [Route("deleteOneBillTypeAsync")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.gl_billType == null)
            {
                return Problem("Entity set 'BillDBContext.gl_billType'  is null.");
            }
            var billType = await _context.gl_billType.FindAsync(id);
            if (billType != null)
            {
                _context.gl_billType.Remove(billType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillTypeExists(int id)
        {
          return (_context.gl_billType?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
