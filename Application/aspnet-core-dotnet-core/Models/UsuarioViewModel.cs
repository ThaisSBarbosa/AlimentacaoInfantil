using AlimentacaoInfantil.Enums;

namespace AlimentacaoInfantil.Models
{
    public class UsuarioViewModel
    {
        public int Codigo { get; set; }

        public string Nome { get; set; }

        public EnumTipoUsuario Tipo { get; set; }
    }
}