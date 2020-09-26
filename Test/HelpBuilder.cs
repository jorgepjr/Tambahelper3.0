using Dominio.Models;

namespace Test
{
    public class HelpBuilder
    {
        readonly string tipo = "Maquina com defeito";
        readonly string descricao = "Meu PC nao liga nem ha pau";
        readonly string setor = "DETIC";
        readonly string telefone = "99999-9999";
        public HelpBuilder() { }
        public Help AguardandoAtendimento()
        {
            var help = new Help(
               tipo: tipo,
               descricao: descricao,
               setor: setor,
               telefone: telefone);

            return help;
        }

        public Help Iniciado()
        {
            var help = AguardandoAtendimento();
            var tecnico = new Tecnico("Katiana");

            help.IniciarAtendimento(tecnico);
            return help;
        }

        public Help Finalizado()
        {
            var help = Iniciado();
            var tecnico = new Tecnico("Jorge");

            help.FinalizarAtendimento(tecnico, "Help resolvido");

            return help;
        }
    }
}