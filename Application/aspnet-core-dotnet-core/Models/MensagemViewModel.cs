using System;
using AlimentacaoInfantil.Enums;

namespace AlimentacaoInfantil.Models
{
    public class MensagemViewModel
    {
        public int Codigo { get; set; }

        public string Conteudo { get; set; }

        public int CodigoUsuarioRemetente { get; set; }

        public int CodigoUsuarioDestinatario { get; set; }

        public EnumStatusMensagem Status { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public int RespondendoMensagem { get; set; }

    }
}