using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace ConsoleApp
{
    class Program
    {
        static List<int> idGerados = new List<int>();

        static void Main(string[] args)
        {
            //cardapio
            var clientCardapio = new RestClient("https://localhost:5001/api/values/cardapio");
            var request = new RestRequest(Method.GET);
            IRestResponse responseCardapio = clientCardapio.Execute(request);
            Console.WriteLine("Abaixo encontra-se nosso cardapio:");
            Console.WriteLine(responseCardapio.Content);

            //fazer pedido
            (new Thread(() => {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("Escolha seu pedido passando o ID do produto listado acima:");
                    var Id = int.Parse(Console.ReadLine());
                    var filterPedido = DefinePedido(Id);
                    var responseRealizaPedido = MontarPostRealizarPedido(filterPedido);
                    int.TryParse(responseRealizaPedido.Content, out Id);
                    idGerados.Add(Id);
                    Console.ReadKey();
                }
            })).Start();

            //consultar pedido
            (new Thread(() => {
                while(idGerados.Count > 0)
                {
                    consultarPedidos(idGerados);
                }
            })).Start();
         

        }

        #region metodos

        public static void consultarPedidos(List<int> idPedidos)
        {
            while (idGerados.Count > 0)
            {
                foreach(var pedido in idPedidos)
                {
                    var response = MontarPostConsultar(pedido);
                    Console.WriteLine(response.Content);
                }
            }
        }

        public static IRestResponse MontarPostRealizarPedido (Pedido pedido)
        {
            var clientRealizaPedido = new RestClient("https://localhost:5001/api/values/fazerPedido");
            var requestRealizaPedido = new RestRequest(Method.POST);
            requestRealizaPedido.AddJsonBody(pedido);
            IRestResponse responseRealizaPedido = clientRealizaPedido.Execute(requestRealizaPedido);
            Console.WriteLine(responseRealizaPedido.Content);

            return responseRealizaPedido;
        } 

        public static IRestResponse MontarPostConsultar(int filter)
        {
            var client = new RestClient("https://localhost:5001/api/values/consultar");
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(filter);
            IRestResponse response = client.Execute(request);
            return response;
        }


        public static Pedido DefinePedido(int Id)
        {
            var pedido = new Pedido();
            pedido.Itens = new List<CardapioItem>();
            if(Id == 1)
            {
                pedido.Id = 1;
                var Hamburguer = new CardapioItem()
                {
                    Descricao = "Hambuguer",
                    TempoPreparo = 20,
                    Id = 1
                };
                pedido.Itens.Add(Hamburguer);
            }

            if (Id == 2)
            {
                pedido.Id = 2;
                var MistoQuente = new CardapioItem()
                {
                    Descricao = "Misto quente",
                    TempoPreparo = 15,
                    Id = 2
                };
                pedido.Itens.Add(MistoQuente);
            }

            if (Id == 3)
            {
                pedido.Id = 3;
                var LancheNatural = new CardapioItem()
                {
                    Descricao = "Lanche natural",
                    TempoPreparo = 10,
                    Id = 3
                };
                pedido.Itens.Add(LancheNatural);
            }

            if (Id == 4)
            {
                pedido.Id = 4;
                var Suco = new CardapioItem()
                {
                    Descricao = "Suco",
                    TempoPreparo = 0,
                    Id = 4
                };
                pedido.Itens.Add(Suco);
            }

            return pedido;
        }
        #endregion
    }
}
