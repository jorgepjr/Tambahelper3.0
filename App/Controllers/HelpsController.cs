using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.ViewModels;
using App.Data;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var tecnicos = await db.Tecnico.ToListAsync();

            ViewBag.Tecnicos = new SelectList(tecnicos, "Id", "Nome");

            var help = await db.Help.FindAsync(id);
            var helpViewModel = new HelpViewModel(help);
            return View(helpViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Atender(int id, int tecnicoId)
        {
            var help = await db.Help.FindAsync(id);
            var tecnicoSelecionado = await db.Tecnico.FindAsync(tecnicoId);

            help.IniciarAtendimento(tecnicoSelecionado);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar(HelpViewModel helpViewModel)
        {
            var help = await db.Help.FindAsync(helpViewModel.Id);

            var tecnico = await db.Tecnico.FindAsync(helpViewModel.Tecnico.Id);

            help.FinalizarAtendimento(tecnico, helpViewModel.Solucao);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Detalhes), new { help.Id });
        }
    }
}