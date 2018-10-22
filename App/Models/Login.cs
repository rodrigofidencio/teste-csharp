using Aptus.Util.SqlGT;
using fundagMVC.Classes;
using fundagMVC.Classes.RetornosSP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace fundagMVC.Models
{
    public class Login
    {
        string conn = WebConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        public Usuario LoginColaborador(string UserName, string Senha)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection cnn = new SqlConnection(conn);
            Usuario usuario = new Usuario();
            usuario.Menu = new List<Menu>() { };

            try
            {               
                if (cnn.State != ConnectionState.Open) cnn.Open();

                da = new SqlDataAdapter("SELECT UserName, Senha FROM RD_CredencialAcesso WHERE UserName=@UserName AND Senha=@Senha", cnn);
                da.SelectCommand.Parameters.AddWithValue("@UserName", UserName);
                da.SelectCommand.Parameters.AddWithValue("@Senha", Senha);
                da.Fill(dt);

                if (dt.Rows.Count > 0){
                    //primeira operacao éo login
                    SqlGT gt = new SqlGT("default");               

                    gt.AddParameter("Login", (string)dt.Rows[0]["UserName"]);
                    gt.AddParameter("Empresa", "01");
                    gt.AddParameter("Ambiente", "");
                          
                    SqlGTResults resultado = gt.Execute("SP_RD_IniciarSessao"); //chamar procedure
                    if (resultado.ResultType == SqlGTResultType.RESULT_SUCCESS)
                    {
                        usuario.SessaoID = resultado.GetValue<string>("Session_Id");
                        foreach (var m in resultado.GetList<MenuRetorno>("ACESSO_MODULO"))
                        {
                            Menu menu = new Menu();
                            menu.Codigo = m.Codigo;
                            menu.Descricao = m.Descricao;

                            usuario.Menu.Add(menu);
                        }                       
                    }
                    gt.Dispose();
                    gt = null;

                    usuario.Mensagem = "";
                    usuario.Sucesso = true;                  
                }
                else {
                   usuario.Sucesso = false;
                   usuario.Mensagem = "Usuário não encontrado";
                }
            }
            catch (Exception ex) {
                usuario.Sucesso = false;
                usuario.Mensagem = "Ocorreu um erro na aplicação!";
            }
            finally{
                if (cnn.State == ConnectionState.Open) cnn.Close();
            }

            return usuario;
        }

    }
}