using System.ComponentModel.DataAnnotations;

namespace InkStudio.Entities
{
    public class Tatuador
    {
        public int TatuadorId { get; set; }
        public String? Nome { get; set; }
        public String? Estilo { get; set; }
        public DateOnly Admissão { get; set; }
        public String? Telefone { get; set; }
        [EmailAddress]
        public String? Email { get; set; }

    }
}