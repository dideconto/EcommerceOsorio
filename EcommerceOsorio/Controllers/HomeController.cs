using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using EcommerceOsorio.Utils;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace EcommerceOsorio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoDAO _produtoDAO;
        private readonly CategoriaDAO _categoriaDAO;
        private readonly ItemVendaDAO _itemVendaDAO;
        private readonly UtilsSession _session;
        public HomeController(ProdutoDAO produtoDAO, CategoriaDAO categoriaDAO,
            ItemVendaDAO itemVendaDAO, UtilsSession session)
        {
            _produtoDAO = produtoDAO;
            _categoriaDAO = categoriaDAO;
            _itemVendaDAO = itemVendaDAO;
            _session = session;
        }
        public IActionResult AdicionarAoCarrinho(int id)
        {
            Produto p = _produtoDAO.BuscarPorId(id);
            ItemVenda item = new ItemVenda
            {
                Produto = p,
                Quantidade = 1,
                Preco = p.Preco,
                CarrinhoId = _session.RetornarCarrinhoId()
            };
            _itemVendaDAO.Cadastrar(item);
            return RedirectToAction("CarrinhoCompras");
        }

        public IActionResult CarrinhoCompras()
        {
            return View(_itemVendaDAO.BuscarItensPorCarrinhoId
                (_session.RetornarCarrinhoId()));
        }

        public IActionResult Index(int? id)
        {
            ViewBag.Categorias = _categoriaDAO.ListarTodos();
            if (id == null)
            {
                return View(_produtoDAO.ListarTodos());
            }
            return View(_produtoDAO.ListarPorCategoria(id));
        }
        public IActionResult Detalhes(int id)
        {
            return View(_produtoDAO.BuscarPorId(id));
        }
    }
}