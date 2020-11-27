using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class TecnicoDao : ITecnicoDao
    {
        private readonly Contexto db;
        private readonly IHelpDao helpDao;

        public TecnicoDao(Contexto db, IHelpDao helpDao)
        {
            this.db = db;
            this.helpDao = helpDao;
        }

        public async Task<IEnumerable<Tecnico>> Buscar()
        {
            return await db.Tecnico.ToListAsync();
        }

        public async Task<Tecnico> BuscarPor(int id)
        {
            return await db.Tecnico.FindAsync(id);
        }

        public async Task Atender(int helpId, int tecnicoId)
        {
            var tecnico = await BuscarPor(tecnicoId);
            await helpDao.Atender(helpId, tecnico);
        }
        public async Task Finalizar(int helpId, int tecnicoId, string solucao)
        {
            var help = await db.Help.Include(x => x.Tecnico).SingleOrDefaultAsync(x => x.Id == helpId);
            help.FinalizarAtendimento(help.Tecnico, solucao);
            await db.SaveChangesAsync();
        }
    }
}