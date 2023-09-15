using APIPix.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIPix.Entity.Filters
{
    public class SalvarChavePixFilter
    {
        public TipoChavePix tipoChavePix { get; set; }
        public string descChavePix { get; set; }
        public string cpfCnpjUsuario { get; set; }
    }
}
