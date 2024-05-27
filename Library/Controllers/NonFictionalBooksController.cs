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
    public class NonFictionalBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NonFictionalBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NonFictionalBooks
        public async Task<IActionResult> Index()
        {
              return _context.NonFictionalBooks != null ? 
                          View(await _context.NonFictionalBooks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.NonFictionalBooks'  is null.");
        }

        // GET: NonFictionalBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NonFictionalBooks == null)
            {
                return NotFound();
            }

            var nonFictionalBook = await _context.NonFictionalBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nonFictionalBook == null)
            {
                return NotFound();
            }

            return View(nonFictionalBook);
        }

        // GET: NonFictionalBooks/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: NonFictionalBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subject,Edition,Id,Title,Author,Pages,Description")] NonFictionalBook nonFictionalBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nonFictionalBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nonFictionalBook);
        }

        // GET: NonFictionalBooks/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NonFictionalBooks == null)
            {
                return NotFound();
            }

            var nonFictionalBook = await _context.NonFictionalBooks.FindAsync(id);
            if (nonFictionalBook == null)
            {
                return NotFound();
            }
            return View(nonFictionalBook);
        }

        // POST: NonFictionalBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Subject,Edition,Id,Title,Author,Pages,Description")] NonFictionalBook nonFictionalBook)
        {
            if (id != nonFictionalBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nonFictionalBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NonFictionalBookExists(nonFictionalBook.Id))
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
            return View(nonFictionalBook);
        }

        // GET: NonFictionalBooks/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NonFictionalBooks == null)
            {
                return NotFound();
            }

            var nonFictionalBook = await _context.NonFictionalBooks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nonFictionalBook == null)
            {
                return NotFound();
            }

            return View(nonFictionalBook);
        }

        // POST: NonFictionalBooks/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NonFictionalBooks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.NonFictionalBooks'  is null.");
            }
            var nonFictionalBook = await _context.NonFictionalBooks.FindAsync(id);
            if (nonFictionalBook != null)
            {
                _context.NonFictionalBooks.Remove(nonFictionalBook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NonFictionalBookExists(int id)
        {
          return (_context.NonFictionalBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
