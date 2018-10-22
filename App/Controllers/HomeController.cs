using fundagMVC.Classes.DTO;
using fundagMVC.Models;
using System.Web.Mvc;

namespace fundagMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["SessionID"] != null)
            {
                SolicitacaoModel solicitacao = new SolicitacaoModel();

                string SessionID = (string)Session["SessionID"];
                var sol = solicitacao.SolicitacoesPorUsuario(SessionID);              

                return View(sol);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDTO credencial)
        {
            if (ModelState.IsValid) //verifica se é válido
            {
                Login login = new Login();                
                var acesso = login.LoginColaborador(credencial.UserName, credencial.Senha);

                if (acesso.Sucesso)
                {
                    Session["SessionID"] = acesso.SessaoID;                   
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

     
    }
}