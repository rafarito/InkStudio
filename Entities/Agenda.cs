using InkStudio.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace InkStudio.Entities
{
    public enum Pagamento
    {
        pago,
        pendente
    }
    public class Agenda
    {
        public int Id { get; set; }
        public virtual required Cliente Cliente { get; set; }
        public virtual required Tatuador Tatuador { get; set; }
        public DateOnly Dt_inicio { get; set; }
        public DateOnly Dt_termino { get; set; }
        public float Preço { get; set; }
        public Pagamento Pagamento { get; set; }

        // public static Cliente AchaCLiente(String query)
        // {
        //     var db = new AppDbContext();
        //     return db.Clientes.Where(c => c.Nome == query).First();
        // }
        // public static Tatuador AchaTatuador(String query)
        // {
        //     private readonly AppDbContext _context;
        //     return _context.Tatuadores.Where(c => c.Nome == query).First();
        // }

        // public void Salvar()
        // {
        //     var db = new AppDbContext();
        //     db.Agendas.Add(this);
        //     db.SaveChanges();
        // }

    }
}