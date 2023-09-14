using System.ComponentModel.DataAnnotations;

namespace APIPix.Entity.Entities
{
    public class UsuarioChavePix
    {
        [Key]
        public long id { get; set; }
        public long idUsuario { get; set; }
        public long idChavePix { get; set; }
    }
}
