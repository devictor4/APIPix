using APIPix.Entity.DTO.UsuarioDTO;
using APIPix.Entity.Filters;
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

            var result = _context.usuario.Where(x => x.cpfCnpj == cpfCnpj).FirstOrDefault();

            if(result == null) throw new Exception("Usuário não encontrado.");
            if (result.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");

            UsuarioDTO usuarioDTO = new UsuarioDTO(result);

            return usuarioDTO;
        }

        public Usuario CadastrarUsuario(CadastrarUsuarioFilter usuarioFilter)
        {
            Usuario usuario = new Usuario();
            ValidaCampos(usuarioFilter);

            var result = _context.usuario.Where(x => x.cpfCnpj == usuarioFilter.CpfCnpj).FirstOrDefault();
            if (result != null && !result.stExclusao) throw new Exception("Já existe um usuário cadastrado com esse CPF ou CNPJ");

            if(result != null && result.stExclusao)
            {
                result.nome = usuarioFilter.Nome;
                result.dataNascimento = usuarioFilter.DataNascimento.Value;
                result.email = usuarioFilter.Email;
                result.telefone = usuarioFilter.Telefone.Value;
                result.dataAlteracao = DateTime.Now;
                result.stExclusao = false;

                _context.usuario.Update(result);
                _context.SaveChanges();

                return result;
            }
            else
            {
                usuario = new()
                {
                    nome = usuarioFilter.Nome,
                    cpfCnpj = usuarioFilter.CpfCnpj,
                    dataNascimento = usuarioFilter.DataNascimento.Value,
                    email = usuarioFilter.Email,
                    telefone = usuarioFilter.Telefone.Value,
                    dataInclusao = DateTime.Now,
                    stExclusao = false
                };

                _context.usuario.Add(usuario);
                _context.SaveChanges();

                return usuario;
            }
        }

        public void AlterarUsuario(CadastrarUsuarioFilter usuarioFilter)
        {
            ValidaCampos(usuarioFilter);
            var result = _context.usuario.Where(x => x.cpfCnpj == usuarioFilter.CpfCnpj).FirstOrDefault();
            if (result == null) throw new Exception("Usuário não encontrado.");
            if (result.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");

            result.nome = usuarioFilter.Nome;
            result.cpfCnpj = usuarioFilter.CpfCnpj;
            result.dataNascimento = usuarioFilter.DataNascimento.Value;
            result.email = usuarioFilter.Email;
            result.telefone = usuarioFilter.Telefone.Value;
            result.dataAlteracao = DateTime.Now;

            _context.usuario.Update(result);
            _context.SaveChanges();
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

        private void ValidaCampos(CadastrarUsuarioFilter usuarioFilter)
        {
            if (string.IsNullOrEmpty(usuarioFilter.Nome)) throw new Exception("O campo Nome deve ser preenchido.");
            if (!usuarioFilter.DataNascimento.HasValue) throw new Exception("O campo Data de Nascimento deve ser preenchido.");
            if (string.IsNullOrEmpty(usuarioFilter.Email)) throw new Exception("O campo Email deve ser preenchido.");
            if (!usuarioFilter.Telefone.HasValue) throw new Exception("O campo Telefone deve ser preenchido.");
            if (string.IsNullOrEmpty(usuarioFilter.CpfCnpj)) throw new Exception("O campo CPF ou CNPJ deve ser preenchido.");
            if (usuarioFilter.CpfCnpj.Length != 11 && usuarioFilter.CpfCnpj.Length != 14) throw new Exception("O campo CPF ou CNPJ deve ser preenchido de forma correta.");
        }
    }
}
