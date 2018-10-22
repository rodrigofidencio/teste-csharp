using fundagMVC.Models;
using System.Linq;
using System.Web.Mvc;
using X.PagedList;

namespace fundagMVC.Controllers
{
    public class ProjetoController : Controller
    {
        // GET: Projeto
        public ActionResult Index(int? pagina)
        {
            if (Session["SessionID"] != null)
            {
                ProjetoModel projeto = new ProjetoModel();

                string SessionID = (string)Session["SessionID"];
                var proj = projeto.ListaProjetos(SessionID);

                // Configuração da paginação
                int paginaTamanho = 10;
                int paginaNumero = (pagina ?? 1);

                var listaProjetos = proj.OrderBy(p => p.ProjetoID)
                                                .ToPagedList(paginaNumero, paginaTamanho);

                return View(listaProjetos);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult CadastroProjeto()
        {
            return View();
        }

        public ActionResult ProjetoDetalhe()
        {
            return View();
        }
    }
}