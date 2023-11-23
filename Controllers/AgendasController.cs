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
using InkStudio.Models;
using System.Drawing.Printing;

namespace InkStudio.Controllers
{
    public class AgendasController : Controller
    {
        private readonly AppDbContext _context;

        public AgendasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Agendas
        public async Task<IActionResult> Index()
        {
            if(_context.Agendas != null)
            {
                var agendas = await _context.Agendas.ToListAsync();
                foreach(var item in agendas)
                {
                    item.Cliente = _context.Clientes.Where(a => a.ClienteId == item.ClienteId).First();
                    item.Tatuador = _context.Tatuadores.Where(a => a.TatuadorId == item.TatuadorId).First();
                }
                return View(agendas);
            }
              return Problem("Entity set 'AppDbContext.Agendas'  is null.");
        }

        // GET: Agendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agendas == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agenda == null)
            {
                return NotFound();
            }

            agenda.Cliente = _context.Clientes.Where(a => a.ClienteId == agenda.ClienteId).First();
            agenda.Tatuador = _context.Tatuadores.Where(a => a.TatuadorId == agenda.TatuadorId).First();

            return View(agenda);
        }

        // GET: Agendas/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var ViewModel = new NovaAgendaViewModel{
                Clientes = _context.Clientes.ToList(),
                Tatuadores = _context.Tatuadores.ToList()
            };
            return View(ViewModel);
        }

        // POST: Agendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,TatuadorId,Dt_inicio,Dt_termino,Preço,Pagamento,Clientes,Tatuadores")] NovaAgendaViewModel viewModel)
        {
            viewModel.Clientes = _context.Clientes.ToList();
            viewModel.Tatuadores = _context.Tatuadores.ToList();
            if (ModelState.IsValid)
            {
                var agenda = new Agenda
                {
                    ClienteId = viewModel.ClienteId,
                    TatuadorId = viewModel.TatuadorId,
                    Cliente = _context.Clientes.Where(c => c.ClienteId == viewModel.ClienteId).First(),
                    Tatuador = _context.Tatuadores.Where(c => c.TatuadorId == viewModel.TatuadorId).First(),
                    Dt_inicio = viewModel.Dt_inicio,
                    Dt_termino = viewModel.Dt_termino,
                    Preço = viewModel.Preço,
                    Pagamento = viewModel.Pagamento
                };
                _context.Add(agenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            foreach (var modelStateKey in ModelState.Keys)
    {
            var modelStateVal = ModelState[modelStateKey];
            foreach (var error in modelStateVal.Errors)
            {
                // Logue os erros aqui
                Console.WriteLine($"Erro no campo {modelStateKey}: {error.ErrorMessage}");
            }
    }
            return View(viewModel);
        }

        // GET: Agendas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agendas == null)
            {
                return NotFound();
            }


            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda == null)
            {
                return NotFound();
            }

            var ViewModel = new NovaAgendaViewModel{
                Id = agenda.Id,
                ClienteId = agenda.ClienteId,
                TatuadorId = agenda.TatuadorId,
                Dt_inicio = agenda.Dt_inicio,
                Dt_termino = agenda.Dt_termino,
                Preço = agenda.Preço,
                Pagamento = agenda.Pagamento,
                // Clientes = _context.Clientes.OrderBy(item => item.ClienteId != agenda.ClienteId).ThenBy(item=>item.ClienteId).ToList(),
                // Tatuadores = _context.Tatuadores.OrderBy(item => item.TatuadorId != agenda.TatuadorId).ThenBy(item=>item.TatuadorId).ToList()
                Clientes = _context.Clientes.ToList(),
                Tatuadores = _context.Tatuadores.ToList()
            };

            return View(ViewModel);
        }

        // POST: Agendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,TatuadorId,Dt_inicio,Dt_termino,Preço,Pagamento,Clientes,Tatuadores")] NovaAgendaViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var agenda = new Agenda
                {
                    Id = id,
                    ClienteId = viewModel.ClienteId,
                    TatuadorId = viewModel.TatuadorId,
                    Cliente = _context.Clientes.Where(c => c.ClienteId == viewModel.ClienteId).First(),
                    Tatuador = _context.Tatuadores.Where(c => c.TatuadorId == viewModel.TatuadorId).First(),
                    Dt_inicio = viewModel.Dt_inicio,
                    Dt_termino = viewModel.Dt_termino,
                    Preço = viewModel.Preço,
                    Pagamento = viewModel.Pagamento
                };
                try
                {
                    _context.Update(agenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!agendaExists(agenda.Id))
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
            return View(viewModel);
        }

        // GET: Agendas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agendas == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agenda == null)
            {
                return NotFound();
            }

            agenda.Cliente = _context.Clientes.Where(a => a.ClienteId == agenda.ClienteId).First();
            agenda.Tatuador = _context.Tatuadores.Where(a => a.TatuadorId == agenda.TatuadorId).First();

            return View(agenda);
        }

        // POST: Agendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agendas == null)
            {
                return Problem("Entity set 'AppDbContext.Agendas'  is null.");
            }
            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda != null)
            {
                _context.Agendas.Remove(agenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool agendaExists(int id)
        {
          return (_context.Agendas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}