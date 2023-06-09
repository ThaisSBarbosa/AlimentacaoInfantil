using AlimentacaoInfantil.Enums;

namespace AlimentacaoInfantil.Models
{
    public class UsuarioViewModel
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public EnumTipoUsuario Tipo { get; set; }

        public string DescricaoTipo { get; set; }

        public bool Amigos { get; set; }
    }
}