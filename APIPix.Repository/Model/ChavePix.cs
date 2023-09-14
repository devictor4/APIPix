using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPix.Repository.Model
{
    [Table("ChavePix")]
    public class ChavePix
    {
        [Key]
        [Column("id")]
        public long id { get; set; }
        [Column("tipoChavePix")]
        public int tipoChavePix { get; set; }
        [Column("descChavePix")]
        public string descChavePix { get; set; }
    }
}
