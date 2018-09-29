using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ViagensOnline.Mvc.Areas.Admin.Models;

namespace ViagensOnline.Mvc.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private string AuthenticationType = "ViagensOnlineCookieAuth";

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (viewModel.Email != "avelino.vitor@gmail.com" 
                || viewModel.Senha != "123")
            {
                ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");

                return View(viewModel);
            }

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, viewModel.Email));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var identidade = new ClaimsIdentity(claims, AuthenticationType);

            Request.GetOwinContext().Authentication.SignIn(identidade);

            var returnUrl = Request.QueryString["ReturnUrl"];

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return Redirect("~/" + returnUrl);
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(AuthenticationType);

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}