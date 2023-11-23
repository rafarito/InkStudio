using System.ComponentModel.DataAnnotations;

namespace InkStudio.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="O Email é obrigatorio")]
        [EmailAddress(ErrorMessage = "Email Invalido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatoria")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}