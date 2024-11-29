using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nome { get; set; } = string.Empty;
        public int? Idade { get; set; }
    }
}
