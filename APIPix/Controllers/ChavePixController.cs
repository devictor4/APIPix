using APIPix.Business.ChavePixBusiness;
using APIPix.Entity.Filters;
using Microsoft.AspNetCore.Mvc;

namespace APIPix.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class ChavePixController : ControllerBase
    {
        private readonly ChavePixBusiness _chavePixBusiness;

        public ChavePixController(ChavePixBusiness chavePixBusiness)
        {
            _chavePixBusiness = chavePixBusiness;
        }

        [HttpPost]
        public IActionResult SalvarChavePix(SalvarChavePixFilter salvarChavePixFilter)
        {
            try
            {
                return Created("", _chavePixBusiness.SalvarChavePix(salvarChavePixFilter));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
