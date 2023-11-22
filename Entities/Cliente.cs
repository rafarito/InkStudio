using System.ComponentModel.DataAnnotations;

namespace InkStudio.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public String? Nome { get; set; }
        public DateOnly Dt_nasci { get; set; }
        public String? Telefone { get; set; }
        [EmailAddress]
        public String? Email { get; set; }
    }
}