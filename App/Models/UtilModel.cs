using Aptus.Util.SqlGT;
using fundagMVC.Classes;
using fundagMVC.Classes.EnvioSP;
using fundagMVC.Classes.RetornosSP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;

namespace fundagMVC.Models
{
    public class UtilModel
    {
        string conn = WebConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public AcessoModulo Menu()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            AcessoModulo menu = new AcessoModulo();
            SqlConnection cnn = new SqlConnection(conn);
            try
            {
                //da = new SqlDataAdapter("", cnn);
                //da.SelectCommand.Parameters.Add(new SqlParameter("@Perfil", Perfil));

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (cnn.State == ConnectionState.Open) cnn.Close();
            }
            return menu;
        }

        public List<Unidade> ListaUnidade(string SessionID, string Tipo = "")
        {
            List<Unidade> listaUnidade = new List<Unidade>() { };

            SqlGT gt = new SqlGT("default");
            FiltroLista filtro = new FiltroLista();
            IList<FiltroLista> listaFiltro = new List<FiltroLista>() { };

            try
            {
                //Foi criado duas listas uma para Tipo e outra para Situação : by Leo
                filtro = new FiltroLista();
                filtro.Codigo = Tipo;
                listaFiltro.Add(filtro);

                gt.AddParameter("Session_Id", SessionID);
                gt.AddParameter("Atributos", listaFiltro.ToList<object>());

                SqlGTResults unidadeResponse = gt.Execute("SP_RD_ListarEntidades"); //chamar procedure
                if (unidadeResponse.ResultType == SqlGTResultType.RESULT_SUCCESS)
                {
                    foreach (var u in unidadeResponse.GetList<UnidadeRetorno>("DEFAULT"))
                    {
                        Unidade unidade = new Unidade();
                        unidade.Codigo = u.Codigo;
                        unidade.Nome = u.Nome;
                        unidade.RazaoSocial = u.RazaoSocial;
                        unidade.CNPJ = u.CNPJ;
                        unidade.TipoPessoa = u.TipoPessoa;
                        unidade.IntegracaoCodigo = u.IntegracaoCodigo;

                        listaUnidade.Add(unidade);
                    }
                }
            }
            catch (Exception ex) { }

            return listaUnidade;
        }

        public List<Colaborador> ListaColaborador(string SessionID, string Tipo = "")
        {
            List<Colaborador> listaColaborador = new List<Colaborador>() { };

            SqlGT gt = new SqlGT("default");

            try
            {
                gt.AddParameter("Session_Id", SessionID);

                SqlGTResults colaboradorResponse = gt.Execute("SP_RD_ListarColaboradores"); //chamar procedure
                if (colaboradorResponse.ResultType == SqlGTResultType.RESULT_SUCCESS)
                {
                    foreach (var c in colaboradorResponse.GetList<ColaboradorRetorno>("DEFAULT"))
                    {
                        Colaborador colaborador = new Colaborador();
                        colaborador.ColaboradorID = c.ColaboradorID;
                        colaborador.Codigo = c.Codigo;                      
                        colaborador.Nome = c.Nome;
                        colaborador.Email = c.Email;
                        colaborador.IntegracaoCodigo = c.IntegracaoCodigo;

                        listaColaborador.Add(colaborador);
                    }
                }
            }
            catch (Exception ex) { }

            return listaColaborador;
        }

