using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository;

namespace EcommerceOsorio.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ProdutoDAO _produtoDAO;
        public UsuarioController(ProdutoDAO produtoDAO)
        {
            _produtoDAO = produtoDAO;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View((Usuario) TempData["Usuario"]);
        }

        [TempData]
        public Usuario Usuario { get; set; }

        [HttpPost]
        public IActionResult BuscarCep(Usuario u)
        {
            string url = "https://viacep.com.br/ws/" + u.Endereco.Cep + "/json/";
            WebClient client = new WebClient();
            string resultado = client.DownloadString(url);
            Usuario.Endereco = JsonConvert.DeserializeObject<Endereco>(resultado);
            TempData["Usuario"] = Usuario;
            return RedirectToAction("Cadastrar");
        }
    }
}