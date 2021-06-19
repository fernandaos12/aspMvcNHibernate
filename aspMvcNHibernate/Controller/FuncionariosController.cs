using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using aspMvcNHibernate.Models;
using aspMvcNHibernate.Repositories;


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
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
          
}
}
