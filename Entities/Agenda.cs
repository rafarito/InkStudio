using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Required]
        [ForeignKey("Clientes")]
        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }
        [Required]
        [ForeignKey("Tatuadores")]
        public int TatuadorId { get; set; }
        public virtual Tatuador? Tatuador { get; set; }
        public DateOnly Dt_inicio { get; set; }
        public DateOnly Dt_termino { get; set; }
        public float Preço { get; set; }
        public Pagamento Pagamento { get; set; }
    }
}