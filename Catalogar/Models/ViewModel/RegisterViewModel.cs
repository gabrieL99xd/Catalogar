using System.ComponentModel.DataAnnotations;

namespace Catalogar.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O usuário é obrigatório")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmarSenha { get; set; }
        public RegisterViewModel(string usuario , string email , string senha , string confirmarSenha )
        {
            Usuario = usuario;
            Email = email;
            Senha = senha;
            ConfirmarSenha = confirmarSenha;
        }
        public RegisterViewModel() {    }
    }
}
