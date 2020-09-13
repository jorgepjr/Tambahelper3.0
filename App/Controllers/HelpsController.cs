using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.ViewModels;
using App.Data;
using Dominio.Models;

namespace App.Controllers
{
    public class HelpsController : Controller
    {
        private readonly Contexto db;

        public HelpsController(Contexto db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            var helps = await db.Help
            .Include(x => x.Tecnico)
            .OrderByDescending(x => x.DataDeRegistro)
            .ToListAsync();
            return View(helps);
        }
        public IActionResult Criar()
        {
            var criarHelpViewModel = new CriarHelpViewModel();
            return View(criarHelpViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarHelpViewModel criarHelpViewModel)
        {
            if (ModelState.IsValid)
            {
                var help = new Help(criarHelpViewModel.TipoDeProblema,
                                    criarHelpViewModel.Descricao,
                                    criarHelpViewModel.Setor,
                                    criarHelpViewModel.Telefone);

                await db.AddAsync(help);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Enviado));
            }
            return View(criarHelpViewModel);
        }

        public IActionResult Enviado()
        {

            return View();
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var help = await db.Help.FindAsync(id);
            var detalhesDoHelpViewModel = new DetalhesDoHelpViewModel(help);
            return View(detalhesDoHelpViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Atender(int id)
        {
            var help = await db.Help.FindAsync(id);

            var tecnico = await db.Tecnico.FirstOrDefaultAsync();

            if (tecnico is null)
            {
                return RedirectToAction("Criar", "Tecnicos");
            }

            help.IniciarAtendimento(tecnico);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar(int id, Help model)
        {
            var help = await db.Help.FindAsync(id);

            var tecnico = await db.Tecnico.FirstOrDefaultAsync();

            if (tecnico is null)
            {
                return RedirectToAction("Criar");
            }

            help.FinalizarAtendimento(tecnico, model.Solucao);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Detalhes), new { id });
        }
    }
}