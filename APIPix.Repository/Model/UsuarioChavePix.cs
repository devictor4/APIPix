using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIPix.Repository.Model
{
    [Table("UsuarioChavePix")]
    public class UsuarioChavePix
    {
        [Key]
        [Column("id")]
        public long id { get; set; }
        [Column("idUsuario")]
        public long idUsuario { get; set; }
        [Column("idChavePix")]
        public long idChavePix { get; set; }
        [Column("dataInclusao")]
        public DateTime dataInclusao { get; set; }
    }
}
