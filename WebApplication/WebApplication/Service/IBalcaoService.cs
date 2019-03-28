using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication.Service
{
    public interface IBalcaoService
    {
        Cardapio ListarCardapio();
        Pedido ConsultarPedido(int pedidoId);
        void AvancarFila();
        int FazerPedido(Pedido pedido);
    }
}