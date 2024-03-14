using System.ComponentModel.DataAnnotations;

namespace Catalogar.Models.ViewModel
{
    public class RegisterProductViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public int Quantidade { get; set; }

        public RegisterProductViewModel()
        {
            
        }

        public RegisterProductViewModel(string nome , decimal preco, int quantidade)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }
    }
}
