using Aptus.Util.SqlGT;
using fundagMVC.Classes;
using fundagMVC.Classes.DTO;
using fundagMVC.Classes.EnvioSP;
using fundagMVC.Classes.RetornosSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Models
{
    public class ReembolsoModel
    {
        public UsuarioReembolso ListaReembolso(string SessionID, int? Colaborador)
        {
            UsuarioReembolso usuarioReembolso = new UsuarioReembolso();
            usuarioReembolso.Colaborador = new List<Colaborador>() { };
            usuarioReembolso.Solicitacaos = new List<Solicitacao>() { };
            usuarioReembolso.Adiantamentos = new List<Solicitacao>() { };

            SolicitacaoModel solicitacaoModel = new SolicitacaoModel();
            UtilModel utilModel = new UtilModel();

            try
            {
                var listaSolicitacao = solicitacaoModel.ListaCriacaoRelatorio(SessionID, Colaborador);
                var listaColaborador = utilModel.ListaColaborador(SessionID);

                //Carregar as solicitalações do usuario logado como default
                foreach (var s in listaSolicitacao)
                {
                    usuarioReembolso.Solicitacaos.Add(s);
                }

                foreach (var c in listaColaborador)
                {
                    usuarioReembolso.Colaborador.Add(c);
                }

            }
            catch (Exception ex)
            {
            }
            return usuarioReembolso;
        }

        public Response CriarReembolso(List<ReembolsoDTO> criarReembolso, string SessionID)
        {
            FiltroLista filtro = new FiltroLista();
            ListaDadosBancarios favorecido = new ListaDadosBancarios();
            SolicitacaoVinculada solicitacaoVinculada = new SolicitacaoVinculada();
            Response response = new Response();

            SqlGT gt = new SqlGT("default");
            string CodigoRetorno = string.Empty;

            List<SolicitacaoVinculada> listaAdiantamento = new List<SolicitacaoVinculada > () { };
            List<ListaDadosBancarios> listaDadosBancarios = new List<ListaDadosBancarios>() { };

            try
            {
                foreach (var item in criarReembolso)
                {
                    foreach (var adiantamento in item.Adiantamentos)
                    {
                        solicitacaoVinculada = new SolicitacaoVinculada();
                        solicitacaoVinculada.SolicitacaoID = Convert.ToInt32(adiantamento);
                        //solicitacaoVinculada.ValorUtilizado = "";

                        listaAdiantamento.Add(solicitacaoVinculada);
                    }

                    foreach (var banco in item.Favorecido)
                    {
                        favorecido = new ListaDadosBancarios();
                        favorecido.BancoCodigo = banco.BancoCodigo;
                        favorecido.BancoAgencia = banco.Agencia;
                        favorecido.BancoAgenciaDigito = banco.AgenciaDigito;
                        favorecido.BancoFavorecidoNome = banco.Nome;
                        favorecido.BancoFavorecidCPF = banco.CPF;
                        favorecido.BancoFavorecidCNPJ = banco.CNPJ;
                        favorecido.BancoContaCorrente = banco.ContaCorrente;
                        favorecido.BancoContaCorrenteDigito = banco.Digito;

                        listaDadosBancarios.Add(favorecido);
                    } 
                    
                    gt.AddParameter("Session_Id", SessionID);
                    gt.AddParameter("SolicitanteID", item.SolicitanteID);
                    gt.AddParameter("ProjetoID", item.ProjetoID);
                    gt.AddParameter("Titulo", item.Titulo);
                    gt.AddParameter("TipoSolicitacaoID", 5);
                    gt.AddParameter("MoedaID", 1);
                    gt.AddParameter("DadosBancarios", listaDadosBancarios.ToList<Object>());
                    gt.AddParameter("SolicitacaoVinculada", listaAdiantamento.ToList<Object>());
                    
                    SqlGTResults solicitacaoResponse = gt.Execute("SP_RD_CriarSolicitacao"); //chamar procedure
                    if (solicitacaoResponse.ResultType == SqlGTResultType.RESULT_SUCCESS)
                    {
                        //foreach (var r in solicitacaoResponse.GetValue<int>("RESULT"))
                        //{
                           response.Codigo = solicitacaoResponse.GetValue<int>("SolicitacaoID").ToString();
                        //response.Mensagem = r.Mensagem;
                        //response.Expection = r.Expection;
                        //}
                    }
                }
            }
            catch (Exception ex) { }

            return response;
        }
    }
}