using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Enum;

namespace WebApplication1.Model
{
    public class Pedido
    {
        public int Id { get; set; }
        public List<CardapioItem> Itens { get; set; }
        public StatusPedido StatusPedido { get; set; }

        public Pedido()
        {
            StatusPedido = StatusPedido.Aguardando;
        }
    }
}
