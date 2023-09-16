using APIPix.Entity.Enums;
using APIPix.Entity.Filters;
using APIPix.Entity.Util;
using APIPix.Repository.Context;
using APIPix.Repository.Model;

namespace APIPix.Business.ChavePixBusiness
{
    public class ChavePixBusiness
    {
        private readonly APIPixContext _context;

        public ChavePixBusiness(APIPixContext aPIPixContext)
        {
            _context = aPIPixContext;
        }

        public UsuarioChavePix ListarPixUsuario(string cpfCnpj)
        {
            var usuario = _context.usuario.Where(x => x.cpfCnpj == cpfCnpj).FirstOrDefault();
            return null;
        }

        public ChavePix SalvarChavePix(SalvarChavePixFilter salvarChavePixFilter)
        {
            if (string.IsNullOrEmpty(salvarChavePixFilter.cpfCnpjUsuario)) throw new Exception("O Campo CPF ou CNPJ não pode ser vazio.");

            var usuario = _context.usuario.Where(x => x.cpfCnpj == salvarChavePixFilter.cpfCnpjUsuario).FirstOrDefault();
            
            if (usuario == null) throw new Exception("Usuário não encontrado.");
            if (usuario.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");

            if (salvarChavePixFilter.tipoChavePix.GetHashCode() == 4) salvarChavePixFilter.descChavePix = Utilities.OnlyNumbers(salvarChavePixFilter.descChavePix);

            ValidaChavePix(salvarChavePixFilter);

            //Crítica Tipo PIX já cadastrado para Usuário a partir do Endpoint ListarPixUsuario

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
                dataInclusao = DateTime.Now,
                stExclusao = false
            };

            _context.usuarioChavePix.Add(usuarioChavePix);
            _context.SaveChanges();

            return chavePix;
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
