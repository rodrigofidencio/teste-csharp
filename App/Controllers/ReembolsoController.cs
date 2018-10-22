using fundagMVC.Classes;
using fundagMVC.Classes.DTO;
using fundagMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using X.PagedList;

namespace fundagMVC.Controllers
{
    public class ReembolsoController : Controller
    {
        // GET: Reembolso
        public ActionResult Index()
        {
            if (Session["SessionID"] != null)
            {
                string SessionID = (string)Session["SessionID"];
                ReembolsoModel reembolso = new ReembolsoModel();
                UtilModel utils = new UtilModel();

                //Define a ação do botão novo
                ViewBag.Rota = "CadastroReembolso";
                ViewBag.ContCadastro = "Reembolso";

                //Carrega as solicitações
                var reb = reembolso.ListaReembolso(SessionID, 0);

                return View(reb);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult CadastroReembolso()
        {
            if (Session["SessionID"] != null)
            {
                string SessionID = (string)Session["SessionID"];
                ReembolsoModel reembolso = new ReembolsoModel();
                ProjetoModel projeto = new ProjetoModel();
                UtilModel utils = new UtilModel();

                //carrega dropdow de projetos / Solicitante / Banco
                ViewBag.Projetos = projeto.ListaProjetos(SessionID);
                ViewBag.Solicitate = utils.ListaColaborador(SessionID);
                ViewBag.Banco = utils.ListaBancos();

                //Carrega as solicitações
                var reb = reembolso.ListaReembolso(SessionID, 0);
                return View(reb);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult CadastrarReembolso(List<ReembolsoDTO> reembolso)
        {
            ReembolsoModel reembolsoModel = new ReembolsoModel();
            string SessionID = (string)Session["SessionID"];

            var reembolsoID = reembolsoModel.CriarReembolso(reembolso, SessionID).Codigo;

            return Json(Url.Action("ReembolsoLancamento", "Reembolso", new { id = reembolsoID}));                      
        }

        public ActionResult ReembolsoLancamento(string id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult GridSolicitacoesReembolso(int? Colaborador)
        {
            string SessionID = (string)Session["SessionID"];
            ReembolsoModel reembolso = new ReembolsoModel();

            //Carrega as solicitações
            var reb = reembolso.ListaReembolso(SessionID, Colaborador);
           
            return PartialView("_GridSolicitacoesReembolso", reb);
        }

        [HttpPost]
        public ActionResult CarregarInfoSolicitante(int ColaboradorID)
        {
            UtilModel util = new UtilModel();
            string SessionID = (string)Session["SessionID"];

            //carregar informações do usuario selecionado no dropdownlist
            var colaborador = util.CarregarColaborador(SessionID, ColaboradorID);

            return Json(colaborador);
        }

    }
}