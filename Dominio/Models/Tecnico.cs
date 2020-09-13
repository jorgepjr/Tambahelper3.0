namespace Dominio.Models
{
    public class Tecnico
    {
        public Tecnico() { }
        public Tecnico(string nome)
        {
            this.Nome = nome;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}