using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sicoInacap.Models;

namespace sicoInacap.Controllers
{
    public class SimpatizantesController : Controller
    {
        private readonly SicoDbContext _context;

        public SimpatizantesController(SicoDbContext context)
        {
            _context = context;
        }

        // GET: Simpatizantes
        public async Task<IActionResult> Index()
        {
            var sicoDbContext = _context.Simpatizante.Include(s => s.UsernameNavigation);
            return View(await sicoDbContext.ToListAsync());
        }

        // GET: Simpatizantes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simpatizante = await _context.Simpatizante
                .Include(s => s.UsernameNavigation)
                .SingleOrDefaultAsync(m => m.Username == id);
            if (simpatizante == null)
            {
                return NotFound();
            }

            return View(simpatizante);
        }

        // GET: Simpatizantes/Create
        public IActionResult Create()
        {
            ViewData["Username"] = new SelectList(_context.Usuario, "Username", "Username");
            return View();
        }

        // POST: Simpatizantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Nombres,Apellidos,Email,FechaNacimiento,Genero")] Simpatizante simpatizante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(simpatizante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Username"] = new SelectList(_context.Usuario, "Username", "Username", simpatizante.Username);
            return View(simpatizante);
        }

        // GET: Simpatizantes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simpatizante = await _context.Simpatizante.SingleOrDefaultAsync(m => m.Username == id);
            if (simpatizante == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Usuario, "Username", "Username", simpatizante.Username);
            return View(simpatizante);
        }

        // POST: Simpatizantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Username,Nombres,Apellidos,Email,FechaNacimiento,Genero")] Simpatizante simpatizante)
        {
            if (id != simpatizante.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(simpatizante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SimpatizanteExists(simpatizante.Username))
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
            ViewData["Username"] = new SelectList(_context.Usuario, "Username", "Username", simpatizante.Username);
            return View(simpatizante);
        }

        // GET: Simpatizantes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var simpatizante = await _context.Simpatizante
                .Include(s => s.UsernameNavigation)
                .SingleOrDefaultAsync(m => m.Username == id);
            if (simpatizante == null)
            {
                return NotFound();
            }

            return View(simpatizante);
        }

        // POST: Simpatizantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var simpatizante = await _context.Simpatizante.SingleOrDefaultAsync(m => m.Username == id);
            _context.Simpatizante.Remove(simpatizante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SimpatizanteExists(string id)
        {
            return _context.Simpatizante.Any(e => e.Username == id);
        }
    }
}
