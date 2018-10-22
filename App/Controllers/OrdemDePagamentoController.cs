using fundagMVC.Models;
using System.Linq;
using System.Web.Mvc;
using X.PagedList;

namespace fundagMVC.Controllers
{
    public class OrdemDePagamentoController : Controller
    {
        // GET: OrdemDePagamento
        public ActionResult Index(int? pagina)
        {
            if (Session["SessionID"] != null)
            {
                string SessionID = (string)Session["SessionID"];
                SolicitacaoModel solicitacoes = new SolicitacaoModel();

                //Define a ação do botão novo
                ViewBag.Rota = "CadastroOrdemDePagamento";
                ViewBag.ContCadastro = "OrdemDePagamento";

                //Carrega as solicitações
                var sol = solicitacoes.Solicitacoes(SessionID, "OP");

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

        public ActionResult CadastroOrdemDePagamento()
        {
            if (Session["SessionID"] != null)
            {
                string SessionID = (string)Session["SessionID"];
                SolicitacaoModel OrdemPgto = new SolicitacaoModel();
                ProjetoModel projeto = new ProjetoModel();
                UtilModel utils = new UtilModel();

                //carrega dropdow de projetos / Solicitante / Banco
                ViewBag.Projetos = projeto.ListaProjetos(SessionID);
                ViewBag.Solicitate = utils.ListaColaborador(SessionID);
                ViewBag.Banco = utils.ListaBancos();

                //Carrega as solicitações
                return View(OrdemPgto.Solicitacoes(SessionID, "DI"));
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult OrdemLancamento()
        {
            return View();
        }
    }
}