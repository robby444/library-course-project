using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    [Authorize]
    public class FictionalBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FictionalBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FictionalBooks
        public async Task<IActionResult> Index()
        {
              return _context.FictionalBooks != null ? 
                          View(await _context.FictionalBooks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.FictionalBooks'  is null.");
        }

        // GET: FictionalBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FictionalBooks == null)
            {
                return NotFound();
            }

            var fictionalBook = await _context.FictionalBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fictionalBook == null)
            {
                return NotFound();
            }

            return View(fictionalBook);
        }

        // GET: FictionalBooks/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FictionalBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Genre,TargetAudience,Id,Title,Author,Pages,Description")] FictionalBook fictionalBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fictionalBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fictionalBook);
        }

        // GET: FictionalBooks/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FictionalBooks == null)
            {
                return NotFound();
            }

            var fictionalBook = await _context.FictionalBooks.FindAsync(id);
            if (fictionalBook == null)
            {
                return NotFound();
            }
            return View(fictionalBook);
        }

        // POST: FictionalBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Genre,TargetAudience,Id,Title,Author,Pages,Description")] FictionalBook fictionalBook)
        {
            if (id != fictionalBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fictionalBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FictionalBookExists(fictionalBook.Id))
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
            return View(fictionalBook);
        }

        // GET: FictionalBooks/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FictionalBooks == null)
            {
                return NotFound();
            }

            var fictionalBook = await _context.FictionalBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fictionalBook == null)
            {
                return NotFound();
            }

            return View(fictionalBook);
        }

        // POST: FictionalBooks/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FictionalBooks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.FictionalBooks'  is null.");
            }
            var fictionalBook = await _context.FictionalBooks.FindAsync(id);
            if (fictionalBook != null)
            {
                _context.FictionalBooks.Remove(fictionalBook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FictionalBookExists(int id)
        {
          return (_context.FictionalBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
