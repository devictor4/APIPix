using APIPix.Entity.DTO.ChavePixDTO;
using APIPix.Entity.Enums;
using APIPix.Entity.Filters;
using APIPix.Entity.Util;
using APIPix.Repository.Context;
using APIPix.Repository.Model;
using System.Linq;
using static APIPix.Entity.Util.Utilities;

namespace APIPix.Business.ChavePixBusiness
{
    public class ChavePixBusiness
    {
        private readonly APIPixContext _context;

        public ChavePixBusiness(APIPixContext aPIPixContext)
        {
            _context = aPIPixContext;
        }

        public List<ChavePixByUsuarioDTO> ListarChavePixByUsuario(string cpfCnpj)
        {
            if (string.IsNullOrEmpty(cpfCnpj)) throw new Exception("O Campo CPF ou CNPJ não pode ser vazio.");

            var usuario = _context.usuario.Where(x => x.cpfCnpj == cpfCnpj).FirstOrDefault();

            if (usuario == null) throw new Exception("Usuário não encontrado.");
            if (usuario.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");

            var usuarioChavesPix = _context.usuarioChavePix.Where(x => x.idUsuario == usuario.id).ToList();

            List<ChavePixByUsuarioDTO> listChavesPix = new();

            if (usuarioChavesPix.Any())
            {
                usuarioChavesPix.ForEach(x =>
                {
                    var chavesPix = _context.chavePix.Where(y => y.id == x.idChavePix).FirstOrDefault();

                    ChavePixByUsuarioDTO chavePixByUsuario = new(chavesPix);

                    if (chavesPix != null) listChavesPix.Add(chavePixByUsuario);
                });
            }

            return listChavesPix;
        }

        public ChavePix SalvarChavePix(SalvarChavePixFilter salvarChavePixFilter)
        {
            if (string.IsNullOrEmpty(salvarChavePixFilter.cpfCnpjUsuario)) throw new Exception("O Campo CPF ou CNPJ não pode ser vazio.");

            var usuario = _context.usuario.Where(x => x.cpfCnpj == salvarChavePixFilter.cpfCnpjUsuario).FirstOrDefault();
            
            if (usuario == null) throw new Exception("Usuário não encontrado.");
            if (usuario.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");

            if (salvarChavePixFilter.tipoChavePix.GetHashCode() == 1 && usuario.cpfCnpj.Length == 11) salvarChavePixFilter.descChavePix = usuario.cpfCnpj;
            if (salvarChavePixFilter.tipoChavePix.GetHashCode() == 4) salvarChavePixFilter.descChavePix = Utilities.OnlyNumbers(salvarChavePixFilter.descChavePix);

            ValidaChavePix(salvarChavePixFilter);

            var chavesPixUsuario = ListarChavePixByUsuario(usuario.cpfCnpj);
            var possuiTipoChavePix = chavesPixUsuario.Where(x => x.tipoChavePix.Contains(DescriptionEnum.GetEnumDescription((TipoChavePix)salvarChavePixFilter.tipoChavePix))).FirstOrDefault();

            if (possuiTipoChavePix != null) throw new Exception($"O usuário já possui uma chave Pix cadastrada para o tipo escolhido. Tipo: {possuiTipoChavePix.tipoChavePix}");


            ChavePix chavePix = new() {
                tipoChavePix = salvarChavePixFilter.tipoChavePix.GetHashCode(),
                descChavePix = salvarChavePixFilter.tipoChavePix.GetHashCode() == 5 ? Guid.NewGuid().ToString() : salvarChavePixFilter.descChavePix
            };

            _context.chavePix.Add(chavePix);
            _context.SaveChanges();

            UsuarioChavePix usuarioChavePix = new()
            {
                idUsuario = usuario.id,
                idChavePix = chavePix.id,
                dataInclusao = DateTime.Now
            };

            _context.usuarioChavePix.Add(usuarioChavePix);
            _context.SaveChanges();

            return chavePix;
        }

        public void ExcluirChavePixByCpfCnpj(string cpfCnpj, int tipoChavePix)
        {
            if (string.IsNullOrEmpty(cpfCnpj)) throw new Exception("O Campo CPF ou CNPJ não pode ser vazio.");
            if (tipoChavePix == 0) throw new Exception("É necessário escolher um tipo de chave para exclusão.");

            var usuario = _context.usuario.Where(x => x.cpfCnpj == cpfCnpj).FirstOrDefault();

            if (usuario == null) throw new Exception("Usuário não encontrado.");
            if (usuario.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");

            var usuarioChavesPix = _context.usuarioChavePix.Where(x => x.idUsuario == usuario.id).ToList();

            if (!usuarioChavesPix.Any()) throw new Exception("Não foi encontrada uma chave Pix cadastrada para esse tipo.");

            usuarioChavesPix.ForEach(x =>
            {
                var chavesPix = _context.chavePix.Where(y => y.id == x.idChavePix && y.tipoChavePix == tipoChavePix).FirstOrDefault();
                if (chavesPix != null) 
                {
                    _context.usuarioChavePix.Remove(x);
                    _context.SaveChanges();

                    _context.chavePix.Remove(chavesPix);
                    _context.SaveChanges();
                } 
            });
        }

        public void ValidaChavePix(SalvarChavePixFilter salvarChavePixFilter)
        {
            //CPF = 1,
            //CNPJ = 2,
            //Email = 3,
            //Telefone = 4,
            //Aleatoria = 5
            if (salvarChavePixFilter.tipoChavePix.GetHashCode() < 1 || salvarChavePixFilter.tipoChavePix.GetHashCode() > 5) throw new Exception("É necessário informar um tipo de Chave PIX válida.");
            if (salvarChavePixFilter.tipoChavePix.GetHashCode() != 5 && string.IsNullOrEmpty(salvarChavePixFilter.descChavePix)) throw new Exception("É necessário informar um valor para a Chave PIX escolhida.");

            if (salvarChavePixFilter.tipoChavePix.GetHashCode() == 1 && !Utilities.IsCpf(salvarChavePixFilter.descChavePix)) throw new Exception("É necessário informar um CPF válido para a chave Pix.");
            if (salvarChavePixFilter.tipoChavePix.GetHashCode() == 2 && !Utilities.IsCnpj(salvarChavePixFilter.descChavePix)) throw new Exception("É necessário informar um CNPJ válido para a chave Pix.");
            if (salvarChavePixFilter.tipoChavePix.GetHashCode() == 3 && !Utilities.EmailValidado(salvarChavePixFilter.descChavePix)) throw new Exception("É necessário informar um Email válido para a chave Pix.");
            if (salvarChavePixFilter.tipoChavePix.GetHashCode() == 4 && salvarChavePixFilter.descChavePix.Length != 11) throw new Exception("É necessário informar um Telefone válido para a chave Pix.");
        }
    }
}
