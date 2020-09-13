using Xunit;
using System;
using Dominio.Enums;
using Dominio.Models;

namespace Test
{
    public class HelpTests
    {
        [Fact]
        public void DeveCriarHelpComSituacaoAguardandoAtendimento()
        {
            var help = new Help("Maquina com defeito", "Nao consigo ligar PC", "Gerencia", "58888555956");

            Assert.True(help.EstaAguardandoAtendimento);
        }

        [Fact]
        public void DeveRegistrarInicioDoAtendimentoComADataDeHojeAoIniciarAtendimento()
        {
            var hoje = DateTime.Now;

            var helpIniciado = new HelpBuilder().Iniciado();

            Assert.Equal(helpIniciado.InicioDoAtendimento.Date, hoje.Date);
        }

        [Fact]
        public void DeveMudarSituacaoParaAssumidoPorTecnicoAoIniciarAtendimento()
        {
            var helpIniciado = new HelpBuilder().Iniciado();

            Assert.Equal(Situacao.AssumidoPorTecnico, helpIniciado.Situacao);
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


