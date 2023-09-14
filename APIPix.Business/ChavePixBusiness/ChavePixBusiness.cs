using APIPix.Entity.Filters;
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

        public ChavePix CadastrarChavePix(CadastrarChavePixFilter cadastrarChavePixFilter)
        {
            if (string.IsNullOrEmpty(cadastrarChavePixFilter.cpfCnpjUsuario)) throw new Exception("O Campo CPF ou CNPJ não pode ser vazio.");

            var usuario = _context.usuario.Where(x => x.cpfCnpj == cadastrarChavePixFilter.cpfCnpjUsuario).FirstOrDefault();
            
            if (usuario == null) throw new Exception("Usuário não encontrado.");
            if (usuario.stExclusao) throw new Exception("O usuário informado encontra-se desligado.");
            if (cadastrarChavePixFilter.tipoChavePix.GetHashCode() < 1 || cadastrarChavePixFilter.tipoChavePix.GetHashCode() > 5) throw new Exception("É necessário informar um Tipo de Chave PIX válido.");
            if (cadastrarChavePixFilter.tipoChavePix.GetHashCode() != 5 && string.IsNullOrEmpty(cadastrarChavePixFilter.descChavePix)) throw new Exception("É necessário informar um valor para a Chave PIX escolhida.");

            ChavePix chavePix = new() {
                tipoChavePix = cadastrarChavePixFilter.tipoChavePix.GetHashCode(),
                descChavePix = cadastrarChavePixFilter.tipoChavePix.GetHashCode() == 5 ? Guid.NewGuid().ToString() : cadastrarChavePixFilter.descChavePix
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
    }
}
