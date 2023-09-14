using APIPix.Business.UsuarioBusiness;
using APIPix.Entity.Filters;
using Microsoft.AspNetCore.Mvc;

namespace APIPix.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioBusiness _usuarioBusiness;
        public UsuarioController(UsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        [HttpGet]
        public IActionResult BuscarUsuarioByCpfCnpj(string cpfCnpj)
        {
            try
            {
                return Ok(_usuarioBusiness.BuscarUsuarioByCpfCnpj(cpfCnpj));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CadastrarUsuarioFilter usuarioFilter)
        {
            try
            {
                return Created("", _usuarioBusiness.CadastrarUsuario(usuarioFilter));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult AlterarUsuario(CadastrarUsuarioFilter usuarioFilter)
        {
            try
            {
                _usuarioBusiness.AlterarUsuario(usuarioFilter);
                return Ok("Usuário alterado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult ExcluirUsuarioByCpfCnpj(string cpfCnpj)
        {
            try
            {
                _usuarioBusiness.ExcluirUsuarioByCpfCnpj(cpfCnpj);
                return Ok("Usuário excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
