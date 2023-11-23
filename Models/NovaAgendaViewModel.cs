using InkStudio.Entities;

namespace InkStudio.Models
{
    public class NovaAgendaViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int TatuadorId { get; set; }
        public DateOnly Dt_inicio { get; set; }
        public DateOnly Dt_termino { get; set; }
        public float Preço { get; set; }
        public Pagamento Pagamento { get; set; }

        // Propriedades adicionais para seleção de chaves estrangeiras
        public IEnumerable<Cliente>? Clientes { get; set; }
        public IEnumerable<Tatuador>? Tatuadores { get; set; }
    }
}