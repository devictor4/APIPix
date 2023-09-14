using APIPix.Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIPix.Entity.Entities
{
    public class ChavePix
    {
        [Key]
        public long id { get; set; }
        public TipoChavePix tipoChavePix { get; set; }
        public string DescChavePix { get; set; }
    }
}
