using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    [Route("api/Produto")]
    [ApiController]
    public class ProdutoAPIController : ControllerBase
    {
        //[Route("BuscarPorId/{id}/{teste}")]
        //public IActionResult BuscarPorId(int id, string teste)

        private readonly ProdutoDAO _produtoDAO;
        public ProdutoAPIController(ProdutoDAO produtoDAO)
        {
            _produtoDAO = produtoDAO;
        }
        //GET: api/Produto/ListarTodos
        [HttpGet]
        [Route("ListarTodos")]
        public IActionResult ListarTodos()
        {
            return Ok(_produtoDAO.ListarTodos());
        }
        //GET: api/Produto/BuscarPorId/2
        [HttpGet]
        [Route("BuscarPorId/{id}")]
        public IActionResult BuscarPorId(int id)
        {
            Produto p = _produtoDAO.BuscarPorId(id);
            if (p != null)
            {
                return Ok(p);
            }
            return NotFound(new { msg = "Esse produto não existe!" });
        }
        //GET: api/Produto/BuscarPorCategoria/2
        [HttpGet]
        [Route("BuscarPorCategoria/{id}")]
        public IActionResult BuscarPorCategoria(int id)
        {
            List<Produto> produtos =
                _produtoDAO.ListarPorCategoria(id);
            if (produtos.Count > 0)
            {
                return Ok(produtos);
            }
            return NotFound(new { msg = "Não existem produtos!" });
        }

        //POST: api/Produto/Cadastrar
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar(Produto p)
        {
            if (ModelState.IsValid)
            {
                if (_produtoDAO.Cadastrar(p))
                {
                    return Created("", p);
                }
                return Conflict(new { msg = "Esse produto já existe!" });
            }
            return BadRequest(ModelState);
        }
    }
}