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

        public ChavePix SalvarChavePix(SalvarChavePixFilter salvarChavePixFilter)
        {
            if (string.IsNullOrEmpty(salvarChavePixFilter.cpfCnpjUsuario)) throw new Exception("O Campo CPF ou CNPJ não pode ser vazio.");

            var usuario = _context.usuario.Where(x => x.cpfCnpj == salvarChavePixFilter.cpfCnpjUsuario).FirstOrDefault();
            
            if (usuario == null) throw new Exception("Usuário não encontrado.");
            if (usuario.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");

            if (salvarChavePixFilter.tipoChavePix.GetHashCode() < 1 || salvarChavePixFilter.tipoChavePix.GetHashCode() > 5) throw new Exception("É necessário informar um tipo de Chave PIX válida.");
            if (salvarChavePixFilter.tipoChavePix.GetHashCode() != 5 && string.IsNullOrEmpty(salvarChavePixFilter.descChavePix)) throw new Exception("É necessário informar um valor para a Chave PIX escolhida.");

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

        public bool ValidaChavePix(int tipoChavePix, string chavePix )
        {
            //CPF = 1,
            //CNPJ = 2,
            //Email = 3,
            //Telefone = 4,
            //Aleatoria = 5

            if (tipoChavePix == 1 && !Utilities.IsCpf(chavePix)) throw new Exception("É necessário informar um CPF válido para a chave Pix.");
            if (tipoChavePix == 2 && !Utilities.IsCnpj(chavePix)) throw new Exception("É necessário informar um CNPJ válido para a chave Pix.");
            if(tipoChavePix == 3 && !Utilities.EmailValidado(chavePix)) throw new Exception("É necessário informar um Email válido para a chave Pix.");


            return false;
        }
    }
}
