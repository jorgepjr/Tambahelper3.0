using System;
using Dominio.Enums;
using Dominio.Models;
using Xunit;

namespace Test
{
    public class IniciarAtendimento
    {
        [Fact]
        public void DeveMudarSituacaoParaEmAtendimentoAoIniciarAtendimento()
        {
            //Given
            var help = new Help("Maquina nao liga", "Meu PC nao quer ligar", "Financeiro", "999999999");
            var tecnico = new Tecnico("Raimundão");

            //When
            help.IniciarAtendimento(tecnico);

            //Then
            Assert.Equal(Situacao.EmAtendimento, help.Situacao);
        }

        [Fact]
        public void DeveRegistrarComADataDeHojeOInicioDoAtendimento()
        {
            //Given
            var hoje = DateTime.Now.Date;
            var help = new Help("Maquina nao liga", "Meu PC nao quer ligar", "Financeiro", "999999999");
            var tecnico = new Tecnico("Raimundão");

            //When
            help.IniciarAtendimento(tecnico);

            //Then
            Assert.Equal(hoje, help.InicioDoAtendimento.Date);
        }

         [Fact]
        public void DeveCriarHelpComSituacaoAguardandoAtendimento()
        {
            Situacao situacaoEsperada = Situacao.AguardandoAtendimento;

            var help = new Help("Maquina com defeito", "Nao consigo ligar PC", "Gerencia", "58888555956");

            Assert.Equal(situacaoEsperada, help.Situacao);
        }

        [Fact]
        public void DeveRegistrarInicioDoAtendimentoComADataDeHojeAoIniciarAtendimento()
        {
            var hoje = DateTime.Now;

            var helpIniciado = new HelpBuilder().Iniciado();

            Assert.Equal(helpIniciado.InicioDoAtendimento.Date, hoje.Date);
        }

        [Fact]
        public void DeveRegistrarFimDoAtendimentoComADataDeHojeAoFinalizarAtendimento()
        {
            var hoje = DateTime.Now;

            var helpIniciado = new HelpBuilder().Iniciado();

            string solucao = "BO resolvido";
            helpIniciado.FinalizarAtendimento(helpIniciado.Tecnico, solucao);

            Assert.Equal(helpIniciado.FimDoAtendimento.Date, hoje.Date);
        }

        [Fact]
        public void DeveRegistrarSolucaoAoFinalizarAtendimento()
        {
            var helpIniciado = new HelpBuilder().Iniciado();

            string solucao = "Placa de rede trocada, problema resolvido";
            helpIniciado.FinalizarAtendimento(helpIniciado.Tecnico, solucao);

            Assert.Equal(helpIniciado.Solucao, solucao);
        }

        [Fact]
        public void DeveMudarSituacaoParaFinalizadoAoFinalizarAtendimento()
        {
            var helpIniciado = new HelpBuilder().Iniciado();

            string solucao = "Tudo certo nada resolvido";
            helpIniciado.FinalizarAtendimento(helpIniciado.Tecnico, solucao);

            Assert.Equal(Situacao.Finalizado, helpIniciado.Situacao);
        }
    }
}