using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pessoal.Repositorios.SqlServer;
using Microsoft.Extensions.Configuration;
using Pessoal.Dominio;

namespace Pessoal.Mvc.Controllers
{
    public class TarefasController : Controller
    {
        private TarefaRepositorio tarefaRepositorio;

        // Injeção de dependência.
        public TarefasController(IConfiguration configuration)
        {
            tarefaRepositorio = new TarefaRepositorio(
                configuration.GetConnectionString("PessoalConnectionString"));
        }

        // GET: Tarefas
        public ActionResult Index()
        {
            return View(tarefaRepositorio.Selecionar());
        }

        // GET: Tarefas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tarefas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarefas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tarefa tarefa)
        {
            try
            {
                tarefaRepositorio.Inserir(tarefa);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Não foi possível inserir a Tarefa.");

                return View();
            }
        }

        // GET: Tarefas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tarefas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {               

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tarefas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tarefas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}