using System.ComponentModel.DataAnnotations;

namespace APIPix.Entity.Entities
{
    public class Usuario
    {
        [Key]
        public long id { get; set; }
        public string nome { get; set; }
        public DateTime dataNascimento { get; set; }
        public string cpfCnpj { get; set; }
        public string email { get; set; }
        public long telefone { get; set; }
    }
}