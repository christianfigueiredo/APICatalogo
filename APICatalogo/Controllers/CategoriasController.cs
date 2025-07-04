﻿using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        public readonly Contexto _contexto;
        public CategoriasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
           // return _contexto.Categorias?.Include(c => c.Produtos).ToList() ?? new List<Categoria>();
            return _contexto.Categorias?.Include(c => c.Produtos).Where(c=>c.CategoriaId <= 5).ToList() ?? new List<Categoria>();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = _contexto.Categorias?.AsNoTracking().ToList();
                if (categorias is null || !categorias.Any())
                {
                    return NotFound("Nenhuma categoria encontrada.");
                }
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
            
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _contexto.Categorias?.FirstOrDefault(c => c.CategoriaId == id);
                if (categoria is null)
                {
                    return NotFound($"Categoria com id = {id} não encontrada.");
                }
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
           
        }

        [HttpPost]
        public IActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Categoria nao pode ser nula.");
            }

            _contexto.Categorias?.Add(categoria);
            _contexto.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Categoria nao encontrada.");
            }

            _contexto.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var categoria = _contexto.Categorias?.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound($"Categoria com id={id} nao encontrada.");
            }

            _contexto.Categorias?.Remove(categoria);
            _contexto.SaveChanges();
            return Ok("Categoria removida com sucesso.");
        }
    }
}
