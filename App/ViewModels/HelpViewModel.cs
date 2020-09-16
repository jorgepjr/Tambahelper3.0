using System;
using Dominio.Enums;
using Dominio.Models;

namespace App.ViewModels
{
    public class HelpViewModel
    {
        public HelpViewModel(){}
        public HelpViewModel(Help help)
        {
            this.Id = help.Id;
            this.TipoDeProblema = help.TipoDeProblema;
            this.Descricao = help.Descricao;
            this.Setor = help.Setor;
            this.Situacao = help.Situacao;
            this.Telefone = help.Telefone;
            this.Solucao = help.Solucao;
            this.InicioDoAtendimento = help.InicioDoAtendimento;
            this.FimDoAtendimento = help.FimDoAtendimento;
            this.Tecnico = help.Tecnico;
        }
        public int Id { get; set; }
        public string TipoDeProblema { get; set; }
        public string Descricao { get; set; }
        public string Setor { get; set; }
        public string Telefone { get; set; }
        public string Solucao { get; set; }
        public DateTime InicioDoAtendimento { get; set; }
        public DateTime FimDoAtendimento { get; set; }
        public Tecnico Tecnico { get; }
        public Situacao Situacao { get; set; }
    }
}