        public Colaborador CarregarColaborador(string SessionID, int ColaboradorID)
        {
            Colaborador colaborador = new Colaborador();

            SqlGT gt = new SqlGT("default");

            try
            {
                gt.AddParameter("Session_Id", SessionID);
                gt.AddParameter("ColaboradorID", ColaboradorID);

                SqlGTResults colaboradorResponse = gt.Execute("SP_RD_ObterColaborador"); //chamar procedure
                if (colaboradorResponse.ResultType == SqlGTResultType.RESULT_SUCCESS)
                {
                    foreach (var c in colaboradorResponse.GetList<ColaboradorRetorno>("DEFAULT"))
                    {
                        colaborador.ColaboradorID = c.ColaboradorID;
                        colaborador.Codigo = c.Codigo;
                        colaborador.Nome = c.Nome;
                        colaborador.Email = c.Email;
                        colaborador.IntegracaoCodigo = c.IntegracaoCodigo;
                    }

                    foreach (var b in colaboradorResponse.GetList<ColaboradorBancoRetorno>("BANCO"))
                    {
                        colaborador.ColaboradorBanco = new ColaboradorBanco();
                        colaborador.ColaboradorBanco.BancoID = b.BancoID;
                        colaborador.ColaboradorBanco.BancoCodigo = b.BancoCodigo;
                        colaborador.ColaboradorBanco.BancoNome = b.BancoNome;
                        colaborador.ColaboradorBanco.BancoAgencia = b.BancoAgencia;
                        colaborador.ColaboradorBanco.BancoAgenciaDigito = b.BancoAgenciaDigito;
                        colaborador.ColaboradorBanco.BancoContaCorrente = b.BancoContaCorrente;
                        colaborador.ColaboradorBanco.BancoContaCorrenteDigito = b.BancoContaCorrenteDigito;
                       
                    }
                }
            }
            catch (Exception ex) { }

            return colaborador;
        }

        public List<SolicitacaoSituacao> ListaSituacao()
        {
            List<SolicitacaoSituacao> listaSituacao = new List<SolicitacaoSituacao>() { };

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection cnn = new SqlConnection(conn);

            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                da = new SqlDataAdapter("SELECT Id,Codigo, Descricao, StatusDeletado FROM RD_TipoSituacaoSolicitacao", cnn);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        SolicitacaoSituacao solicitacaoSituacao = new SolicitacaoSituacao();
                        solicitacaoSituacao.Id = (int)dr["Id"];
                        solicitacaoSituacao.Codigo = (string)dr["Codigo"];
                        solicitacaoSituacao.Descricao = (string)dr["Descricao"];
                        solicitacaoSituacao.StatusDeletado = (bool)dr["StatusDeletado"];

                        listaSituacao.Add(solicitacaoSituacao);
                    }
                }
            }
            catch (Exception ex) { }
            finally { if (cnn.State != ConnectionState.Open) cnn.Open(); }


            return listaSituacao;
        }

        public List<TipoSolicitacao> ListaTipoSolicitacao()
        {
            List<TipoSolicitacao> listaTipo = new List<TipoSolicitacao>() { };

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection cnn = new SqlConnection(conn);

            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                da = new SqlDataAdapter("SELECT Id, Codigo, Descricao, Classificacao,StatusDeletado  FROM RD_TipoSolicitacao", cnn);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TipoSolicitacao tipo = new TipoSolicitacao();

                        tipo.Id = (int)dr["Id"];
                        tipo.Codigo = (string)dr["Codigo"];
                        tipo.Descricao = (string)dr["Descricao"];
                        tipo.Classificacao = (string)dr["Classificacao"];
                        tipo.StatusDeletado = (bool)dr["StatusDeletado"];

                        listaTipo.Add(tipo);
                    }
                }
            }
            catch (Exception ex) { }
            finally { if (cnn.State != ConnectionState.Open) cnn.Open(); }

            return listaTipo;

        }

        public List<Banco> ListaBancos()
        {
            List<Banco> listaBanco = new List<Banco>() { };
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection cnn = new SqlConnection(conn);

            try
            {
                if (cnn.State != ConnectionState.Open) cnn.Open();
                da = new SqlDataAdapter("SELECT Id, Codigo, Descricao, EmpresaID FROM RD_BANCO WHERE statusDeletado = 0", cnn);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Banco banco = new Banco();
                        banco.Id = (int)dr["Id"];
                        banco.Codigo = (string)dr["Codigo"];
                        banco.Descricao = (string)dr["Descricao"];
                        banco.EmpresaID = (int)dr["EmpresaID"];

                        listaBanco.Add(banco);
                    }
                }
            }
            catch (Exception ex) { }

            return listaBanco;
        }
    }
}