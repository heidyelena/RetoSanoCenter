using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoSanoCenter.Data;
using ProyectoSanoCenter.Models;

namespace ProyectoSanoCenter.Controllers
{
    public class RetosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RetosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Retos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reto.Include(r => r.Entrenador).Include(x => x.Ejercicios);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> MisRetos()
        {
            var applicationDbContext = _context.Reto.Include(r => r.Entrenador).Include(x => x.Ejercicios);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Retos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reto = await _context.Reto
                .Include(r => r.Entrenador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reto == null)
            {
                return NotFound();
            }

            return View(reto);
        }

        // GET: Retos/Create
        public async Task<IActionResult> Create()
        {

            EjercicioRetoVM ejercicioRetoVM = new EjercicioRetoVM
            {
                Ejercicios = await _context.Ejercicio.ToListAsync(),
                Reto = new Reto()

            };
            ViewData["EntrenadorId"] = new SelectList(_context.Entrenador, "Id", "Id");
            ViewData["EjercicioId"] = new SelectList(_context.Ejercicio, "Id", "Id");
            return View(ejercicioRetoVM);
        }

        // POST: Retos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imagen,NombreReto,Dificultad,Series,Repeticiones,FechaLimite,EntrenadorId")] Reto reto, string[] ejercicios)
        {

            if (ModelState.IsValid)
            {                
           
                List<Ejercicio> ejerciciosLista = new List<Ejercicio>();

                for (int i = 0; i < ejercicios.Length; i++)
                {
                    ejerciciosLista.Add(await _context.Ejercicio.FindAsync(Convert.ToInt32(ejercicios[i])));
                }
               
                reto.Ejercicios = ejerciciosLista;

                _context.Add(reto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntrenadorId"] = new SelectList(_context.Entrenador, "Id", "Id", reto.EntrenadorId);
            return View(reto);
        }

        // GET: Retos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reto = await _context.Reto.FindAsync(id);
            if (reto == null)
            {
                return NotFound();
            }
            ViewData["EntrenadorId"] = new SelectList(_context.Entrenador, "Id", "Id", reto.EntrenadorId);
            return View(reto);
        }

        // POST: Retos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imagen,NombreReto,Dificultad,Series,Repeticiones,FechaLimite,EntrenadorId")] Reto reto)
        {
            if (id != reto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetoExists(reto.Id))
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
            ViewData["EntrenadorId"] = new SelectList(_context.Entrenador, "Id", "Id", reto.EntrenadorId);
            return View(reto);
        }

        // GET: Retos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reto = await _context.Reto
                .Include(r => r.Entrenador)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reto == null)
            {
                return NotFound();
            }

            return View(reto);
        }

        // POST: Retos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reto = await _context.Reto.FindAsync(id);
            _context.Reto.Remove(reto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetoExists(int id)
        {
            return _context.Reto.Any(e => e.Id == id);
        }
    }
}
