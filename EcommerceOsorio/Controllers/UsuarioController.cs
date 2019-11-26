using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository;

namespace EcommerceOsorio.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioDAO _usuarioDAO;
        private readonly UserManager<UsuarioLogado> _userManager;
        private readonly SignInManager<UsuarioLogado> _signInManager;
        public UsuarioController(UsuarioDAO usuarioDAO,
            UserManager<UsuarioLogado> userManager,
            SignInManager<UsuarioLogado> signInManager)
        {
            _usuarioDAO = usuarioDAO;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Cadastrar(Usuario u)
        {
            if (ModelState.IsValid)
            {
                //Preencher obrigatoriamente o UserName e Email
                UsuarioLogado uLogado = new UsuarioLogado
                {
                    UserName = u.Email,
                    Email = u.Email
                };

                IdentityResult result = await _userManager.CreateAsync(uLogado, u.Senha);
                if (result.Succeeded)
                {
                    string codigo =
                        await _userManager.GenerateEmailConfirmationTokenAsync(uLogado);
                    await _signInManager.SignInAsync(uLogado, isPersistent: false);
                    if (_usuarioDAO.Cadastrar(u))
                    {
                        return RedirectToAction("Index", "Produto");
                    }
                    ModelState.AddModelError("", "Esse e-mail já está sendo usado!");
                }
                AdicionarErros(result);
            }
            return View(u);
        }
        public void AdicionarErros(IdentityResult result)
        {
            foreach (var erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Usuario u)
        {
            Microsoft.AspNetCore.Identity.SignInResult result =
                await _signInManager.
                PasswordSignInAsync(u.Email, u.Senha, true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Produto");
            }
            ModelState.AddModelError("", "Falha no login!");
            return View(u);
        }
    }
}