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
        private readonly IHelpDao dao;

        public HelpsController(IHelpDao dao)
        {
            this.dao = dao;
        }
        public async Task<IActionResult> Index()
        {
            var helps = await dao.Buscar();
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

                await dao.Criar(help);
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

            var help = await dao.BuscarPor(id);

            var helpViewModel = new HelpViewModel(help);
            return View(helpViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Atender(int id, int tecnicoId)
        {
            await dao.Atender(id);
            return RedirectToAction(nameof(Detalhes), new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar(HelpViewModel helpViewModel)
        {
            var help = await db.Help.Include(x => x.Tecnico).SingleOrDefaultAsync(x => x.Id == helpViewModel.Id);

            help.FinalizarAtendimento(help.Tecnico, helpViewModel.Solucao);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Detalhes), new { help.Id });
        }
    }
}