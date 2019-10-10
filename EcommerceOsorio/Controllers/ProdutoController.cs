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
        private readonly ProdutoDAO produtoDAO;
        public ProdutoController(Context context)
        {
            produtoDAO = new ProdutoDAO(context);
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
            produtoDAO.Cadastrar(p);
            return View();
        }
    }
}