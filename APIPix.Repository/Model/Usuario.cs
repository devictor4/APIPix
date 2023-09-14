using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPix.Repository.Model
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public long id { get; set; }
        [Column("nome")]
        public string nome { get; set; }
        [Column("dataNascimento")]
        public DateTime dataNascimento { get; set; }
        [Column("cpfCnpj")]
        public string cpfCnpj { get; set; }
        [Column("email")]
        public string email { get; set; }
        [Column("telefone")]
        public long telefone { get; set; }
        [Column("dataInclusao")]
        public DateTime dataInclusao { get; set; }
        [Column("dataAlteracao")]
        public DateTime? dataAlteracao { get; set; }
        [Column("dataExclusao")]
        public DateTime? dataExclusao { get; set; }
        [Column("stExclusao")]
        public bool stExclusao { get; set; }
    }
}