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
        public IActionResult CadastrarChavePix(CadastrarChavePixFilter cadastrarChavePixFilter)
        {
            try
            {
                return Created("", _chavePixBusiness.CadastrarChavePix(cadastrarChavePixFilter));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
