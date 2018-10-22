using Aptus.Util.SqlGT;
using fundagMVC.Classes;
using fundagMVC.Classes.DTO;
using fundagMVC.Classes.EnvioSP;
using fundagMVC.Classes.RetornosSP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fundagMVC.Models
{
    public class SolicitacaoModel
    {
        private object gt;

        public Usuario SolicitacoesPorUsuario(string SessionID, string Tipo = "")
        {
            Usuario usuario = new Usuario();
            usuario.SolicitacoesNaoEnviada = new List<Solicitacao>() { };
            usuario.SolicitacoesPendente = new List<Solicitacao>() { };
            usuario.SolicitacoesFinalizadas = new List<Solicitacao>() { };
            usuario.Projetos = new List<Projeto>() { };

            SqlGT gt = new SqlGT("default");
            try
            {
                //Solicitações:
                //NE - Não enviado
                //AP - Aprovação Pendente
                //FI - Finalizado
                string[] Situacao = { "NE", "AP", "FI" };

                foreach (var situacao in Situacao)
                {
                    switch (situacao)
                    {
                        case "NE":
                            var listaNaoEnviada = TipoSolicitacao(SessionID, Tipo, situacao.ToString());
                            foreach (var naoEnviada in listaNaoEnviada)
                            {
                                usuario.SolicitacoesNaoEnviada.Add(naoEnviada);
                            }
                            break;
                        case "AP":
                            var listaPendente = TipoSolicitacao(SessionID, Tipo, situacao.ToString());
                            foreach (var pendente in listaPendente)
                            {
                                usuario.SolicitacoesNaoEnviada.Add(pendente);
                            }
                            break;
                        case "FI":
                            var listaFinalizada = TipoSolicitacao(SessionID, Tipo, situacao.ToString());
                            foreach (var finalizada in listaFinalizada)
                            {
                                usuario.SolicitacoesNaoEnviada.Add(finalizada);
                            }
                            break;
                    }
                }

                //Listagem de projetos
                gt.AddParameter("Session_Id", SessionID);

                SqlGTResults listaprojeto = gt.Execute("SP_RD_ListarProjetos");
                if (listaprojeto.ResultType == SqlGTResultType.RESULT_SUCCESS)
                {
                    foreach (var p in listaprojeto.GetList<ProjetoRetorno>("DEFAULT"))
                    {
                        Projeto projeto = new Projeto();
                        projeto.Codigo = p.Codigo;
                        projeto.Descricao = p.Descricao;
                        projeto.CodAcompanhamento = p.CodAcompanhamento;
                        projeto.IntegracaoCodigo = p.IntegracaoCodigo;

                        usuario.Projetos.Add(projeto);
                    }
                }

                gt.Dispose();
                gt = null;
            }
            catch (Exception ex)
            {
            }
            return usuario;
        }

        public List<Solicitacao> TipoSolicitacao(string SessionID, string Tipo = "", string Situacao = "", string Colaborador = "")
        {
            List<Solicitacao> listaSolicitacao = new List<Solicitacao>() { };
            IList<FiltroLista> listaTipo = new List<FiltroLista>() { };
            IList<FiltroLista> listaSituacao = new List<FiltroLista>() { };

            FiltroLista filtro = new FiltroLista();
            SqlGT gt = new SqlGT("default");

            try
            {
                gt.AddParameter("Session_Id", SessionID);
                //Foi criado duas listas uma para Tipo e outra para Situação : by Leo
                if (!String.IsNullOrEmpty(Tipo))
                {
                    filtro = new FiltroLista();
                    filtro.Codigo = Tipo;
                    listaTipo.Add(filtro);
                    gt.AddParameter("ClassificacaoSolicitacao", listaTipo.ToList<object>());
                }

                if (!String.IsNullOrEmpty(Situacao))
                {
                    filtro = new FiltroLista();
                    filtro.Codigo = Situacao;
                    listaSituacao.Add(filtro);
                    gt.AddParameter("Situacao", listaSituacao.ToList<object>());
                }

                if (!String.IsNullOrEmpty(Colaborador))
                {
                    filtro = new FiltroLista();
                    filtro.Codigo = Colaborador;
                    listaSituacao.Add(filtro);
                    gt.AddParameter("Solicitante", listaSituacao.ToList<object>());
                }

                SqlGTResults solicitacaoResponse = gt.Execute("SP_RD_PesquisarSolicitacao"); //chamar procedure
                if (solicitacaoResponse.ResultType == SqlGTResultType.RESULT_SUCCESS)
                {
                    foreach (var s in solicitacaoResponse.GetList<SolicitacaoRetorno>("DEFAULT"))
                    {

                        Solicitacao solicitacao = new Solicitacao();
                        solicitacao.SolicitacaoID = s.SolicitacaoID;
                        solicitacao.TipoSolicitacaoDescricao = s.TipoSolicitacaoDescricao;
                        solicitacao.Codigo = s.Codigo;
                        solicitacao.MoedaID = s.MoedaID;
                        solicitacao.DescricaoMoeda = s.DescricaoMoeda;
                        solicitacao.SolicitanteID = s.SolicitanteID;
                        solicitacao.DescricaoSolicitante = s.DescricaoSolicitante;
                        solicitacao.Data = s.Data;
                        solicitacao.ValorTotal = s.ValorTotal;
                        solicitacao.ValorTotalBase = s.ValorTotalBase;
                        solicitacao.SituacaoID = s.SituacaoID;
                        solicitacao.SituacaoDescricao = s.SituacaoDescricao;
                        solicitacao.ProjetoID = s.ProjetoID;
                        solicitacao.ProjetoDescricao = s.ProjetoDescricao;

                        listaSolicitacao.Add(solicitacao);
                    }
                }
            }
            catch (Exception ex) { }

            return listaSolicitacao;
        }

        public List<Solicitacao> Solicitacoes(string SessionID, string Tipo = "")
        {
            List<Solicitacao> listaSolicitacao = new List<Solicitacao>() { };

            var listaNaoEnviada = TipoSolicitacao(SessionID, Tipo);
            foreach (var solicitacao in listaNaoEnviada)
            {
                listaSolicitacao.Add(solicitacao);
            }

            return listaSolicitacao;
        }

        public List<Solicitacao> FiltrarSolicitacao(List<SolicitacaoDTO> solicitacaoDTO, string SessionID)
        {
            List<Solicitacao> listaSolicitacoes = new List<Solicitacao>() { };
            FiltroLista filtro = new FiltroLista();
            SqlGT gt = new SqlGT("default");

            try
            {
                //Foi criado 9 listas para passar o objeto para a SP : by Leo                
                IList<FiltroLista> listaSolicitante = new List<FiltroLista>() { };
                IList<FiltroLista> listaProjeto = new List<FiltroLista>() { };
                IList<FiltroLista> listaSituacao = new List<FiltroLista>() { };
                IList<FiltroLista> listaTipoSolicitacao = new List<FiltroLista>() { };
                IList<FiltroLista> listaPesquisador = new List<FiltroLista>() { };
                IList<FiltroLista> listaCoordenador = new List<FiltroLista>() { };
                IList<FiltroLista> listaUnidadeContratante = new List<FiltroLista>() { };
                IList<FiltroLista> listaUnidadeSolicitadora = new List<FiltroLista>() { };
                IList<FiltroLista> listaUnidadeRealizadora = new List<FiltroLista>() { };

                foreach (var s in solicitacaoDTO)
                {
                    foreach (var solicitante in s.Solicitante)
                    {
                        filtro = new FiltroLista();
                        filtro.Codigo = solicitante;
                        listaSolicitante.Add(filtro);
                    }

                    foreach (var projeto in s.Projeto)
                    {
                        filtro = new FiltroLista();
                        filtro.Codigo = projeto;
                        listaProjeto.Add(filtro);
                    }

                    foreach (var situacao in s.Situacao)
                    {
                        filtro = new FiltroLista();
                        filtro.Codigo = situacao;
                        listaSituacao.Add(filtro);
                    }

                    foreach (var unidadeContratante in s.UnidadeContratante)
                    {
                        filtro = new FiltroLista();
                        filtro.Codigo = unidadeContratante;
                        listaUnidadeContratante.Add(filtro);
                    }

                    foreach (var unidadeSolicitadora in s.UnidadeSolicitadora)
                    {
                        filtro = new FiltroLista();
                        filtro.Codigo = unidadeSolicitadora;
                        listaUnidadeSolicitadora.Add(filtro);
                    }

                    foreach (var unidadeRealizadora in s.UnidadeRealizadora)
                    {
                        filtro = new FiltroLista();
                        filtro.Codigo = unidadeRealizadora;
                        listaUnidadeRealizadora.Add(filtro);
                    }

                    foreach (var coordenador in s.Coordenador)
                    {
                        filtro = new FiltroLista();
                        filtro.Codigo = coordenador;
                        listaCoordenador.Add(filtro);
                    }

                    foreach (var pesquisador in s.Pesquisador)
                    {
                        filtro = new FiltroLista();
                        filtro.Codigo = pesquisador;
                        listaPesquisador.Add(filtro);
                    }

                    foreach (var tipoSolicitacao in s.TipoSolicitacao)
                    {
                        filtro = new FiltroLista();
                        filtro.Codigo = tipoSolicitacao;
                        listaTipoSolicitacao.Add(filtro);
                    }

                    //Enviar requisição para a SP
                    gt.AddParameter("Session_Id", SessionID);
                    gt.AddParameter("DataInicio", s.DataHoraFim.ToString());
                    gt.AddParameter("DataFim", s.DataHoraFim.ToString());
                    gt.AddParameter("Solicitante", listaSolicitante.ToList<object>());
                    gt.AddParameter("Projeto", listaProjeto.ToList<object>());
                    gt.AddParameter("Situacao", listaTipoSolicitacao.ToList<object>());
                    gt.AddParameter("TipoSolicitacao", listaTipoSolicitacao.ToList<object>());
                    gt.AddParameter("Coordenador", listaCoordenador.ToList<object>());
                    gt.AddParameter("Pesquisador", listaPesquisador.ToList<object>());
                    gt.AddParameter("UnidadeContratante",listaUnidadeContratante.ToList<object>());
                    gt.AddParameter("UnidadeSolicitadora",listaUnidadeSolicitadora.ToList<object>());
                    gt.AddParameter("UnidadeRealizadora",listaUnidadeRealizadora.ToList<object>());
                    gt.AddParameter("ClassificacaoSolicitacao", listaUnidadeRealizadora.ToList<object>());
                    
                    SqlGTResults solicitacaoResponse = gt.Execute("SP_RD_PesquisarSolicitacao"); //chamar procedure
                    if (solicitacaoResponse.ResultType == SqlGTResultType.RESULT_SUCCESS)
                    {
                        foreach (var sol in solicitacaoResponse.GetList<SolicitacaoRetorno>("DEFAULT"))
                        {
                            Solicitacao solicitacao = new Solicitacao();
                            solicitacao.SolicitacaoID = sol.SolicitacaoID;
                            solicitacao.TipoSolicitacaoDescricao = sol.TipoSolicitacaoDescricao;
                            solicitacao.Codigo = sol.Codigo;
                            solicitacao.MoedaID = sol.MoedaID;
                            solicitacao.DescricaoMoeda = sol.DescricaoMoeda;
                            solicitacao.SolicitanteID = sol.SolicitanteID;
                            solicitacao.DescricaoSolicitante = sol.DescricaoSolicitante;
                            solicitacao.Data = sol.Data;
                            solicitacao.ValorTotal = sol.ValorTotal;
                            solicitacao.ValorTotalBase = sol.ValorTotalBase;
                            solicitacao.SituacaoID = sol.SituacaoID;
                            solicitacao.SituacaoDescricao = sol.SituacaoDescricao;
                            solicitacao.ProjetoID = sol.ProjetoID;
                            solicitacao.ProjetoDescricao = sol.ProjetoDescricao;

                            listaSolicitacoes.Add(solicitacao);
                        }
                    }
                }
            }
            catch (Exception ex) { }

            return listaSolicitacoes;
        }

        public List<Solicitacao> ListaCriacaoRelatorio(string SessionID, int? Colaborador)
        {
            List<Solicitacao> listaSolitacao = new List<Solicitacao>() { };
            IList<FiltroLista> listaTipo = new List<FiltroLista>() { };

            FiltroLista filtro = new FiltroLista();
            SqlGT gt = new SqlGT("default");

            try
            {

                if (Colaborador > 0)
                {
                    gt.AddParameter("Session_Id", SessionID);
                    gt.AddParameter("SolicitanteID", (int)Colaborador);
                }
                else
                {
                    gt.AddParameter("Session_Id", SessionID);
                    gt.AddParameter("SolicitanteID", 0);
                }

                //Sempre carrega o tipo de solicitação de Reembolso              
                gt.AddParameter("TipoSolicitacaoID", 5);


                SqlGTResults solicitacaoResponse = gt.Execute("SP_RD_CriarSolicitacaoListarDados"); //chamar procedure
                if (solicitacaoResponse.ResultType == SqlGTResultType.RESULT_SUCCESS)
                {
                    foreach (var s in solicitacaoResponse.GetList<SolicitacaoRetorno>("SOLICITACAO_VINCULADA"))
                    {
                        Solicitacao solicitacao = new Solicitacao();
                        solicitacao.SolicitacaoID = s.SolicitacaoID;
                        solicitacao.TipoSolicitacaoDescricao = s.TipoSolicitacaoDescricao;
                        solicitacao.Codigo = s.Codigo;
                        solicitacao.MoedaID = s.MoedaID;
                        solicitacao.DescricaoMoeda = s.DescricaoMoeda;
                        solicitacao.SolicitanteID = s.SolicitanteID;
                        solicitacao.DescricaoSolicitante = s.DescricaoSolicitante;
                        solicitacao.Data = s.Data;
                        solicitacao.ValorTotal = s.ValorTotal;
                        solicitacao.ValorTotalBase = s.ValorTotalBase;
                        solicitacao.SituacaoID = s.SituacaoID;
                        solicitacao.SituacaoDescricao = s.SituacaoDescricao;
                        solicitacao.ProjetoID = s.ProjetoID;
                        solicitacao.ProjetoDescricao = s.ProjetoDescricao;

                        listaSolitacao.Add(solicitacao);
                    }
                }

            }
            catch (Exception ex) { }

            return listaSolitacao;
        }

    }
}