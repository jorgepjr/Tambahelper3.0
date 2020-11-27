using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class HelpDao : IHelpDao
    {
        private readonly Contexto db;

        public HelpDao(Contexto db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Help>> Buscar()
        {
            var helps = await db.Help
           .Include(x => x.Tecnico)
           .ToListAsync();
            return helps;
        }

        public async Task Criar(Help help)
        {
            await db.AddAsync(help);
            await db.SaveChangesAsync();
        }

        public async Task<Help> BuscarPor(int id)
        {
            var help = await db.Help.FindAsync(id);
            return help;
        }
        public async Task Atender(int helpId, Tecnico tecnico)
        {
            var help = await BuscarPor(helpId);
            help.IniciarAtendimento(tecnico);
            await db.SaveChangesAsync();
        }
    }
}