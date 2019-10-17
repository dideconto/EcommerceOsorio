using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceOsorio.DAL;
using EcommerceOsorio.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceOsorio.Controllers
{
    public class ProdutoController : Controller
    {
        //readonly serve para dizer que o objeto só receberá informação no 
        //contrutor ou na criação do objeto
        private readonly ProdutoDAO _produtoDAO;
        public ProdutoController(ProdutoDAO produtoDAO)
        {
            _produtoDAO = produtoDAO;
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(string txtNome, string txtDescricao, string txtPreco, string txtQuantidade)
        {
            Produto p = new Produto
            {
                Nome = txtNome,
                Descricao = txtDescricao,
                Quantidade = Convert.ToInt32(txtQuantidade),
                Preco = Convert.ToDouble(txtPreco)
            };
            _produtoDAO.Cadastrar(p);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            ViewBag.Produtos = _produtoDAO.ListarProdutos();
            ViewBag.DataHora = DateTime.Now;
            return View();
        }
        public IActionResult Remover(int id)
        {
            _produtoDAO.RemoverProduto(id);
            return RedirectToAction("Index");
        }

        public IActionResult Alterar(int id)
        {
            ViewBag.Produto = _produtoDAO.BuscarProdutoPorId(id);
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(string txtId, string hdnId, string txtNome, string txtDescricao, string txtPreco, string txtQuantidade)
        {
            Produto p = _produtoDAO.BuscarProdutoPorId(Convert.ToInt32(hdnId));
            p.Nome = txtNome;
            p.Descricao = txtDescricao;
            p.Quantidade = Convert.ToInt32(txtQuantidade);
            p.Preco = Convert.ToDouble(txtPreco);

            _produtoDAO.Alterar(p);
            return RedirectToAction("Index");
        }
    }
}