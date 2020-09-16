using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Enums
{
    public enum Situacao : int
    {
        AguardandoAtendimento = 0,

        [Description("Em atendimento")]
        EmAtendimento = 1,

        [Display(Name = "Resolvido por")]
        Finalizado = 2,
        Pendente = 3
    }
}