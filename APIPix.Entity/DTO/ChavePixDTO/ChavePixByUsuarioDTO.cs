using APIPix.Entity.Enums;
using APIPix.Entity.Util;
using APIPix.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static APIPix.Entity.Util.Utilities;

namespace APIPix.Entity.DTO.ChavePixDTO
{
    public class ChavePixByUsuarioDTO
    {
        public string tipoChavePix { get; set; }
        public string descChavePix { get; set; }

        public ChavePixByUsuarioDTO()
        {
            
        }

        public ChavePixByUsuarioDTO(ChavePix item)
        {
            tipoChavePix = DescriptionEnum.GetEnumDescription((TipoChavePix)item.tipoChavePix);
            descChavePix = item.descChavePix;
        }
    }
}
