using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{id:int:min(1)}", Name="ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _contexto.Produtos?.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto nao encontrado...");
            }
            return Ok(produto);
        }

        [HttpGet("primeiroProduto")]
        public ActionResult<Produto> GetPrimeiro()
        {
            var produto = _contexto.Produtos?.FirstOrDefault();
            if (produto is null)
            {
                return NotFound("Nenhum produto encontrado.");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest("Produto nao pode ser nulo.");
            }

            _contexto.Produtos?.Add(produto);
            _contexto.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("Produto nao encontrado.");
            }

            _contexto.Entry(produto).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _contexto.Produtos?.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto nao encontrado.");
            }

            _contexto.Produtos?.Remove(produto);
            _contexto.SaveChanges();
            return Ok(produto);
        }
    }
}
