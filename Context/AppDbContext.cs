using InkStudio.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InkStudio.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Tatuador> Tatuadores {get; set;}
        public DbSet<Cliente> Clientes {get; set;}
        public DbSet<Agenda> Agendas {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tatuador>().HasData(
                new Tatuador{
                    TatuadorId = 1,
                    Nome= "rafoso",
                    Estilo = "neoclassico",
                    Admissão = DateOnly.FromDateTime(DateTime.Now),
                    Telefone = "71981321267",
                    Email="rafaritogames@gmail.com",
                }
            );
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente{
                    ClienteId = 1,
                    Nome = "Alê",
                    Dt_nasci = DateOnly.FromDateTime(DateTime.Now),
                    Telefone = "9999999999",
                    Email = "alê@email.com"
                }
            );
            // modelBuilder.Entity<Agenda>().HasData(
            //     new Agenda{
            //         Id = 1,
            //         Cliente = Clientes.Where(c => c.Nome == "alê").First(),
            //         Tatuador = Tatuadores.Where(t => t.Nome == "rafoso").First(),
            //         Dt_inicio = DateOnly.FromDateTime(DateTime.Now),
            //         Dt_termino = DateOnly.FromDateTime(DateTime.Now),
            //         Preço = 2.50F,
            //         Pagamento = Pagamento.pendente
            //     }
            // );
        }

    }
}