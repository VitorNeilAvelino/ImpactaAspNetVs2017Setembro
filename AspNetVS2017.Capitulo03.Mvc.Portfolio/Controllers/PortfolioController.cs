using AspNetVS2017.Capitulo03.Mvc.Portfolio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetVS2017.Capitulo03.Mvc.Portfolio.Controllers
{
    public class PortfolioController : Controller
    {
        // GET: Portfolio
        public ActionResult Index()
        {
            const string diretorioImagens = "/Content/Imagens/Portfolio";
            var caminhos = Directory.EnumerateFiles(Server.MapPath(diretorioImagens));

            var viewModel = new PortfolioViewModel();
            //viewModel.CaminhosImagens = new List<string>();

            foreach (var caminho in caminhos)
            {
                viewModel.CaminhosImagens.Add($"{diretorioImagens}/{Path.GetFileName(caminho)}");
            }

            return View(viewModel);
        }
    }
}