using APIPix.Repository.Model;
namespace APIPix.Entity.Filters
{
    public class CadastrarUsuarioFilter
    {
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Email { get; set; }
        public long? Telefone { get; set; }

    }
}
