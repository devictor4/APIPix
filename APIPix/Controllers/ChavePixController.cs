using APIPix.Business.ChavePixBusiness;
using Microsoft.AspNetCore.Mvc;

namespace APIPix.Controllers
{
    [ApiController]
    [Route("api/v1/{controller}")]
    public class ChavePixController : ControllerBase
    {
        private readonly ChavePixBusiness _chavePixBusiness;

        public ChavePixController(ChavePixBusiness chavePixBusiness)
        {
            _chavePixBusiness = chavePixBusiness;
        }

        public IActionResult BuscarChavePix(long id)
        {
            var result = _chavePixBusiness.BuscarChavePix(id);

            return result;
        }
    }
}
