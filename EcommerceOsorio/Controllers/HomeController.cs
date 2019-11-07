using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace EcommerceOsorio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoDAO _produtoDAO;
        private readonly CategoriaDAO _categoriaDAO;
        public HomeController(ProdutoDAO produtoDAO, CategoriaDAO categoriaDAO)
        {
            _produtoDAO = produtoDAO;
            _categoriaDAO = categoriaDAO;
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