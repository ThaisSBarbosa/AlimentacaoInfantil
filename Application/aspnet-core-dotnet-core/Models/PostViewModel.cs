using System;

namespace AlimentacaoInfantil.Models
{
    public class PostViewModel
    {
        public int Codigo { get; set; }

        public string Conteudo { get; set; }

        public int Autor { get; set; }

        public int Amei { get; set; }

        public bool Anuncio { get; set; }

        public DateTime Data { get; set; }
    }
}