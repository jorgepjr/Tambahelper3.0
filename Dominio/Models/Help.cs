using System;
using Dominio.Enums;

namespace Dominio.Models
{
    public class Help
    {
        protected Help(){}
        public Help(string tipo, string descricao, string setor, string telefone)
        {
            Tipo = tipo;
            Descricao = descricao;
            Setor = setor;
            Telefone = telefone;
            Situacao = Situacao.AguardandoAtendimento;
        }

        public int Id { get; private set; }
        public string Tipo { get; private set; }
        public string Descricao { get; private set; }
        public string Setor { get; private set; }
        public string Telefone { get; private set; }
        public Tecnico Tecnico { get; private set; }
        public DateTime InicioDoAtendimento { get; private set; }
        public DateTime FimDoAtendimento { get; private set; }
        public Situacao Situacao { get; private set; }
        public DateTime DataDeRegistro { get; private set; } = DateTime.Now;
        public string Solucao { get; private set; }
        public void IniciarAtendimento(Tecnico tecnico)
        {
            if (!PodeAtender)
            {
                throw new InvalidOperationException("Este help não esta Aguardando Atendimento!");
            }
            this.Tecnico = tecnico;
            this.InicioDoAtendimento = DateTime.Now;
            this.Situacao = Situacao.EmAtendimento;
        }
        public void FinalizarAtendimento(Tecnico tecnico, string solucao)
        {
            if (!AssumidoPorTecnico)
            {
                throw new InvalidOperationException("O atendimento deste help ainda não foi iniciado!");
            }
            this.Tecnico = tecnico;
            this.Solucao = solucao;
            this.FimDoAtendimento = DateTime.Now;
            this.Situacao = Situacao.Finalizado;
        }
        public bool AssumidoPorTecnico => this.Situacao == Situacao.EmAtendimento;
        public bool PodeAtender => this.Situacao == Situacao.AguardandoAtendimento;
    }
}