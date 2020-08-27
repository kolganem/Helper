using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDiplom.Data;
using MyDiplom.Models;

namespace MyDiplom.Controllers
{
    public class FailuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FailuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Failures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Failures.Include(f => f.Device);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Failures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var failure = await _context.Failures
                .Include(f => f.Device)
                .FirstOrDefaultAsync(m => m.FailureId == id);
            if (failure == null)
            {
                return NotFound();
            }

            return View(failure);
        }

        // GET: Failures/Create
        public IActionResult Create(int? id)
        {
            ViewData["DeviceId"] = id;
            return View();
        }

        // POST: Failures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FailureId,DateFailure,Reason,Consequence,DeviceId")] Failure failure, int? id)
        {
            ViewData["DeviceId"] = id;
            if (ModelState.IsValid)
            {
                _context.Add(failure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(failure);
        }

        // GET: Failures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var failure = await _context.Failures.FindAsync(id);
            if (failure == null)
            {
                return NotFound();
            }
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Id", failure.DeviceId);
            ViewData["myDate"] = failure.DateFailure;
            return View(failure);
        }

        // POST: Failures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FailureId,DateFailure,Reason,Consequence,DeviceId")] Failure failure)
        {
            if (id != failure.FailureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(failure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FailureExists(failure.FailureId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/Failures/ForAdminFailure");
            }
            ViewData["DeviceId"] = new SelectList(_context.Devices, "Id", "Id", failure.DeviceId);

            return Redirect("~/Failures/ForAdminFailure");
        }

        // GET: Failures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var failure = await _context.Failures
                .Include(f => f.Device)
                .FirstOrDefaultAsync(m => m.FailureId == id);
            if (failure == null)
            {
                return NotFound();
            }

            return View(failure);
        }

        // POST: Failures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var failure = await _context.Failures.FindAsync(id);
            _context.Failures.Remove(failure);
            await _context.SaveChangesAsync();
            return Redirect("~/Failures/ForAdminFailure");
        }

        private bool FailureExists(int id)
        {
            return _context.Failures.Any(e => e.FailureId == id);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ForAdminFailure()
        {
            var temp = _context.Failures.ToList();
            return View(temp);
        }


    }
}