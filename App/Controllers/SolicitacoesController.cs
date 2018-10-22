using fundagMVC.Classes.DTO;
using fundagMVC.Classes.RetornoJSON;
using fundagMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace fundagMVC.Controllers
{
    public class SolicitacoesController : Controller
    {
        // GET: Solicitacoes
        public ActionResult Index(int? pagina)
        {
            if (Session["SessionID"] != null)
            {
                string SessionID = (string)Session["SessionID"];
                SolicitacaoModel solicitacoes = new SolicitacaoModel();
                ProjetoModel projeto = new ProjetoModel();
                UtilModel util = new UtilModel();               

                //Define a ação do botão novo
                ViewBag.Rota = "";
                ViewBag.ContCadastro = "";

                //Carrega as solicitações
                var sol = solicitacoes.Solicitacoes(SessionID);

                //Configuração da paginação
                int paginaTamanho = 10;
                int paginaNumero = (pagina ?? 1);
                
                //Carrega 10 solicitações por requisição.
                var listaSolicitacoes = sol.OrderBy(s => s.Codigo)
                                                .ToPagedList(paginaNumero, paginaTamanho);

                ViewBag.Situacao = util.ListaSituacao();
                ViewBag.Tipo = util.ListaTipoSolicitacao();

                // Lista Projeto
                ViewBag.Projeto = projeto.ListaProjetos(SessionID);

                //Lista de usuarios do filtro
                ViewBag.Solicitante = util.ListaColaborador(SessionID);
                ViewBag.Coordenador = util.ListaColaborador(SessionID);
                ViewBag.Pesquisador = util.ListaColaborador(SessionID);

                //Lista de Unidades
                ViewBag.UnidadeContratante = util.ListaUnidade(SessionID, "UNIDADE");
                ViewBag.UnidadeSolicitadora = util.ListaUnidade(SessionID, "UNIDADE");
                ViewBag.UnidadeRealizadora = util.ListaUnidade(SessionID, "UNIDADE");

                return View(listaSolicitacoes);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
               
        [HttpGet]
        public JsonResult UnidadeContratante(string Prefix)
        {            
            UtilModel util = new UtilModel();
            string SessionID = (string)Session["SessionID"];


            ListaItem listaItem = new ListaItem();
            listaItem.Itens = new List<Item>() { };

            var unidadeContratante = util.ListaUnidade(SessionID, "UNIDADE");

            var retorno = (from u in unidadeContratante
                           where u.Nome.Contains("PARANA")
                           select new {u.Codigo, u.Nome });

            foreach(var i in retorno)
            {
                Item item = new Item();
                item.codigo = i.Codigo;
                item.Descricao = i.Nome;

                listaItem.Itens.Add(item);
            }
           

            return Json(listaItem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FiltrarSolicitacao(List<SolicitacaoDTO> solicitacao, int? pagina)
        {
            SolicitacaoModel solicitacaoModel = new SolicitacaoModel();
            string SessionID = (string)Session["SessionID"];

            //Configuração da paginação
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);

            var solicitacoes = solicitacaoModel.FiltrarSolicitacao(solicitacao, SessionID);

            //Carrega 10 solicitações por requisição.
            var listaSolicitacoes = solicitacoes.OrderBy(s => s.Codigo)
                                                .ToPagedList(paginaNumero, paginaTamanho);

            return PartialView("_FiltrarSolicitacao", listaSolicitacoes);
        }
    }
}