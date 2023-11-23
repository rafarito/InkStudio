using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InkStudio.Context;
using InkStudio.Entities;
using Microsoft.AspNetCore.Authorization;

namespace InkStudio.Controllers
{
    public class TatuadoresController : Controller
    {
        private readonly AppDbContext _context;

        public TatuadoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tatuadores
        public async Task<IActionResult> Index()
        {
              return _context.Tatuadores != null ? 
                          View(await _context.Tatuadores.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Tatuadores'  is null.");
        }

        // GET: Tatuadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tatuadores == null)
            {
                return NotFound();
            }

            var tatuador = await _context.Tatuadores
                .FirstOrDefaultAsync(m => m.TatuadorId == id);
            if (tatuador == null)
            {
                return NotFound();
            }

            return View(tatuador);
        }

        // GET: Tatuadores/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tatuadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TatuadorId,Nome,Email,Estilo,Telefone,Admissão")] Tatuador tatuador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tatuador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tatuador);
        }

        // GET: Tatuadores/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tatuadores == null)
            {
                return NotFound();
            }

            var tatuador = await _context.Tatuadores.FindAsync(id);
            if (tatuador == null)
            {
                return NotFound();
            }
            return View(tatuador);
        }

        // POST: Tatuadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TatuadorId,Nome,Email,Estilo,Telefone,Admissão")] Tatuador tatuador)
        {
            if (id != tatuador.TatuadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tatuador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tatuadorExists(tatuador.TatuadorId))
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
            return View(tatuador);
        }

        // GET: Tatuadores/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tatuadores == null)
            {
                return NotFound();
            }

            var tatuador = await _context.Tatuadores
                .FirstOrDefaultAsync(m => m.TatuadorId == id);
            if (tatuador == null)
            {
                return NotFound();
            }

            return View(tatuador);
        }

        // POST: Tatuadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tatuadores == null)
            {
                return Problem("Entity set 'AppDbContext.Tatuadores'  is null.");
            }
            var tatuador = await _context.Tatuadores.FindAsync(id);
            if (tatuador != null)
            {
                _context.Tatuadores.Remove(tatuador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tatuadorExists(int id)
        {
          return (_context.Tatuadores?.Any(e => e.TatuadorId == id)).GetValueOrDefault();
        }
    }
}