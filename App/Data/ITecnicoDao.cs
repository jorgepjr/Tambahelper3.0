using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Models;

namespace App.Data
{
    public interface ITecnicoDao
    {
        Task<IEnumerable<Tecnico>> Buscar();
        Task<Tecnico> BuscarPor(int id);
        Task Atender(int helpId, int tecnicoId);
        Task Finalizar(int helpId, int tecnicoId, string solucao);

    }
}