using App.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Data;
using TiaIdentity;

namespace App.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly Contexto db;
        private readonly Autenticador autenticador;

        public AutenticacaoController(Contexto db, Autenticador autenticador)
        {
            this.db = db;
            this.autenticador = autenticador;
        }
        public IActionResult Acessar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Acessar(LoginViewModel viewModel)
        {
            var usuario = await db.Usuario.FirstOrDefaultAsync(x => x.Nome == viewModel.Nome);

            bool autenticacaoValida = usuario == null || !usuario.SenhaCorreta(viewModel.Senha);

            if (!autenticacaoValida)
                ModelState.AddModelError("", "Usuário ou Senha incorretos!");
            if (ModelState.IsValid)
                await autenticador.LoginAsync(usuario, true);

            return RedirectToAction("Index", "Helps");
        }
    }
}

