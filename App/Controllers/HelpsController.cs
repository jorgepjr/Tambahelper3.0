using App.Data;
using App.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dominio.Models;

namespace App.Controllers
{
    public class HelpsController : Controller
    {
        private readonly IHelpDao helpDao;
        private readonly ITecnicoDao tecnicoDao;

        public HelpsController(IHelpDao helpDao, ITecnicoDao tecnicoDao)
        {
            this.helpDao = helpDao;
            this.tecnicoDao = tecnicoDao;
        }
        public async Task<IActionResult> Index()
        {
            var helps = await helpDao.Buscar();
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

                await helpDao.Criar(help);
                return RedirectToAction(nameof(Enviado));
            }
            return View(criarHelpViewModel);
        }
        public async Task<IActionResult> Detalhes(int id)
        {
            var tecnicos = await tecnicoDao.Buscar();

            ViewBag.Tecnicos = new SelectList(tecnicos, "Id", "Nome");

            var help = await helpDao.BuscarPor(id);

            var helpViewModel = new HelpViewModel(help);
            return View(helpViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Atender(int helpId, int tecnicoId)
        {
            await tecnicoDao.Atender(helpId, tecnicoId);
            return RedirectToAction(nameof(Detalhes), new { id = helpId });
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar(int helpId, int tecnicoId, string solucao)
        {
            await tecnicoDao.Finalizar(helpId, tecnicoId, solucao);

            return RedirectToAction(nameof(Detalhes), new { id = helpId });
        }
        public IActionResult Enviado()
        {

            return View();
        }

    }
}