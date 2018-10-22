using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes.RetornosSP
{
    public class SolicitacaoRetorno
    {
        public int SolicitacaoID;
        public string TipoSolicitacaoDescricao;
        public string Codigo;
        public int SolicitanteID;
        public string DescricaoSolicitante;
        public int MoedaID;
        public string DescricaoMoeda;
        public int SituacaoID;
        public string SituacaoDescricao;
        public int ProjetoID;
        public string ProjetoDescricao;
        public string ValorTotal;
        public string ValorTotalBase;
        public string Data;
    }
}