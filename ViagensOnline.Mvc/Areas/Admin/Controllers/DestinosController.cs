using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ViagensOnline.Dominio;
using ViagensOnline.Mvc.Models;
using ViagensOnline.Repositorios.SqlServer;

namespace ViagensOnline.Mvc.Areas.Admin.Controllers
{
    [Authorize]
    public class DestinosController : Controller
    {
        private ViagensOnlineDbContext db = new ViagensOnlineDbContext();
        private string _caminhoImagensDestinos = ConfigurationManager.AppSettings["caminhoImagensDestinos"];

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(Mapear(db.Destinos.ToList()));
        }

        private List<DestinoViewModel> Mapear(List<Destino> destinos)
        {
            var viewModels = new List<DestinoViewModel>();

            foreach (var destino in destinos)
            {
                viewModels.Add(Mapear(destino));
            }

            return viewModels;
        }

        private DestinoViewModel Mapear(Destino destino)
        {
            var viewModel = new DestinoViewModel();

            viewModel.CaminhoImagem = 
                Path.Combine(_caminhoImagensDestinos, destino.NomeImagem);
            viewModel.Cidade = destino.Cidade;
            viewModel.Id = destino.Id;
            viewModel.Nome = destino.Nome;
            viewModel.Pais = destino.Pais;

            return viewModel;
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destino destino = db.Destinos.Find(id);
            if (destino == null)
            {
                return HttpNotFound();
            }
            return View(Mapear(destino));
        }

        // GET: Admin/Destinos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Destinos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DestinoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var destino = Mapear(viewModel);

                SalvarFoto(viewModel.ArquivoFoto);

                db.Destinos.Add(destino);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        private void SalvarFoto(HttpPostedFileBase arquivoFoto)
        {
            var caminhoVirtual = 
                Path.Combine(_caminhoImagensDestinos, arquivoFoto.FileName);
            var caminhoFisico = Request.MapPath(caminhoVirtual);

            arquivoFoto.SaveAs(caminhoFisico);
        }

        private Destino Mapear(DestinoViewModel viewModel)
        {
            var destino = new Destino();

            destino.Cidade = viewModel.Cidade;
            destino.Id = viewModel.Id;
            destino.Nome = viewModel.Nome;
            destino.NomeImagem = viewModel.ArquivoFoto.FileName;
            destino.Pais = viewModel.Pais;

            return destino;
        }

        // GET: Admin/Destinos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destino destino = db.Destinos.Find(id);
            if (destino == null)
            {
                return HttpNotFound();
            }
            return View(Mapear(destino));
        }

        // POST: Admin/Destinos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DestinoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var destino = db.Destinos.Find(viewModel.Id);

                db.Entry(destino).CurrentValues.SetValues(viewModel);
                //db.Entry(destino).State = EntityState.Modified;

                if (viewModel.ArquivoFoto != null)
                {
                    SalvarFoto(viewModel.ArquivoFoto);
                    destino.NomeImagem = viewModel.ArquivoFoto.FileName;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [Authorize(Roles = "MegaMaster")]
        [Authorize(Roles = "Master, Gerente")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Destino destino = db.Destinos.Find(id);
            if (destino == null)
            {
                return HttpNotFound();
            }
            return View(Mapear(destino));
        }

        [Authorize(Roles = "MegaMaster")]
        [Authorize(Roles = "Master, Gerente")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Destino destino = db.Destinos.Find(id);
            db.Destinos.Remove(destino);
            db.SaveChanges();

            System.IO.File.Delete(
                Server.MapPath(Path.Combine(_caminhoImagensDestinos, destino.NomeImagem)));

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
