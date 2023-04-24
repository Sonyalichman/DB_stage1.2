using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DB_stage1._2.Models;

namespace DB_stage1._2.Controllers
{
    public class ExhibitionsController : Controller
    {
        private readonly BdgalleryContext _context;

        public ExhibitionsController(BdgalleryContext context)
        {
            _context = context;
        }

        // GET: Exhibitions
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Genres", "Index");
            ViewBag.GenreId = id;
            ViewBag.GenreName = name;
            var exhibitionsByGenre = _context.Exhibitions.Where(e=>e.GenreId == id).Include(e => e.Genre);
            return View(await exhibitionsByGenre.ToListAsync());
        }

        // GET: Exhibitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exhibitions == null)
            {
                return NotFound();
            }

            var exhibition = await _context.Exhibitions
                .Include(e => e.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exhibition == null)
            {
                return NotFound();
            }

            return View(exhibition);
        }

        // GET: Exhibitions/Create
        public IActionResult Create(int genreId)
        {
            //ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            ViewBag.GenreId = genreId;
            ViewBag.GenreName = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault().Name;
            return View();
        }

        // POST: Exhibitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int genreId, [Bind("Id,Name,GenreId,StartDate,EndDate")] Exhibition exhibition)
        {
            exhibition.GenreId = genreId;
            if (ModelState.IsValid)
            {
                _context.Add(exhibition);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Exhibitions", new { id = genreId, name = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault().Name });
            //    return RedirectToAction(nameof(Index));
            }
         //   ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", exhibition.GenreId);
           // return View(exhibition);
           return RedirectToAction("Index", "Exhibitions", new { id = genreId, name = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault().Name });
        }

        // GET: Exhibitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exhibitions == null)
            {
                return NotFound();
            }

            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", exhibition.GenreId);
            return View(exhibition);
        }

        // POST: Exhibitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GenreId,StartDate,EndDate")] Exhibition exhibition)
        {
            if (id != exhibition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exhibition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExhibitionExists(exhibition.Id))
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
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", exhibition.GenreId);
            return View(exhibition);
        }

        // GET: Exhibitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exhibitions == null)
            {
                return NotFound();
            }

            var exhibition = await _context.Exhibitions
                .Include(e => e.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exhibition == null)
            {
                return NotFound();
            }

            return View(exhibition);
        }

        // POST: Exhibitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exhibitions == null)
            {
                return Problem("Entity set 'BdgalleryContext.Exhibitions'  is null.");
            }
            var exhibition = await _context.Exhibitions.FindAsync(id);
            if (exhibition != null)
            {
                _context.Exhibitions.Remove(exhibition);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExhibitionExists(int id)
        {
          return (_context.Exhibitions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
