
using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Models;

namespace App.Data
{
    public interface IHelpDao
    {
        Task<IEnumerable<Help>> Buscar();
        Task Criar(Help help); 
        Task<Help> BuscarPor(int id);
        Task Atender(int helpId, Tecnico tecnico);
    }
}