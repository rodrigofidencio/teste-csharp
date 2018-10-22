using Aptus.Util.SqlGT;
using fundagMVC.Classes;
using fundagMVC.Classes.RetornosSP;
using System;
using System.Collections.Generic;

namespace fundagMVC.Models
{
    public class ProjetoModel
    {
        public List<Projeto> ListaProjetos(string SessionID)
        {
           List<Projeto> listaProjeto = new List<Projeto>(){ };            

            SqlGT gt = new SqlGT("default");
            try
            { 
                //Relação de Projetos
                gt.AddParameter("Session_Id", SessionID);            

                SqlGTResults listaprojeto = gt.Execute("SP_RD_ListarProjetos"); //chamar procedure
                if (listaprojeto.ResultType == SqlGTResultType.RESULT_SUCCESS)
                {
                    foreach (var p in listaprojeto.GetList<ProjetoRetorno>("DEFAULT"))
                    {

                        Projeto projeto = new Projeto();
                        projeto.ProjetoID = p.ProjetoID;
                        projeto.Codigo = p.Codigo;
                        projeto.Descricao = p.Descricao;
                        projeto.CodAcompanhamento = p.CodAcompanhamento;
                        projeto.IntegracaoCodigo = p.IntegracaoCodigo;

                        listaProjeto.Add(projeto);
                    }
                }
                gt.Dispose();
                gt = null;
            }
            catch (Exception ex)
            {
            }
            return listaProjeto;
        }
    }
}