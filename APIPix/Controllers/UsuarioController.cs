using APIPix.Business.UsuarioBusiness;
using APIPix.Entity.Filters;
using Microsoft.AspNetCore.Mvc;

namespace APIPix.Controllers
{
    [ApiController]
    [Route("api/v1/{controller}")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioBusiness _usuarioBusiness;
        public UsuarioController(UsuarioBusiness usuarioBusiness)
        {
            _usuarioBusiness = usuarioBusiness;
        }

        [HttpGet]
        public IActionResult BuscarUsuarioByCpfCnpj(string CpfCnpj)
        {
            try
            {
                return Ok(_usuarioBusiness.BuscarUsuarioByCpfCnpj(CpfCnpj));
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
        public IActionResult ExcluirUsuario(string CpfCnpj)
        {
            try
            {
                _usuarioBusiness.ExcluirUsuarioByCpfCnpj(CpfCnpj);
                return Ok("Usuário excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
