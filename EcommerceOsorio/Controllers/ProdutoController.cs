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
        public IActionResult Cadastrar(Produto p)
        {
            if (ModelState.IsValid)
            {
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
            return View(_produtoDAO.ListarProdutos());
        }
        public IActionResult Remover(int id)
        {
            _produtoDAO.RemoverProduto(id);
            return RedirectToAction("Index");
        }

        public IActionResult Alterar(int id)
        {
            return View(_produtoDAO.BuscarProdutoPorId(id));
        }

        [HttpPost]
        public IActionResult Alterar(Produto p)
        {
            _produtoDAO.Alterar(p);
            return RedirectToAction("Index");
        }
    }
}