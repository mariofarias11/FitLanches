using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Service;
using WebApplication1.Model;

namespace WebApplication1.Services
{
    public class BalcaoService: IBalcaoService

    {
        int Capacidade = 0;
        List<Pedido> Fila = new List<Pedido>();
        List<Pedido> EmAtendimento = new List<Pedido>();
        Cardapio Cardapio = new Cardapio();


        public BalcaoService()
        {
            Capacidade = 2;
        }

        public Cardapio ListarCardapio()
        {
            var cardapio = new Cardapio();
            return cardapio;
        }

        public Pedido ConsultarPedido (int pedidoId)
        {
            var pedido = Fila.Where(x => x.Id == pedidoId).FirstOrDefault();
            if (pedido != null)
            {
                return pedido;
            }
            pedido = EmAtendimento.Where(x => x.Id == pedidoId).FirstOrDefault();
            if (pedido != null)
            {
                return pedido;
            }
            Console.WriteLine("O Pedido não foi encontrado");
            return null;
        }

        public void AvancarFila()
        {
            if (Capacidade > 0)
            {
                var primeiroDaFila = Fila.FirstOrDefault();
                Fila.Remove(primeiroDaFila);
                TratarPedido(primeiroDaFila);
            } else
            {
                Console.WriteLine("O Balcão está lotado");
            }
        }


        public int FazerPedido(Pedido pedido)
        {
            AdicionaBrinde(pedido);

            pedido.Id = new Random().Next();
            if (BalcaoDisponivel())
            {
                TratarPedido(pedido);
                AvancarFila();
            }
            else
            {
                PedidoEmEspera(pedido);
            }
            return pedido.Id;
        }

        private void TratarPedido(Pedido pedido)
        {
            if (pedido != null)
            {
                AtenderPedido(pedido);
                PrepararPedido(pedido);
                DespacharPedido(pedido);
                EntregarPedido(pedido);
                FinalizarAtendimento(pedido);
            }
        }

        #region MetodoPrivados

        private bool BalcaoDisponivel()
        {
            if (Capacidade == 0)
            {
                return false;
            }
            if (Capacidade > 0)
            {
                Capacidade--;
                return true;
            }
            return false;
        }

        private void LiberarBalcao()
        {
            Capacidade++;
        }

        private void AtenderPedido(Pedido pedido)
        {
            EmAtendimento.Add(pedido);
        }

        private void FinalizarAtendimento(Pedido pedido)
        {
            EmAtendimento.Remove(pedido);
            LiberarBalcao();
        }

        private void PrepararPedido(Pedido pedido)
        {
            pedido.StatusPedido = Enum.StatusPedido.Preprando;
            var tempoTotalPedido = 0;
            foreach (var item in pedido.Itens)
            {
                tempoTotalPedido += item.TempoPreparo;
            }
            System.Threading.Thread.Sleep(tempoTotalPedido * 100);
            Console.WriteLine("pedido " + pedido.Id + ": " + pedido.StatusPedido);
        }

        private void PedidoEmEspera(Pedido pedido)
        {
            Fila.Add(pedido);
        }

        private void EntregarPedido(Pedido pedido)
        {
            pedido.StatusPedido = Enum.StatusPedido.Entregue;
            Console.WriteLine("pedido " + pedido.Id + ": " + pedido.StatusPedido);
        }

        private void DespacharPedido(Pedido pedido)
        {
            pedido.StatusPedido = Enum.StatusPedido.ProntoParaEntrega;
            Console.WriteLine("pedido " + pedido.Id + ": " + pedido.StatusPedido);
        }

        private bool AdicionaBrinde(Pedido pedido)
        {
            if (pedido.Itens.Count(x => x.Descricao == Cardapio.Hamburguer.Descricao) >= 2)
            {
                pedido.Itens.Add(Cardapio.Suco);
                Console.WriteLine("Você ganhou um suco de brinde!");
            }
            else
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
