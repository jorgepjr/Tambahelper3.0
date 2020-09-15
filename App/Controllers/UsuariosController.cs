using App.Data;
using Dominio.Models;
using App.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Contexto db;

        public UsuariosController(Contexto db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await db.Usuario.ToListAsync();

            return View(usuarios);
        }
        public async Task<IActionResult> Criar()
        {
            var tecnicos = await db.Tecnico.ToListAsync();
            ViewBag.Tecnicos = tecnicos;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(UsuarioViewModel viewModel)
        {
            var tecnico = await db.Tecnico.FindAsync(viewModel.TecnicoId);

            if (ModelState.IsValid)
            {
                var usuario = new Usuario(tecnico, viewModel.Email, viewModel.Perfil);
                await db.AddAsync(usuario);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            var usuario = await db.Usuario.FindAsync(id);
            if (usuario is null || usuario.Id != id)
            {
                return NotFound();
            }
            db.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}