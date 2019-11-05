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
        private readonly UsuarioDAO _usuarioDAO;
        public UsuarioController(UsuarioDAO usuarioDAO)
        {
            _usuarioDAO = usuarioDAO;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            Usuario u = new Usuario();
            if (TempData["Usuario"] != null)
            {
                u = JsonConvert.DeserializeObject<Usuario>(TempData["Usuario"].ToString());
            }
            return View(u);
        }

        [HttpPost]
        public IActionResult BuscarCep(Usuario u)
        {
            try
            {
                string url = "https://viacep.com.br/ws/" + u.Endereco.Cep + "/json/";
                WebClient client = new WebClient();
                u.Endereco = JsonConvert.DeserializeObject<Endereco>(client.DownloadString(url));
                TempData["Usuario"] = JsonConvert.SerializeObject(u);
            }
            catch (Exception)
            {
                //Mostrar uma mensagem para o usuário
            }
            return RedirectToAction("Cadastrar");
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario u)
        {
            if (ModelState.IsValid)
            {
                if (_usuarioDAO.Cadastrar(u))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("","Esse e-mail já está sendo usado!");
            }
            return View(u);
        }
    }
}