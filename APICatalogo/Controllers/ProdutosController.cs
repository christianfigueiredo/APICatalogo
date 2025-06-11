using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly Contexto _contexto;
        public ProdutosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _contexto.Produtos?.ToList();
            if (produtos is null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }
    }
}
