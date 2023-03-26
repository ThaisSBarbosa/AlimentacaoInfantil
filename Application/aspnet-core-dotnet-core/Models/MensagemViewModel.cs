using System;

namespace AlimentacaoInfantil.Models
{
    public class MensagemViewModel
    {
        public int Codigo { get; set; }

        public string Conteudo { get; set; }

        public int CodigoUsuarioRemetente { get; set; }

        public int CodigoUsuarioDestinatario { get; set; }

        public string Status { get; set; }

        public DateTime DataAtualizacao { get; set; }

    }
}