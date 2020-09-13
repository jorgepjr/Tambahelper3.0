using System.ComponentModel.DataAnnotations;

namespace App.ViewModels
{
    public class CriarHelpViewModel
    {
        [Required(ErrorMessage = "Informe o que aconteceu ?")]
        public string TipoDeProblema { get;  set; }

        [Required(ErrorMessage = "É preciso descrever o que houve")]
        public string Descricao { get;  set; }

        [Required(ErrorMessage = "Precisamos saber o setor para atende-lo o mais rápido possível")]
        public string Setor { get;  set; }

        [Required(ErrorMessage = "Para facilitar ainda mais o atendimento, precisamos do telefone para contato")]
        public string Telefone { get;  set; }
    }
}