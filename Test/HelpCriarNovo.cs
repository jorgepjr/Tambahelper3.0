using System;
using Dominio.Enums;
using Dominio.Models;
using Xunit;

namespace Test
{
    public class HelpCriarNovo
    {
        [Fact]
        public void ComDataDeRegistroDeHoje()
        {
            //Given
            DateTime hoje = DateTime.Now.Date;

            //When
            Help help = new HelpBuilder().AguardandoAtendimento();

            //Then
            Assert.Equal(hoje, help.DataDeRegistro.Date);
        }

        [Fact]
        public void ComSituacaoAguardandoAtendimento()
        {
            //Given
            Situacao aguardandoAtendimento = Situacao.AguardandoAtendimento;

            //When
            Help help = new HelpBuilder().AguardandoAtendimento();

            //Then
            Assert.Equal(aguardandoAtendimento, help.Situacao);
        }
    }
}