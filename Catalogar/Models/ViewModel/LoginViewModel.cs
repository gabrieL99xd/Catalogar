using System.ComponentModel.DataAnnotations;

namespace Catalogar.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }

        public LoginViewModel()
        {
            
        }

        public LoginViewModel(string email,  string password, bool rememberMe)
        {
            Email = email;
            Password = password;
        }
    }
}
