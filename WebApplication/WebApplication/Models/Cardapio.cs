using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Cardapio
    {
        public CardapioItem Hamburguer { get; set; }
        public CardapioItem MistoQuente { get; set; }
        public CardapioItem LancheNatural { get; set; }
        public CardapioItem Suco { get; set; }
  
        public  Cardapio()
        {
            Hamburguer = new CardapioItem()
            {
                Descricao = "Hambuguer",
                TempoPreparo = 20,
                Id = 1
            };

            MistoQuente = new CardapioItem()
            {
                Descricao = "Misto quente",
                TempoPreparo = 15,
                Id = 2

            };

            LancheNatural = new CardapioItem()
            {
                Descricao = "Lanche natural",
                TempoPreparo = 10,
                Id = 3
            };

            Suco = new CardapioItem()
            {
                Descricao = "Suco",
                TempoPreparo = 0,
                Id = 4
            };
        }
    }
}
