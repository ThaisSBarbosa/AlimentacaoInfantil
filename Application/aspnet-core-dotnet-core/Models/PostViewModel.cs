using System;

namespace AlimentacaoInfantil.Models
{
    public class PostViewModel
    {
        public int Codigo { get; set; }

        public string Conteudo { get; set; }

        public string Autor { get; set; }

        public string Amei { get; set; }

        public Boolean Anuncio { get; set; }

        public DateTime DataAtualizacao { get; set; }

    }
}