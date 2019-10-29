using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository;

namespace EcommerceOsorio.Controllers
{
    public class ProdutoController : Controller
    {
        //readonly serve para dizer que o objeto só receberá informação no 
        //contrutor ou na criação do objeto
        private readonly ProdutoDAO _produtoDAO;
        private readonly CategoriaDAO _categoriaDAO;
        public ProdutoController(ProdutoDAO produtoDAO, CategoriaDAO categoriaDAO)
        {
            _produtoDAO = produtoDAO;
            _categoriaDAO = categoriaDAO;
        }
        public IActionResult Cadastrar()
        {
            ViewBag.Categorias =
                new SelectList(_categoriaDAO.ListarTodos(), "CategoriaId", "Nome");
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(Produto p, int drpCategorias)
        {
            ViewBag.Categorias =
                new SelectList(_categoriaDAO.ListarTodos(), "CategoriaId", "Nome");
            if (ModelState.IsValid)
            {
                p.Categoria = _categoriaDAO.BuscarPorId(drpCategorias);
                if (_produtoDAO.Cadastrar(p))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Esse produto já existe!");
                return View(p);
            }
            return View(p);
        }
        public IActionResult Index()
        {
            ViewBag.DataHora = DateTime.Now;
            return View(_produtoDAO.ListarTodos());
        }
        public IActionResult Remover(int id)
        {
            _produtoDAO.RemoverProduto(id);
            return RedirectToAction("Index");
        }

        public IActionResult Alterar(int id)
        {
            return View(_produtoDAO.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Alterar(Produto p)
        {
            _produtoDAO.Alterar(p);
            return RedirectToAction("Index");
        }
    }
}