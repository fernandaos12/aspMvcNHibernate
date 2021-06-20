using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using aspMvcNHibernate.Models;
using aspMvcNHibernate.Repositories;
using aspMvcNHibernate.Models;
using System;
using System.Web.Mvc;

namespace aspMvcNHibernate.Controller
{
    public class FuncionariosController : Controller
    {
        private readonly FuncionarioRepository funcionariorepository;

        public FuncionariosController(NHibernate.ISession session) =>
            funcionariorepository = new FuncionarioRepository(session);

        public IActionResult Index()
        {
            return View(funcionariorepository.FindAll().ToList());
        }


        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return StatusCode(404);
            }
            Funcionarios funcionario = await FuncionarioRepository.FindById(id.Value);
            if (funcionario == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(funcionario);
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id, Nome, Idade, Salario")] Funcionarios funcionario)
        {
            if (ModelState.IsValid)
            {
                await funcionariorepository.Add(funcionario);
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Funcionarios funcionario = await funcionariorepository.FindById(id.Value);
            if (funcionario == null)
            {
                return StatusCodeResult(StatusCodes.Status404NotFound);
            }
            return View(funcionario);
        }

        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Funcionarios funcionario = await funcionariorepository.FindById(id.Value);
            if (funcionario == null)
            {
                return StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return View(funcionario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await funcionariorepository.Remove(id);
            return RedirectToActionResult("Index");
        }
    }

}
