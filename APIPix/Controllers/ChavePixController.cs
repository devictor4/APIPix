using APIPix.Business.ChavePixBusiness;
using APIPix.Entity.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public IActionResult ListarChavePixByUsuario(string cpfCnpj)
        {
            try
            {
                return Ok(_chavePixBusiness.ListarChavePixByUsuario(cpfCnpj));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

        [HttpDelete]
        public IActionResult ExcluirChavePixByCpfCnpj(string cpfCnpj, int tipoChavePix)
        {
            try
            {
                _chavePixBusiness.ExcluirChavePixByCpfCnpj(cpfCnpj, tipoChavePix);
                return Ok("Chave Pix excluída com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
