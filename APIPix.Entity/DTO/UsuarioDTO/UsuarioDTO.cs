using APIPix.Repository.Model;

namespace APIPix.Entity.DTO.UsuarioDTO
{
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public long Telefone { get; set; }
        public bool StExclusao { get; set; }

        public UsuarioDTO()
        {
            
        }

        public UsuarioDTO(Usuario item)
        {
            Id = item.id;
            Nome = item.nome;
            CpfCnpj = item.cpfCnpj;
            Email = item.email;
            Telefone = item.telefone;
            StExclusao = item.stExclusao;
        }
    }
}
