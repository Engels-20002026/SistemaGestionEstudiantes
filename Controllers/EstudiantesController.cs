
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEstudiantes.Data;
using SistemaGestionEstudiantes.Models;

namespace SistemaGestionEstudiantes.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly AplicacionDbContext _context;

        public EstudiantesController(AplicacionDbContext context)
        {
            _context = context;
        }

        // CONSULTAR ESTUDIANTES
        public async Task<IActionResult> Index(string buscar)
        {
            var estudiantes = _context.Estudiantes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                estudiantes = estudiantes.Where(e =>
                    e.Nombre.Contains(buscar) ||
                    e.Apellido.Contains(buscar) ||
                    e.Matricula.Contains(buscar));
            }

            return View(await estudiantes.ToListAsync());
        }

        // MOSTRAR FORMULARIO DE REGISTRO
        public IActionResult Crear()
        {
            return View();
        }

        // GUARDAR NUEVO ESTUDIANTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Estudiantes.Add(estudiante);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(estudiante);
        }

        // MOSTRAR FORMULARIO DE EDICIÓN
        public async Task<IActionResult> Editar(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // ACTUALIZAR ESTUDIANTE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Estudiantes.Update(estudiante);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(estudiante);
        }

        // MOSTRAR CONFIRMACIÓN DE ELIMINACIÓN
        public async Task<IActionResult> Eliminar(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // ELIMINAR ESTUDIANTE
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);

            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
