using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Data;
using Dominio.Models;

namespace App.Controllers
{
    public class TecnicosController : Controller
    {
        private readonly Contexto db;

        public TecnicosController(Contexto db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var tecnicos = await db.Tecnico.ToListAsync();
            return View(tecnicos);
        }
        public IActionResult Criar()
        {
            var tecnico = new Tecnico();
            return View(tecnico);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Tecnico tecnico)
        {
            if (ModelState.IsValid)
                await db.AddAsync(tecnico);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            var busca = db.Tecnico.Find(id);
            var tecnico = new Tecnico { Nome = busca.Nome };
            return View(tecnico);
        }

        [HttpPost]
        public IActionResult Editar(Tecnico tecnico)
        {

            if (ModelState.IsValid)
            {
                db.Update(tecnico);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(tecnico);
        }
    }
}