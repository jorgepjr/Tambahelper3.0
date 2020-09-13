using Dominio.Enums;
using Dominio.Models;
using Xunit;

namespace Test
{
    public class HelpIniciarAtendimento
    {
        [Fact]
        public void DeveMudarSituacaoParaAssumidoPorTecnico()
        {
            //Given
            Tecnico tecnico = new Tecnico { Id = 1, Nome = "José" };
            Situacao assumidoPorTecnico = Situacao.AssumidoPorTecnico;
            Help help = new HelpBuilder().AguardandoAtendimento();


            //When
            help.IniciarAtendimento(tecnico);

            //Then
            Assert.Equal(assumidoPorTecnico, help.Situacao);
        }

        [Fact]
        public void SomenteSeEstiverAguardandoAtendimento()
        {
            //Given
            Situacao aguardandoAtendimento = Situacao.AguardandoAtendimento;
            Help help = new HelpBuilder().AguardandoAtendimento();
            Tecnico tecnico = new Tecnico { Id = 1, Nome = "José" };
            Assert.Equal(aguardandoAtendimento, help.Situacao);

            Situacao assumidoPorTecnico = Situacao.AssumidoPorTecnico;

            //When
            help.IniciarAtendimento(tecnico);

            //Then
            Assert.Equal(assumidoPorTecnico, help.Situacao);
        }
    }
}