using fundagMVC.Models;
using System.Linq;
using System.Web.Mvc;
using X.PagedList;

namespace fundagMVC.Controllers
{
    public class QuilometragemController : Controller
    {
        // GET: Quilometragem
        public ActionResult Index(int? pagina)
        {
            if (Session["SessionID"] != null)
            {
                string SessionID = (string)Session["SessionID"];
                SolicitacaoModel solicitacoes = new SolicitacaoModel();

                //Define a ação do botão novo
                ViewBag.Rota = "CadastroQuilometragem";
                ViewBag.ContCadastro = "Quilometragem";

                //Carrega as solicitações
                var sol = solicitacoes.Solicitacoes(SessionID, "KM");

                // Configuração da paginação
                int paginaTamanho = 10;
                int paginaNumero = (pagina ?? 1);
                //Carrega 10 solicitações por requisição.
                var listaSolicitacoes = sol.OrderBy(s => s.Codigo)
                                                .ToPagedList(paginaNumero, paginaTamanho);

                return View(listaSolicitacoes);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        
        public ActionResult CadastroQuilometragem()
        {
             if (Session["SessionID"] != null)
            {
                string SessionID = (string)Session["SessionID"];
                SolicitacaoModel Quilometragem = new SolicitacaoModel();
                ProjetoModel projeto = new ProjetoModel();
                UtilModel utils = new UtilModel();

                //carrega dropdow de projetos / Solicitante / Banco
                ViewBag.Projetos = projeto.ListaProjetos(SessionID);
                ViewBag.Solicitate = utils.ListaColaborador(SessionID);
                ViewBag.Banco = utils.ListaBancos();

                //Carrega as solicitações
                var res = Quilometragem.Solicitacoes(SessionID, "KM");
                return View(res);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult QuilometragemLancamento()
        {
            return View();
        }

    }
}