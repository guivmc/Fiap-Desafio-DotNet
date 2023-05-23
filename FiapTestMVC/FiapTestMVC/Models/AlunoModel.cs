using System.ComponentModel.DataAnnotations;

namespace FiapTestWeb.Models
{
    public class AlunoModel
    {
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; } = null!;
        [Required]
        public string Usuario { get; set; } = null!;
        [Required]
        public string Senha { get; set; } = null!;
    }
}
