using System;
using Dominio.Enums;
using Dominio.Models;

namespace App.ViewModels
{
    public class DetalhesDoHelpViewModel
    {
        public DetalhesDoHelpViewModel(Help help)
        {
            this.TipoDeProblema = help.TipoDeProblema;
            this.Descricao = help.Descricao;
            this.Setor = help.Setor;
            this.Situacao = help.Situacao;
            this.Telefone = help.Telefone;
        }
        public int Id { get; set; }
        public string TipoDeProblema { get; set; }
        public string Descricao { get; set; }
        public string Setor { get; set; }
        public string Telefone { get; set; }
        public string Solucao { get; set; }
        public string Observacao { get; set; }
        public DateTime InicioDoAtendimento { get; set; }
        public DateTime FimDoAtendimento { get; set; }
        public Situacao Situacao { get; set; }
        public bool EstaAguardadoAtendimento { get; set; }
        public bool AssumidoPorTecnico { get; set; }
        public bool Finalizado { get; set; }
        public bool Pendente { get; set; }
        public Avaliacao AvaliacaoDoAtendimento { get; set; }
    }
}