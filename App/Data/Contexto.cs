using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Help> Help { get; set; }
        public DbSet<Tecnico> Tecnico { get; set; }
    }
}