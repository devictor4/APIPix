using APIPix.Entity.DTO.UsuarioDTO;
using APIPix.Entity.Filters;
using APIPix.Entity.Util;
using APIPix.Repository.Context;
using APIPix.Repository.Model;

namespace APIPix.Business.UsuarioBusiness
{
    public class UsuarioBusiness
    {
        private readonly APIPixContext _context;

        public UsuarioBusiness(APIPixContext aPIPixContext)
        {
            _context = aPIPixContext;
        }

        public UsuarioDTO BuscarUsuarioByCpfCnpj(string cpfCnpj)
        {
            if (string.IsNullOrEmpty(cpfCnpj)) throw new Exception("O Campo CPF ou CNPJ não pode ser vazio.");
            if (!Utilities.IsCpf(cpfCnpj) || !Utilities.IsCnpj(cpfCnpj)) throw new Exception("É necessário informar um CPF ou CNPJ válido.");

            var result = _context.usuario.Where(x => x.cpfCnpj == cpfCnpj).FirstOrDefault();

            if (result == null) throw new Exception("Usuário não encontrado.");
            if (result.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");

            UsuarioDTO usuarioDTO = new UsuarioDTO(result);

            return usuarioDTO;
        }

        public Usuario SalvarUsuario(SalvarUsuarioFilter salvarUsuarioFilter)
        {
            Usuario usuario = new Usuario();
            ValidaCampos(salvarUsuarioFilter);

            var result = _context.usuario.Where(x => x.cpfCnpj == salvarUsuarioFilter.CpfCnpj).FirstOrDefault();

            if(result == null)
            {
                usuario = new()
                {
                    nome = salvarUsuarioFilter.Nome,
                    cpfCnpj = salvarUsuarioFilter.CpfCnpj,
                    dataNascimento = salvarUsuarioFilter.DataNascimento.Value,
                    email = salvarUsuarioFilter.Email,
                    telefone = salvarUsuarioFilter.Telefone.Value,
                    dataInclusao = DateTime.Now,
                    stExclusao = false
                };

                _context.usuario.Add(usuario);
                _context.SaveChanges();

                return usuario;
            }
            else
            {
                result.nome = salvarUsuarioFilter.Nome;
                result.dataNascimento = salvarUsuarioFilter.DataNascimento.HasValue ? salvarUsuarioFilter.DataNascimento.Value : result.dataNascimento;
                result.email = salvarUsuarioFilter.Email;
                result.telefone = salvarUsuarioFilter.Telefone.HasValue ? salvarUsuarioFilter.Telefone.Value : result.telefone;
                result.dataAlteracao = DateTime.Now;
                result.stExclusao = false;

                _context.usuario.Update(result);
                _context.SaveChanges();

                return result;
            }
        }

        public void ExcluirUsuarioByCpfCnpj(string cpfCnpj)
        {
            var result = _context.usuario.Where(x => x.cpfCnpj == cpfCnpj).FirstOrDefault();
            if (result == null) throw new Exception("Usuário não encontrado.");
            if (result.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");

            result.dataExclusao = DateTime.Now;
            result.stExclusao = true;

            _context.usuario.Update(result);
            _context.SaveChanges();
        }

        private void ValidaCampos(SalvarUsuarioFilter salvarUsuarioFilter)
        {
            if (string.IsNullOrEmpty(salvarUsuarioFilter.Nome)) throw new Exception("O campo Nome deve ser preenchido.");
            if (!salvarUsuarioFilter.DataNascimento.HasValue) throw new Exception("O campo Data de Nascimento deve ser preenchido.");
            if (string.IsNullOrEmpty(salvarUsuarioFilter.Email)) throw new Exception("O campo Email deve ser preenchido.");
            if (!salvarUsuarioFilter.Telefone.HasValue) throw new Exception("O campo Telefone deve ser preenchido.");
            if (string.IsNullOrEmpty(salvarUsuarioFilter.CpfCnpj)) throw new Exception("O campo CPF ou CNPJ deve ser preenchido.");
            if (!Utilities.IsCpf(salvarUsuarioFilter.CpfCnpj) || !Utilities.IsCnpj(salvarUsuarioFilter.CpfCnpj)) throw new Exception("É necessário informar um CPF ou CNPJ válido.");
            if (salvarUsuarioFilter.CpfCnpj.Length != 11 && salvarUsuarioFilter.CpfCnpj.Length != 14) throw new Exception("O campo CPF ou CNPJ deve ser preenchido de forma correta.");
        }
    }
}
