using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.Data
{
    public class Contexto : DbContext
    {
        private readonly string conection;

        protected Contexto() { }
        public Contexto(IConfiguration configuration)
        {
            this.conection = configuration["conection"];
        }
        public Contexto(DbContextOptions<Contexto> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(conection);
        }

        public DbSet<Help> Help { get; set; }
        public DbSet<Tecnico> Tecnico { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}