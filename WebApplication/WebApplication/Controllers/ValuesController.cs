using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Service;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly IBalcaoService _balcaoService;
        
        public ValuesController(IBalcaoService balcaoService)
        {
            _balcaoService = balcaoService;
        }

        [HttpGet]
        [Route("cardapio")]
        public Cardapio ListarCardapio()
        {
            return _balcaoService.ListarCardapio();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Route("consultar")]
        public Task<Pedido> ConsultarPedido(int idPedido)
        {
            return Task.Run(() =>
            { 
                return _balcaoService.ConsultarPedido(idPedido);
            });
        }

        // POST api/values
        [HttpPost]
        [Route("fazerPedido")]
        public int FazerPedido([FromBody] Pedido pedido)
        {
            return _balcaoService.FazerPedido(pedido);
        }

        [HttpPost("avancarFila")]
        [Route("teste")]
        public int AvancarFila()
        {
            _balcaoService.AvancarFila();
            return 0;
        }

    }
}



